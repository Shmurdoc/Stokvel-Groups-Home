using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountServices.AccountRequestService;

public class AccountRequestServices : IAccountRequestService
{


	private readonly IAccountServices _accountServices;
	private readonly IPrepaymentServices _prepaymentServices;
	private readonly IAccountProfileServices _accountProfileServices;

	public AccountRequestServices(IAccountServices accountServices,
		IPrepaymentServices prepaymentServices,
		IAccountProfileServices accountProfileServices)
	{
		_accountServices = accountServices;
		_prepaymentServices = prepaymentServices;
		_accountProfileServices = accountProfileServices;

	}

	public async Task<List<int>> AccountIdForMemberInGroups(string userId, AccountType? typeAccount) => await _accountServices.AccountIdForMemeberInGroups(userId, typeAccount);

	public async Task StartStokvel(List<int> AccountId, int groupId)
	{
		await _accountServices.StartStokvel(AccountId);
		await _accountServices.StokvelActive(groupId);
	}


	public async Task<int> AccountProfileId(string id) => await _accountServices.AccountProfileId(id);
	//checks to see how many groups a user has joined using bool
	public async Task<List<bool>> TypeAccountExists(string userId)
	{
		List<bool> typeAccountExists = new();
		List<AccountType> listOfAccountTypes = new() { AccountType.Low_Savings_Club, AccountType.Mid_Savings_Club, AccountType.High_Savings_club};


		// Filter to get each Group where true
		for (int i = 0; i < listOfAccountTypes.Count; i++)
		{
			bool list = await _accountServices.GroupNamesExists(userId, listOfAccountTypes[i]);
			if (list != true) continue;
			typeAccountExists.Add(list);

		}

		return typeAccountExists;
	}
	//fetch a list type of accounts for each group member has joined
	public async Task<List<AccountType>> ValidAccountTypesAdded(string userId)
	{
		List<AccountType> listOfAccountTypes = new() { AccountType.Low_Savings_Club, AccountType.Mid_Savings_Club, AccountType.High_Savings_club};
		List<AccountType> AccountTypesAdded = new();

		// Filter to get each Group
		for (int i = 0; i < listOfAccountTypes.Count; i++)
		{
			bool list = await _accountServices.GroupNamesExists(userId, listOfAccountTypes[i]);
			if (list != true) continue;
			AccountTypesAdded.Add(listOfAccountTypes[i]);
		}
		return AccountTypesAdded;
	}


	// Get AccountType List And Cleans it From Any Duplicates 
	public async Task<List<UserGroupMember>> CleanListAccountTypesAdded(List<AccountType> accountTypesAdded)
	{
		var profile = await PendingMembersInGroup();
		List<UserGroupMember> profileFilter = new();
		foreach (var accountType in accountTypesAdded)
		{

			if (profile.Where(x => x.GroupMembers.Group.TypeAccount == accountType).FirstOrDefault() == null) continue;
			profileFilter.Add(profile.Where(x => x.GroupMembers.Group.TypeAccount == accountType).FirstOrDefault());

		}
		return profileFilter;
	}

	public async Task<string> TotalGroupMembersJoined(AccountType? accountType, string groupName, string userId)
	{

		string? result = default;
		var maxMembersInGroup = await MembersInGroup();
		var groupId = await _accountServices.CollectGroupId(
			await _accountServices.AccountIdForMemeberInGroups(userId, accountType),
			groupName
			);

		var maxMember = maxMembersInGroup.Where(x => x.GroupMembers.GroupId == groupId && x.GroupMembers.Account.Accepted == true).ToList().Count;

		var totalMembersValueInDb = await _accountServices.MaxNumberOfMembersInGroup(groupId);


		if (maxMember != 0 && totalMembersValueInDb != 0)
		{
			if (maxMember >= totalMembersValueInDb)
			{
				groupName = groupName.Replace(" ", "+");
				result = "/Accounts" + "/AcceptedMembersDashboard?" + "AccountType=" + accountType.ToString() + "&" + "GroupName=" + groupName;

			}
		}
		return result;
	}



	public async Task<int> CollectGroupIdInDb(List<int> accountIdOfGroups, string groupName)
	{

		var CollectGroupId = await _accountServices.CollectGroupId(accountIdOfGroups, groupName);
		return CollectGroupId;
	}

	public async Task<List<int>> MembersOfGroup(string userId, AccountType typeAccount)
	{
		List<int> groupusers = new();


		var memberInGroup = await _accountServices.AccountIdForMemeberInGroups(userId, typeAccount);


		for (int i = 0; i < memberInGroup.Count; i++)
		{
			if (memberInGroup[i] != 0)
			{
				groupusers.Add(memberInGroup[i]);
			}
		}
		return groupusers;
	}

	public async Task<(List<int> Paid,
		List<int> NotPaid,
		List<string> UserNames,
		int MembersinDbCount,
		List<int> MemberQueue,
		List<int> MemberNotPaid,
		List<int> MemberPending,
		List<int> MemberPaid)> MembersPaidFilter(int groupId)
	{


		List<int> memberNotPaid = new();
		List<int> memberPending = new();
		List<int> memberPaid = new();


		List<int> memberQueue = new();

		bool platinum = false;
		bool pendingPay = false;
		bool paidDeposit = false;


		//get the count of members in the group
		var totalMembersInDb = await _accountServices.Profile();
		var membersInDb = totalMembersInDb.Where(x => x.GroupMembers.GroupId == groupId && x.GroupMembers.Account.Accepted == true).ToList();
		var membersInDbCount = membersInDb.Count();

		//get who paid the deposit and who did not
		var accountIdInDb = membersInDb.Select(x => x.GroupMembers.AccountId).ToList();

		// add accountIdList by if statement
		List<int> accountIdOfGroups = new();
		List<int> depositNotPaid = new();

		List<string>? userNames = new();
		List<string>? userSecondName = new();

		//use account list to count paid-members, pending members and premium members
		foreach (var member in accountIdInDb)
		{

			// get deposit per member as to decimal (Dec)
			var memberCheckDeposit = await _prepaymentServices.PrepaymentDeposit(member);
			int depositAmountPerMember = Convert.ToInt32(memberCheckDeposit);
			// get expected deposit per member from target amount
			var groupTotalPayoutInDb = await _prepaymentServices.DepositTotal(groupId);
			int amountMemberDeposited = Convert.ToInt32(groupTotalPayoutInDb);

			if (depositAmountPerMember == amountMemberDeposited)
			{

				paidDeposit = await _accountProfileServices.MemberStatusPaidDepo(member);
				if (paidDeposit)
				{
					accountIdOfGroups.Add(member);
					memberPaid.Add(member);
				}
			}
			if (depositAmountPerMember <= amountMemberDeposited)
			{
				pendingPay = await _accountProfileServices.MemberStatusPending(member);
				if (pendingPay)
				{
					accountIdOfGroups.Add(member);
					memberPending.Add(member);
				}

			}
			if (depositAmountPerMember == 0)
			{
				platinum = await _accountProfileServices.MemberStatusPlat(member);
				if (platinum)
				{
					accountIdOfGroups.Add(member);
					memberNotPaid.Add(member);
				}
				else
				{
					depositNotPaid.Add(member);
					userNames.Add(membersInDb.Where(x => x.GroupMembers.AccountId == member).Select(x => x.AccountUser.FirstName).FirstOrDefault().ToString() + " " +
					  membersInDb.Where(x => x.GroupMembers.AccountId == member).Select(x => x.AccountUser.LastName).FirstOrDefault());
				}
			}
		}

		return (accountIdOfGroups, depositNotPaid, userNames, membersInDbCount, memberQueue, memberNotPaid, memberPending, memberPaid);

	}

	// get member turn specific info on payments, deposits and details
	public async Task<List<DisplayMemberTurn>> DisplayMemberTurnProfile()
	{
		var memberProfile = await _accountServices.NextPaidMember();
		memberProfile = memberProfile.Where(x => x.Account.Accepted == true).ToList();
		return memberProfile;
	}
	//displays only members in group that have been accepted by Manager of group
	public async Task<IEnumerable<UserGroupMember>> MembersInGroup()
	{

		var membersInGroup = await _accountServices.Profile();
		membersInGroup = membersInGroup.Where(x => x.Account.Accepted == true).ToList();
		return membersInGroup;
	}

	//displays members yet to be accepted to join the group
	public async Task<IEnumerable<UserGroupMember>> PendingMembersInGroup()
	{
		var membersInGroup = await _accountServices.Profile();
		return membersInGroup;
	}

	public async Task<decimal> DisplayMemberTurnFinance()
	{

		var memberFinace = await _accountServices.NextPaidMember();

		var depositedAmount = memberFinace.Where(x => x.Deposit.DepositReference == "Wallet" && x.Deposit.DepositDate.Month == DateTime.Now.Month).Sum(x => x.Deposit.DepositAmount);

		return depositedAmount;

	}


	public async Task AcceptGroupMember(Account account) => await _accountServices.AcceptGroupMemeber(account);

	// fetch list of AccountId of members in groups
	public async Task<List<int>> AcceptedGroupMembers(int groupId) => await _accountServices.AccptedGroupMembers(groupId);



}
