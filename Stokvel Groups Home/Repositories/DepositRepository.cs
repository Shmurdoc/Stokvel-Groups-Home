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
