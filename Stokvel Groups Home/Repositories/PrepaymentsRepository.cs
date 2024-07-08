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

			_context.Prepayments.Remove(delPrepayInDB);

		}

		public async Task<Prepayment> Detail(int? id)
		{
			return await _context.Prepayments.FirstOrDefaultAsync(m => m.PrepaymentId == id);
		}

		public async Task<Prepayment>? GetById(int? id)
		{
			return await _context.Prepayments.FirstOrDefaultAsync(m => m.AccountId == id);
		}

		public async Task Edit(Prepayment? prepayment)
		{
			_context.Prepayments.Update(prepayment);
		}

		public async Task<List<Prepayment>>? GetAll()
		{
			var allPrepayment = await _context.Prepayments.ToListAsync();
			return allPrepayment;
		}

		public async Task Inset(Prepayment? prepayment)
		{
			await _context.Prepayments.AddAsync(prepayment);
		}


		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}


		public bool PrepaymentExists(int? id)
		{
			return (_context.Prepayments?.Any(e => e.PrepaymentId == id)).GetValueOrDefault();
		}


	}
}
