using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;

namespace Stokvel_Groups_Home.Services.AccountServices.AccountRequestService
{
	public class AccountStartStokvelRequestService
	{
		private readonly IAccountServices _accountServices;
		private readonly IGroupsCRUDService _groupsService;
		private readonly IPrepaymentServices _prepaymentServices;
		private readonly IAccountProfileServices _accountProfileServices;

		public AccountStartStokvelRequestService(IAccountServices accountServices, IGroupsCRUDService groupsService, IPrepaymentServices prepaymentServices,
		IAccountProfileServices accountProfileServices)
		{
			_accountServices = accountServices;
			_prepaymentServices = prepaymentServices;
			_accountProfileServices = accountProfileServices;
		}


		public async Task<(List<int> Paid,
	   List<int> NotPaid,
	   List<string> UserNames,
	   int MembersinDbCount,
	   List<int> MemberQueue,
	   List<int> MemberNotPaid,
	   List<int> MemberPending,
	   List<int> MemberPaid)> MemebersPaidFilter(int groupId)
		{


			List<int> memberNotPaid = new();
			List<int> memberPending = new();
			List<int> memberPaid = new();


			List<int> memberQueue = new();

			bool platnum = false;
			bool pendingPay = false;
			bool paidDeposit = false;


			//get the count of members in the group
			var totalMembersinDb = await _accountServices.Profile();
			var membersinDb = totalMembersinDb.Where(x => x.GroupMembers.GroupId == groupId && x.GroupMembers.Account.Accepted == true).ToList();
			var membersinDbCount = membersinDb.Count();

			//get who paid the deposit and who did not
			var accountIdInDb = membersinDb.Select(x => x.GroupMembers.AccountId).ToList();

			// add accountidList by if statement
			List<int> accountIdOfGroups = new();
			List<int> depositNotPaid = new();

			List<string>? userNames = new();
			List<string>? userSecondName = new();

			//use account list to count paid-members, pending members and premium members
			foreach (var member in accountIdInDb)
			{

				// get deposit per member as to decimal (Dec)
				var memberCheckDeposit = await _prepaymentServices.PrepaymentDeposit(member);
				int depositAmoutPerMemebr = Convert.ToInt32(memberCheckDeposit);
				// get expected deposit per memeber from target amaount
				var groupTotalPayoutInDb = await _prepaymentServices.DepositTotal(groupId);
				int amountMemeberDeposited = Convert.ToInt32(groupTotalPayoutInDb);

				if (depositAmoutPerMemebr == amountMemeberDeposited)
				{

					paidDeposit = await _accountProfileServices.MemberStatusPaidDepo(member);
					if (paidDeposit)
					{
						accountIdOfGroups.Add(member);
						memberPaid.Add(member);
					}
				}
				if (depositAmoutPerMemebr <= amountMemeberDeposited)
				{
					pendingPay = await _accountProfileServices.MemberStatusPending(member);
					if (pendingPay)
					{
						accountIdOfGroups.Add(member);
						memberPending.Add(member);
					}

				}
				if (depositAmoutPerMemebr == 0)
				{
					platnum = await _accountProfileServices.MemberStatusPlat(member);
					if (platnum)
					{
						accountIdOfGroups.Add(member);
						memberNotPaid.Add(member);
					}
					else
					{
						depositNotPaid.Add(member);
						userNames.Add(membersinDb.Where(x => x.GroupMembers.AccountId == member).Select(x => x.AccountUser.FirstName).FirstOrDefault().ToString() + " " +
						  membersinDb.Where(x => x.GroupMembers.AccountId == member).Select(x => x.AccountUser.LastName).FirstOrDefault());
					}
				}
			}

			return (accountIdOfGroups, depositNotPaid, userNames, membersinDbCount, memberQueue, memberNotPaid, memberPending, memberPaid);

		}

	}
}
