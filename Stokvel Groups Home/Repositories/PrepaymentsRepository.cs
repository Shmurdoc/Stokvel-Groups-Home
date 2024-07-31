using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class PrepaymentsRepository : IPrepaymentsRepository
	{

		private readonly ApplicationDbContext _context;

		public PrepaymentsRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task Delete(int? id)
		{
			var delPrepayInDB = await this.Detail(id);

			_context.PreDeposit.Remove(delPrepayInDB);

		}

		public async Task<PreDeposit> Detail(int? id)
		{
			return await _context.PreDeposit.FirstOrDefaultAsync(m => m.PrepaymentId == id);
		}

		public async Task<PreDeposit>? GetById(int? id)
		{
			return await _context.PreDeposit.FirstOrDefaultAsync(m => m.AccountId == id);
		}

		public async Task Edit(PreDeposit? prepayment)
		{
            _context.PreDeposit.Update(prepayment);
		}

		public async Task<List<PreDeposit>>? GetAll()
		{
			var allPrepayment = await _context.PreDeposit.ToListAsync();
			return allPrepayment;
		}

		public async Task Inset(PreDeposit? prepayment)
		{
			await _context.PreDeposit.AddAsync(prepayment);
		}


		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}


		public bool PrepaymentExists(int? id)
		{
			return (_context.PreDeposit?.Any(e => e.PrepaymentId == id)).GetValueOrDefault();
		}


	}
}
