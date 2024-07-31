using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WithdrawServices;

public class WithdrawServices : IWithdrawServices
{
	private readonly IAccountsRepository _accountsRepository;
	private readonly IGroupMembersRepository _groupMembersRepository;
	private readonly IAccountUserRepository _accountUserRepository;
	private readonly IDepositRepository _depositRepository;
	private readonly IPenaltyFeeRepository _penaltyFeeRepository;
	private readonly IInvoicesRepository _invoicesRepository;
	private readonly IMemberInvoiceRepository _memberInvoiceRepository;
	private readonly IWithdrawRepository _withdrawRepository;


	public WithdrawServices(IAccountsRepository accountsRepository, IAccountUserRepository accountUserRepository, IGroupMembersRepository groupMembersRepository,
			IDepositRepository depositRepository, IPenaltyFeeRepository penaltyFeeRepository, IInvoicesRepository invoicesRepository, IMemberInvoiceRepository memberInvoiceRepository,
			IWithdrawRepository withdrawRepository)
	{
		_groupMembersRepository = groupMembersRepository;
		_accountUserRepository = accountUserRepository;
		_accountsRepository = accountsRepository;
		_depositRepository = depositRepository;
		_penaltyFeeRepository = penaltyFeeRepository;
		_memberInvoiceRepository = memberInvoiceRepository;
		_withdrawRepository = withdrawRepository;
		_invoicesRepository = invoicesRepository;

	}

	// For home page only. Paid members
	public async Task<List<DisplayPaidMember>> PaidMember()
	{

		List<AccountUser> accountUsers = await _accountUserRepository.GetAll();
		List<Account> accounts = await _accountsRepository.GetAll();
		List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();
		List<MemberInvoice> memberInvoices = await _memberInvoiceRepository.GetAll();
		List<Invoice> invoices = await _invoicesRepository.GetAll();
		List<Deposit> depositList = await _depositRepository.GetAll();
		List<WithdrawDetails> invoiceDetails = await _withdrawRepository.GetAll();
		List<PenaltyFee> penaltyFees = await _penaltyFeeRepository.GetAll();


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
						   join w in invoiceDetails on d.InvoiceId equals w.InvoiceId into table5
						   from w in table5.ToList()
						   join pf in penaltyFees on w.PenaltyFeeId equals pf.PenaltyFeeId into table6
						   from pf in table6.ToList()

						   select new DisplayPaidMember
						   {
							   AccountUser = au,
							   Account = a,
							   GroupMembers = gm,
							   MemberInvoice = mi,
							   Invoice = i,
							   Deposit = d,
							   InvoiceDetails = w,
							   PenaltyFee = pf
						   }).ToList();


		return groupMember;
	}




	public async Task<int> CreditAccount(WithdrawDetails invoiceDetails)
	{
		Invoice CreditFromInvoice = new()
		{
			InvoiceDate = DateTime.Now,
			TotalAmount = invoiceDetails.CreditAmount,
			Discription = "Deposit To Wallet",
		};

		//Save to Invoice
		await _invoicesRepository.Inset(CreditFromInvoice);
		await _invoicesRepository.SaveAsync();

		//credit a members account
		await _withdrawRepository.Insert(invoiceDetails);
		var withdrawId = invoiceDetails.DetailedId;

		return withdrawId;
	}


	public async Task<int> groupId(int accountId)
	{
		List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();

		var groupId = groupMembers.Where(x => x.AccountId == accountId).Select(x => x.GroupId).FirstOrDefault();
		return groupId;
	}



}
