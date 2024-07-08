using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Models.Tables;

namespace Stokvel_Groups_Home.Repositories
{
	public class DepositRepository : IDepositRepository
	{
		private readonly ApplicationDbContext _context;
		public DepositRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<Deposit>>? GetAll()
		{
			var allDeposits = await _context.Deposits.ToListAsync();
			return allDeposits;

		}

		public async Task<Deposit>? Details(int? id)
		{

			var getDeposits = await _context.Deposits
				.Include(d => d.PaymentStatus)

				.FirstOrDefaultAsync(m => m.DepositId == id);

			return getDeposits;
		}

		public async Task Inset(Deposit? deposits)
		{
			await _context.Deposits.AddAsync(deposits);

		}

		public async Task Edit(Deposit? deposits)
		{
			await _context.Deposits.Update(deposits).ReloadAsync();
		}
		public async Task Delete(int? id)
		{
			var delDepositInDb = await this.Details(id);


			_context.Deposits.Remove(delDepositInDb);

		}

		public async Task<List<DepositSystem>> Profile()
		{

			List<PaymentMethod> paymentMethods = await _context.PaymentMethods.ToListAsync();
			List<PaymentStatus> paymentStatus = await _context.PaymentStatuses.ToListAsync();
			List<Invoice> invoices = await _context.Invoices.ToListAsync();
			List<Prepayment> prepayments = await _context.Prepayments.ToListAsync();
			List<Deposit> deposits = await _context.Deposits.ToListAsync();

			var profile = (from d in deposits
						   join i in invoices on d.InvoiceId equals i.InvoiceId
						   join pm in paymentMethods on d.MethodId equals pm.MethodId
						   join ps in paymentStatus on d.PaymentStatusId equals ps.PaymentStatusId

						   select new DepositSystem
						   {
							   Deposit = d,
							   Invoice = i,
							   PaymentMethod = pm,
							   PaymentStatus = ps,

						   }).ToList();
			return profile;
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public bool DepositExists(int? id)
		{
			return (_context.Deposits?.Any(e => e.DepositId == id)).GetValueOrDefault();
		}




	}
}
