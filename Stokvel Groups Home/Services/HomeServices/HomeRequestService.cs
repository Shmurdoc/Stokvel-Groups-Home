using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.HomeService
{
	public class HomeRequestService : IHomeRequestService
	{

		private readonly IAccountServices _accountServices;
		private readonly IWithdrawServices _withdrawServices;
		private readonly IHomeServices _homeServices;
		private readonly IPrepaymentServices _prepaymentServices;


		public HomeRequestService(IAccountServices accountServices, IWithdrawServices withdrawServices, IHomeServices homeServices, IPrepaymentServices prepaymentServices)
		{
			_withdrawServices = withdrawServices;
			_accountServices = accountServices;
			_homeServices = homeServices;
			_prepaymentServices = prepaymentServices;

		}




		public async Task<int> NumberOfAccounts(string id) => await _homeServices.NumberOfAccounts(id);
		public async Task<List<decimal>> TotalOwed(string id) => await _homeServices.TotalOwed(id);



		public async Task<List<DateOnly>> DateToNextDeposit(string id)
		{
			List<DateOnly> dateToNextDeposit = new();
			var memberInDb = await _accountServices.NextPaidMember();
			var accountProfile = await _accountServices.Profile();


			var accountId = memberInDb.Where(x => x.AccountUser.Id == id).Select(x => x.Account.AccountId).ToList();



			foreach (var account in accountId)
			{
				var monthlyDepositedInDb = memberInDb.Where(x => x.Deposit.DepositDate.Month == DateTime.Now.Month).Sum(x => x.Deposit.DepositAmount);
				var groupId = memberInDb.Select(x => x.GroupMembers.GroupId).FirstOrDefault();
				var monthlyTargetAmount = accountProfile.Where(x => x.Group.GroupId == groupId).Select(x => x.Group.AccountTarget).FirstOrDefault();
				var totalMonthlyDeposit = monthlyTargetAmount / Convert.ToDecimal(memberInDb.Select(x => x.GroupMembers.Group.TotalGroupMembers).FirstOrDefault());
				if (monthlyDepositedInDb == totalMonthlyDeposit || monthlyDepositedInDb > totalMonthlyDeposit)
				{
					var date = memberInDb.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month + 1).Select(x => x.Account.AccountQueueStart).FirstOrDefault();
					var dateToNext = DateOnly.FromDateTime(date);
					dateToNextDeposit.Add(dateToNext);
				}
				else
				{
					var date = memberInDb.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month).Select(x => x.Account.AccountQueueStart).FirstOrDefault();
					var dateToNext = DateOnly.FromDateTime(date);
					dateToNextDeposit.Add(dateToNext);
				}
			}
			return dateToNextDeposit;
		}

		public async Task<List<decimal>> TotalAmountDue(string id)
		{
			List<decimal> listDuePerGroup = new();

			var memberInDb = await _accountServices.NextPaidMember();

			var accountId = memberInDb.Where(x => x.AccountUser.Id == id).Select(x => x.Account.AccountId).ToList();

			var depositReference = memberInDb.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month).Select(x => x.AccountUser.FirstName).FirstOrDefault();

			foreach (var memberId in accountId)
			{
				var memberInitDeposit = await _prepaymentServices.PrepaymentDeposit(memberId);
				var memberInList = memberInDb.Where(x => x.Account.AccountId == memberId).ToList();
				var amountDue = memberInList.Where(x => x.Deposit.DepositDate.Month == DateTime.Now.Month && x.Deposit.DepositReference == depositReference).Sum(x => x.Deposit.DepositAmount);
				var dueAmount = memberInList.Select(x => x.GroupMembers.Group.AccountTarget).FirstOrDefault();
				var depPerMonth = dueAmount / Convert.ToDecimal(memberInList.Select(x => x.GroupMembers.Group.TotalGroupMembers).FirstOrDefault() - 1);
				if (memberInDb.Any(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month))
				{
					listDuePerGroup.Add(depPerMonth - amountDue);
				}
				else
				{
					listDuePerGroup.Add(0);
				}
			}
			return listDuePerGroup;
		}

		//Paid members
		public async Task<List<DisplayPaidMember>> CreditedMembers(List<int> groupId)
		{
			//get list of all in database
			var membersInGroup = await _withdrawServices.PaidMember();

			var memebersList = membersInGroup.Where(x => x.GroupMembers.GroupId == groupId[1] && x.GroupMembers.GroupId == groupId[2])
				.Where(x => x.InvoiceDetails.CreditAmount > 0).ToList();

			return memebersList;
		}


		//get groupId of member accounts from home service with withdrawal
		public async Task<List<int>> MemberAccountGroupIdWithdrawal(string userId)
		{
			//get list of all in database
			var displayPaidMember = await _withdrawServices.PaidMember();

			var result = displayPaidMember.Where(x => x.AccountUser.Id == userId).Select(x => x.GroupMembers.GroupId).ToList();

			return result;
		}

		//get groupId of member accounts from account service no withdrawal
		public async Task<List<int>> MemberAccountGroupId(string userId)
		{
			//get list of all in database
			var displayPaidMember = await _accountServices.NextPaidMember();

			var result = displayPaidMember.Where(x => x.AccountUser.Id == userId).Select(x => x.GroupMembers.GroupId).Distinct().ToList();
			return result;
		}

		// Daily Updates  
		public async Task<IEnumerable<DisplayMemberTurn>> MemberQueueList()
		{
			//get list of all in database
			var membersInDb1 = await _accountServices.NextPaidMember();

			membersInDb1 = membersInDb1.OrderBy(x => x.GroupMembers.Account.AccountQueue).ToList();
			return membersInDb1;
		}

		public async Task<IEnumerable<DisplayMemberTurn>> DisplayRecentDeposit()
		{
			//get list of all in database
			var memberInDb2 = await _accountServices.NextPaidMember();

			memberInDb2 = memberInDb2.Where(x => x.Deposit.DepositDate.Day >= DateTime.Now.Day - 5).ToList();

			return memberInDb2;
		}




	}
}
