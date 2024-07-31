using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class WithdrawRepository : IWithdrawRepository
	{
		private readonly ApplicationDbContext _context;

		public WithdrawRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task? Delete(int? id)
		{
			var delInvoicDInDb = await this.Details(id);
			_context.Remove(delInvoicDInDb);
			await _context.SaveChangesAsync();
		}

		public async Task? Edit(WithdrawDetails invoiceDetails)
		{
			_context.Update(invoiceDetails);
			await _context.SaveChangesAsync();
		}

		public async Task<List<WithdrawDetails>> GetAll()
		{
			return await _context.WithdrawDetails.ToListAsync();
		}

		public async Task<WithdrawDetails> Details(int? id)
		{

			return await _context.WithdrawDetails.FirstOrDefaultAsync(x => x.InvoiceId == id);
		}

		public async Task? Insert(WithdrawDetails invoiceDetails)
		{
			await _context.AddAsync(invoiceDetails);
			await _context.SaveChangesAsync();
		}

		public bool InvoiceDetailsExists(int? id)
		{
			return (_context.WithdrawDetails?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
		}

	}
}
