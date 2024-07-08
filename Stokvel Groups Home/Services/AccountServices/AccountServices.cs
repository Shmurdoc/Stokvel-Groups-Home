using Microsoft.VisualBasic;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountServices;

public class AccountServices : IAccountServices
{

	private readonly IAccountsRepository _accountsRepository;
	private readonly IGroupsRepository _groupsRepository;
	private readonly IGroupMembersRepository _groupMembersRepository;
	private readonly IAccountUserRepository _accountUserRepository;
	private readonly IAccountProfileRepository _accountProfileRepository;
	private readonly IDepositRepository _depositRepository;
	private readonly IInvoicesRepository _invoicesRepository;
	private readonly IMemberInvoiceRepository _memberInvoiceRepository;
	private readonly IWithdrawRepository _invoiceDetailsRepository;
	public AccountServices(IAccountsRepository accountsRepository,
		IGroupsRepository groupsRepository,
		IGroupMembersRepository groupMembersRepository,
		IAccountUserRepository accountUserRepository,
		IAccountProfileRepository accountProfileRepository,
		IDepositRepository depositRepository,
		IMemberInvoiceRepository memberInvoiceRepository,
		IInvoicesRepository invoicesRepository,
		IWithdrawRepository invoiceDetailsRepository
		)
	{
		_accountsRepository = accountsRepository;
		_groupsRepository = groupsRepository;
		_groupMembersRepository = groupMembersRepository;
		_accountUserRepository = accountUserRepository;
		_accountProfileRepository = accountProfileRepository;
		_depositRepository = depositRepository;
		_memberInvoiceRepository = memberInvoiceRepository;
		_invoicesRepository = invoicesRepository;
		_invoiceDetailsRepository = invoiceDetailsRepository;

	}

	public async Task<bool> GroupNamesExists(string? id, AccountType typeAccont)
	{
		List<GroupMembers>? groupMembers = await _groupMembersRepository.GetAll();
		List<Group>? groups = await _groupsRepository.GetAll();
		List<Account> accounts = await _accountsRepository.GetAll();
		List<bool>? DecisionMaker = new();
		int accountidvalue = 0;
		bool idverify = false;

		var accountid = accounts.Where(x => x.Id == id).Select(x => x.AccountId).ToList();
		//Use forloop to filter by AccountId
		for (int i = 0; i < accountid.Count; i++)
		{
			var groupid = groupMembers.Where(x => x.Account.AccountId == accountid[i]).Select(x => x.GroupId).ToList();

			foreach (var item in groupid)
			{
				// create list of bool for each GroupId values

				accountidvalue = item;

				// filter to find bool of GroupId
				var names = groups.Any(x => x.GroupId == accountidvalue && x.TypeAccount == typeAccont);

				// Add bool to list
				DecisionMaker.Add(names);
			}
		}
		idverify = this.FilterAccount(DecisionMaker);

		return idverify;
	}



	public bool FilterAccount(List<bool> decisionMaker)
	{
		// use linq to find if all are true or any are true
		var result = decisionMaker.Any(a => a.Equals(true));

		return result;
	}

	public async Task<IEnumerable<UserGroupMember>> Profile()
	{

		List<GroupMembers>? groupMembers = await _groupMembersRepository.GetAll();
		List<AccountUser>? accountUsers = await _accountUserRepository.GetAll();
		List<Account> accounts = await _accountsRepository.GetAll();
		List<Group> groups = await _groupsRepository.GetAll();

		var profile = (from au in accountUsers
					   join a in accounts on au.Id equals a.Id into table1
					   from a in table1.ToList()
					   join gm in groupMembers on a.AccountId equals gm.AccountId into table2
					   from gm in table2.ToList()
					   join g in groups on gm.GroupId equals g.GroupId into table3
					   from g in table3.ToList()

					   select new UserGroupMember
					   {
						   AccountUser = au,
						   Account = a,
						   GroupMembers = gm,
						   Group = g
					   }).ToList();
		return profile;
	}
	public async Task<int> CollectGroupId(List<int> accountIdOfGroups, string groupName)
	{
		List<Account> accounts = await _accountsRepository.GetAll();
		List<Group> groups = await _groupsRepository.GetAll();

		int GroupMemberIdList = new();
		foreach (var memberID in accountIdOfGroups)
		{
			var getGroupVerifykey = accounts.Where(x => x.AccountId == memberID).Select(x => x.GroupVerifyKey).FirstOrDefault();

			var groupid = groups.Where(x => x.VerifyKey == getGroupVerifykey && x.GroupName == groupName).Select(x => x.GroupId).FirstOrDefault();
			if (groupid == 0) continue;
			GroupMemberIdList = groupid;
		}
		return GroupMemberIdList;
	}

	//generates a list of accepted members in a group
	public async Task<List<int>> AccptedGroupMembers(int groupId)
	{
		List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();

		var accountId = groupMembers.Where(x => x.GroupId == groupId).Select(x => x.AccountId).ToList();

		return accountId;
	}

	// generates the whole list of account Ids in a group
	public async Task<List<int>> AccountIdForMemeberInGroups(string userId, AccountType? typeAccount)
	{
		List<int>? member = new();
		List<int>? accountidlist = new();
		List<int>? accountId = new();

		List<Group>? groups = await _groupsRepository.GetAll();
		List<Account>? accounts = await _accountsRepository.GetAll();
		List<GroupMembers>? groupMembers = await _groupMembersRepository.GetAll();

		accountId = accounts.Where(x => x.Id == userId).Select(x => x.AccountId).ToList();

		foreach (var item in accountId)
		{
			var filterAccountId = accounts.Where(x => x.AccountId == item).Select(x => x.GroupVerifyKey).FirstOrDefault();
			var finalGroupId = groups.Where(x => x.VerifyKey == filterAccountId && x.TypeAccount.ToString() == typeAccount.ToString()).Select(x => x.GroupId).FirstOrDefault();
			var listAccountId = groupMembers.Where(x => x.GroupId == finalGroupId).Select(x => x.AccountId).FirstOrDefault();
			if (listAccountId != 0)
			{
				accountidlist.Add(listAccountId);
			}
		}
		return accountidlist;
	}


	//collects the maximum number of members in a single group
	public async Task<int> MaxNumberOfMembersInGroup(int groupId)
	{

		var group = await _groupsRepository.GetAll();

		var memeberLimitInGroup = group.Where(x => x.GroupId == groupId).Select(x => x.TotalGroupMembers).FirstOrDefault();
		return memeberLimitInGroup;
	}


	public async Task<int> AccountProfileId(string id)
	{
		var accountProfile = await _accountProfileRepository.GetAll();

		var accountProfileId = accountProfile.Where(x => x.Id == id).Select(x => x.AccountProfileId).FirstOrDefault();

		return accountProfileId;

	}

	public async Task StokvelActive(int groupId)
	{
		var groups = await _groupsRepository.GetById(groupId);

		groups.Active = true;

		await _groupsRepository.Edit(groups);
		await _groupsRepository.SaveAsync();
	}

	public async Task<List<int>> AccountId(string userId)
	{
		List<Account>? accounts = await _accountsRepository.GetAll();
		var accountIdInDb = accounts.Where(x => x.Id == userId && x.Accepted == true).Select(x => x.AccountId).ToList();
		return accountIdInDb;
	}


	public async Task<List<DisplayMemberTurn>> NextPaidMember()
	{

		List<AccountUser> accountUsers = await _accountUserRepository.GetAll();
		List<Account> accounts = await _accountsRepository.GetAll();
		List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();
		List<MemberInvoice> memberInvoices = await _memberInvoiceRepository.GetAll();
		List<Invoice> invoices = await _invoicesRepository.GetAll();
		List<Deposit> depositList = await _depositRepository.GetAll();
		List<InvoiceDetails> invoiceDetails = await _invoiceDetailsRepository.GetAll();


		var groupMember = (from au in accountUsers
						   join a in accounts.Where(x => x.Accepted == true) on au.Id equals a.Id into table
						   from a in table.ToList()
						   join gm in groupMembers on a.AccountId equals gm.AccountId into table1
						   from gm in table1.ToList()
						   join mi in memberInvoices on a.AccountId equals mi.AccountId into table2
						   from mi in table2.ToList()
						   join i in invoices on mi.InvoceId equals i.InvoiceId into table3
						   from i in table3.ToList()
						   join d in depositList on i.InvoiceId equals d.InvoiceId into table4
						   from d in table4.ToList()
						   select new DisplayMemberTurn
						   {
							   AccountUser = au,
							   Account = a,
							   GroupMembers = gm,
							   MemberInvoice = mi,
							   Invoice = i,
							   Deposit = d,
						   }).ToList();


		return groupMember;
	}

	// assign users date time and array
	public async Task StartStokvel(List<int> AccountId)
	{
		// Get Each Member In Queue from AccountId
		List<Account> accounts = await _accountsRepository.GetAll();



		for (int i = 0; i < AccountId.Count; i++)
		{
			var account = accounts.Where(x => x.AccountId == AccountId[i]).FirstOrDefault();

			account.AccountQueue = i + 1;
			account.AccountQueueStart = await this.GenerateDateStart(AccountId[i], i);
			account.AccontQueueEnd = await this.GenerateDateEnd(AccountId[i], i);

			await _accountsRepository.Edit(account);
			await _accountsRepository.SaveAsync();
			;

		}



	}

	public async Task<DateTime> GenerateDateStart(int numberOfMembers, int QueueNumber)
	{

		// DateTime Date
		_ = new DateTime();

		//must Add Select case in Future to see when group can start
		//For now we start every next month on the first for ever group

		// Generate logic of when date starts
		DateTime someDate = new(year: DateAndTime.Now.Year, month: DateAndTime.Now.Month + QueueNumber + 1, 1);

		DateTime Dates = someDate;

		return Dates;
	}

	public async Task<DateTime> GenerateDateEnd(int numberOfMembers, int QueueNumber)
	{

		// DateTime Date
		_ = new DateTime();

		//must Add Select case in Future to see when group can start
		//For now we start every next month on the first for ever group

		// Generate logic of when date starts
		DateTime someDate = new(year: DateAndTime.Now.Year, month: DateAndTime.Now.Month + QueueNumber + 1, 25);

		DateTime Dates = someDate;

		return Dates;
	}

	public async Task<List<int>> MembersOfGroup(string userId, AccountType viewTypeAccount)
	{
		List<int> groupusers = new();


		var AccountIdMemeberInGroup = viewTypeAccount;
		var memberInGroup = await this.AccountIdForMemeberInGroups(userId, AccountIdMemeberInGroup);


		for (int i = 0; i < memberInGroup.Count; i++)
		{
			if (memberInGroup[i] != 0)
			{
				groupusers.Add(memberInGroup[i]);
			}
		}
		return groupusers;
	}
	public async Task AcceptGroupMemeber(Account account)
	{
		await _accountsRepository.Edit(account);
		await _accountsRepository.SaveAsync();
	}

}
