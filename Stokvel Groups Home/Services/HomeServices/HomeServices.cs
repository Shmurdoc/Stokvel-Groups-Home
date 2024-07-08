using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;

namespace Stokvel_Groups_Home.Services.HomeService
{
	public class HomeServices : IHomeServices
	{

		private readonly IAccountsRepository _accountsRepository;
		private readonly IGroupMembersRepository _groupMembersRepository;
		private readonly IAccountUserRepository _accountUserRepository;
		private readonly IDepositRepository _depositRepository;
		private readonly IGroupsRepository _groupsRepository;
		private readonly IInvoicesRepository _invoicesRepository;
		private readonly IMemberInvoiceRepository _memberInvoiceRepository;
		private readonly IWithdrawRepository _invoiceDetailsRepository;

		public HomeServices(IAccountsRepository accountsRepository, IAccountUserRepository accountUserRepository, IGroupMembersRepository groupMembersRepository,
			IDepositRepository depositRepository, IGroupsRepository groupsRepository, IInvoicesRepository invoicesRepository, IMemberInvoiceRepository memberInvoiceRepository,
			IWithdrawRepository invoiceDetailsRepository)
		{
			_groupMembersRepository = groupMembersRepository;
			_accountUserRepository = accountUserRepository;
			_accountsRepository = accountsRepository;
			_depositRepository = depositRepository;
			_groupsRepository = groupsRepository;
			_memberInvoiceRepository = memberInvoiceRepository;
			_invoiceDetailsRepository = invoiceDetailsRepository;
			_invoicesRepository = invoicesRepository;

		}




		public async Task<int> NumberOfAccounts(string? id)
		{
			List<int> numberOfAccounts = new();

			var account = await _accountsRepository.GetAll();
			var accountId = account.Where(x => x.AccountQueue > 0 && x.Id == id && x.Accepted == true).Select(x => x.AccountId).ToList();
			return accountId.Count;
		}


		// total amount owed by member for this month
		public async Task<List<decimal>> TotalOwed(string? id)
		{
			var accountUser = await _accountUserRepository.GetAll();
			var memberInvoice = await _memberInvoiceRepository.GetAll();
			var deposit = await _depositRepository.GetAll();
			var groupDeposit = await _groupsRepository.GetAll();
			var groupMember = await _groupMembersRepository.GetAll();
			var account = await _accountsRepository.GetAll();
			List<decimal> listDuePerGroup = new();
			decimal invoiceAmountList = 0;

			var accountId = account.Where(x => x.Id == id && x.Accepted == true).Select(x => x.AccountId).ToList();


			foreach (var memberId in accountId)
			{

				var invoiceId = memberInvoice.Where(x => x.AccountId == memberId).Select(x => x.InvoceId).ToList();
				var groupId = groupMember.Where(x => x.AccountId == memberId).Select(x => x.GroupId).FirstOrDefault();
				foreach (var membe in invoiceId)
				{
					var amountDue = deposit.Where(x => x.InvoiceId == membe && x.DepositReference != "deposit" && x.DepositReference != "wallet").Select(x => x.DepositAmount).FirstOrDefault();
					if (amountDue == 0) continue;
					invoiceAmountList += amountDue;
				}

				var dueAmount = groupDeposit.Where(x => x.GroupId == groupId).Select(x => x.AccountTarget).FirstOrDefault();

				listDuePerGroup.Add((dueAmount) - invoiceAmountList);
			}
			return listDuePerGroup;
		}




	}
}
