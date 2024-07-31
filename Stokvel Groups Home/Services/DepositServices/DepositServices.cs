using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.DepositServices
{
	public class DepositServices : IDepositServices
	{
		private readonly IPrepaymentsRepository _prepaymentsRepository;
		private readonly IDepositRepository _depositRepository;
		private readonly IInvoicesRepository _invoicesRepository;
		private readonly IPaymentMethodsRepository _paymentMethodsRepository;
		private readonly IPaymentStatusRepository _paymentStatusRepository;
		private readonly IAccountProfileRepository _accountProfileRepository;
		private readonly IMemberInvoiceRepository _memberInvoiceRepository;
		private readonly IGroupsRepository _groupsRepository;
		private readonly IGroupMembersRepository _groupMembersRepository;
		private readonly IAccountsRepository _accountsRepository;
		private readonly IAccountUserRepository _accountUserRepository;
		private readonly IPenaltyFeeRepository _penaltyFeeRepository;
		private readonly IWalletRepository _walletRepository;
		private readonly ApplicationDbContext _context;

		public DepositServices(IPrepaymentsRepository prepaymentsRepository,
			IDepositRepository depositRepository,
			IInvoicesRepository invoicesRepository,
			IPaymentStatusRepository paymentStatusRepository,
			IPaymentMethodsRepository paymentMethodsRepository,
			IAccountProfileRepository accountProfileRepository,
			IMemberInvoiceRepository memberInvoiceRepository,
			IGroupsRepository groupsRepository,
			IGroupMembersRepository groupMembersRepository,
			IAccountsRepository accountsRepository
, IPenaltyFeeRepository penaltyFeeRepository,
			IAccountUserRepository accountUserRepository,
			IWalletRepository walletRepository,
			ApplicationDbContext context

			)

		{
			_prepaymentsRepository = prepaymentsRepository;
			_depositRepository = depositRepository;
			_invoicesRepository = invoicesRepository;
			_paymentStatusRepository = paymentStatusRepository;
			_accountProfileRepository = accountProfileRepository;
			_paymentMethodsRepository = paymentMethodsRepository;
			_memberInvoiceRepository = memberInvoiceRepository;
			_groupsRepository = groupsRepository;
			_groupMembersRepository = groupMembersRepository;
			_accountsRepository = accountsRepository;
			_accountUserRepository = accountUserRepository;
			_penaltyFeeRepository = penaltyFeeRepository;
			_walletRepository = walletRepository;
			_context = context;
		}

		public async Task<AccountProfile> Details(int accountProfileId) => await _accountProfileRepository.Details(accountProfileId);


		public async Task<int> MemebersFirstPreDeposit(int id, int prePaymentId, decimal preDepoAmount, decimal DepositAmount)
		{

            int preDepoId = 0;
			var memberInDb = await _groupMembersRepository.GetAll();
			var typeAccount = memberInDb.Where(x => x.AccountId == id).Select(x => x.Group.TypeAccount).FirstOrDefault();


			PreDeposit prepayment = new()
			{
                PrepaymentType = typeAccount.ToString(),
				PrepaymentDate = DateTime.Now.ToString(),
				Status = true,
				AccountId = id,
			};


			
			if (prePaymentId == 0)
			{
				await _prepaymentsRepository.Inset(prepayment);
				await _prepaymentsRepository.SaveAsync();
                preDepoId = prepayment.PrepaymentId;

				PenaltyFee penaltyFee = new()
				{
					PenaltyDate = DateTime.Now,
					PenaltyAmount = 0,
					PenaltyLevel = "Low",
				};
				prepayment.Amount = Convert.ToInt32(DepositAmount);

                await _penaltyFeeRepository.Insert(penaltyFee);
			}
			else
			{
				prepayment.PrepaymentId = prePaymentId;
				prepayment.Amount = Convert.ToInt32(DepositAmount + preDepoAmount);
                await _prepaymentsRepository.Edit(prepayment);
                await _prepaymentsRepository.SaveAsync();
				preDepoId = prepayment.PrepaymentId;
            }
			return preDepoId;
		}



		// 
		public async Task MemeberDepositMade(int id, string userId, Deposit deposit, int accountProfileId, int membershipRank, decimal totalAmountDeposited, MemberStatuses statusRank)
		{
			DepositMethod paymentMethod = new()
			{
				MethodName = "Card Payment",
				Description = "Done Sucssesful",
			};

			DepositStatus paymentStatus = new()
			{
				PaymentStatusName = "Card Payment",
				PaymentStatesDescription = "Done Sucssesful",
			};

			var amountDeposited = await this.MonthlyDeposit(id, deposit.DepositAmount,0);

			Invoice depositToInvoice = new()
			{
				InvoiceDate = DateTime.Now,
				TotalAmount = amountDeposited,
				Discription = paymentMethod.MethodName,

			};

			//Save to Invoice
			await _invoicesRepository.Inset(depositToInvoice);
			await _invoicesRepository.SaveAsync();

			//
			var invoiceId = depositToInvoice.InvoiceId;

			// 
			await _invoicesRepository.InsetDeposit(id, invoiceId);
			await _paymentMethodsRepository.Inset(paymentMethod);
			await _paymentMethodsRepository.SaveAsync();
			await _paymentStatusRepository.Inset(paymentStatus);
			await _paymentStatusRepository.SaveAsync();


			deposit.InvoiceId = depositToInvoice.InvoiceId;
			deposit.PaymentStatusId = paymentStatus.PaymentStatusId;
			deposit.MethodId = paymentMethod.MethodId;
			deposit.DepositDate = DateTime.Now;
			deposit.InvoiceId.Equals(deposit.InvoiceId);
			deposit.DepositAmount = amountDeposited;

			await _depositRepository.Inset(deposit);
			await _depositRepository.SaveAsync();


			var rank = statusRank;
			var pointCount = amountDeposited / 100;
			decimal dtot = amountDeposited;


			AccountProfile accountProfiles = new();


			if (accountProfileId > 0)
			{
				accountProfiles.AccountProfileId = accountProfileId;
				accountProfiles.Id = userId;
				accountProfiles.StatusRank = rank;
				accountProfiles.MembershipRank = membershipRank + Convert.ToInt32(pointCount);
				accountProfiles.TotalAmoutDeposited = totalAmountDeposited + dtot;

				if (statusRank != MemberStatuses.PendingPayment)
				{
					var memberSataus = await this.StatusClassCalculation(membershipRank);
					accountProfiles.StatusRank = memberSataus;
				}
				await _accountProfileRepository.Edit(accountProfiles);
				await _accountProfileRepository.SaveAsync();
			}
			else
			{
				accountProfiles.Id = userId;
				accountProfiles.MembershipRank = Convert.ToInt32(pointCount);
				accountProfiles.TotalAmoutDeposited = dtot;
				await _accountProfileRepository.Inset(accountProfiles);
				await _accountProfileRepository.SaveAsync();
			}
		}

		public async Task Update(AccountProfile accountProfile)
		{
			if (accountProfile.AccountProfileId > 0)
			{
				await _accountProfileRepository.Edit(accountProfile);
				await _accountProfileRepository.SaveAsync();
			}
			else
			{
				await _accountProfileRepository.Inset(accountProfile);
				await _accountProfileRepository.SaveAsync();
			}
		}

		public async Task<decimal> MonthlyDeposit(int accountId, decimal deposit, decimal preDepoAmount)
		{
			List<Account> accounts = await _accountsRepository.GetAll();
			List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();
			List<Group> groups = await _groupsRepository.GetAll();
			List<Deposit> depositList = await _depositRepository.GetAll();

			var groupMember = (from a in accounts.Where(x => x.AccountId == accountId && x.Accepted == true).ToList()
							   join gm in groupMembers on a.AccountId equals gm.AccountId into table1
							   from gm in table1.ToList()
							   join g in groups on gm.GroupId equals g.GroupId into table2
							   from g in table2.ToList()
							   select new
							   {
								   g.TotalGroupMembers,
								   g.AccountTarget
							   }).FirstOrDefault();

			var numOfMembers = groupMember.TotalGroupMembers-1;
			decimal members = Convert.ToDecimal(numOfMembers);
			var totalDepositCal = groupMember.AccountTarget;

			var totalPreDepo = totalDepositCal / (numOfMembers);

			var limitDeposit = (totalDepositCal / numOfMembers - preDepoAmount);


			List<MemberInvoice> memberInvoices = await _memberInvoiceRepository.GetAll();
			List<Invoice> invoices = await _invoicesRepository.GetAll();

			var depositMonthlyTotal = (from a in accounts.Where(x => x.AccountId == accountId && x.Accepted == true).ToList()
									   join mi in memberInvoices on a.AccountId equals mi.AccountId into table1
									   from mi in table1.ToList()
									   join i in invoices on mi.InvoceId equals i.InvoiceId into table2
									   from i in table2.ToList()
									   join d in depositList.Where(x => x.DepositDate.Month == DateTime.Now.Month) on i.InvoiceId equals d.InvoiceId into table3
									   from d in table3.ToList()
									   select new
									   {
										   d.DepositAmount
									   }).ToList();

			DepositMethod paymentMethod = new()
			{
				MethodName = "wallet",
				Description = "Done Sucssesful",
			};

			DepositStatus paymentStatus = new()
			{
				PaymentStatusName = "wallet",
				PaymentStatesDescription = "Done Sucssesful",
			};

			// get the total amount of money paid this month
			var totalPaid = depositMonthlyTotal.Sum(x => x.DepositAmount);
			decimal roundedLimitDeposit = Math.Round(limitDeposit, 0);


			if (deposit > limitDeposit || limitDeposit < 0)
			{
				var excess = deposit - limitDeposit;


				Invoice depositToInvoice = new()
				{
					InvoiceDate = DateTime.Now,
					TotalAmount = excess,
					Discription = "Wallet",

				};

				//send leftover to invoice
				//Save to Invoice
				await _invoicesRepository.Inset(depositToInvoice);
				await _invoicesRepository.SaveAsync();

				// 
				await _paymentMethodsRepository.Inset(paymentMethod);
				await _paymentMethodsRepository.SaveAsync();
				await _paymentStatusRepository.Inset(paymentStatus);
				await _paymentStatusRepository.SaveAsync();

				Deposit deposits = new()
				{
					InvoiceId = depositToInvoice.InvoiceId,
					PaymentStatusId = paymentStatus.PaymentStatusId,
					MethodId = paymentMethod.MethodId,
					DepositDate = DateTime.Now,
					DepositReference = paymentStatus.PaymentStatusName,
					DepositAmount = excess
				};

				await _depositRepository.Inset(deposits);
				await _depositRepository.SaveAsync();

				return limitDeposit;
			}

			return (deposit);
		}

		public async Task<MemberStatuses> StatusClassCalculation(int membershipRank)
		{
			MemberStatuses memberStatuses = new();
			switch (membershipRank)
			{
				case <= 60:
					memberStatuses = MemberStatuses.Bronz;
					break;
				case <= 120:
					memberStatuses = MemberStatuses.Silver;
					break;
				case <= 240:
					memberStatuses = MemberStatuses.Gold;
					break;
				case <= 300:
					memberStatuses = MemberStatuses.Platnum;
					break;


			}

			return memberStatuses;
		}
		
		public	async Task<List<PreDeposit>> PreDeposit(int accountId)
		{

			var members = await _prepaymentsRepository.GetAll();
			var memberDepositList = members.Where(x => x.AccountId == accountId).ToList();

			return memberDepositList;
        }
        public IQueryable<PreDeposit> GetAll()
        {
			return _context.PreDeposit.Include(a => a.AccountId);
}

        public async Task<PreDeposit> GetAnomaly(int accountId)
        {
            var anomaly = await GetAll()
                .FirstOrDefaultAsync(a => a.AccountId == accountId);
            return anomaly;
        }
    }
}
