using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class PaymentStatusRepository : IPaymentStatusRepository
	{

		private readonly ApplicationDbContext _context;

		public PaymentStatusRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<DepositStatus>>? GetAll()
		{
			var allInvoices = await _context.DepositStatus.ToListAsync();
			return allInvoices;

		}

		public List<SelectListItem>? PaymentStatusExtendInclude()
		{
			var extend = new SelectList(_context.DepositStatus, "PaymentStatusId", "PaymentStatusId").ToList();
			return extend;
		}

		public async Task<DepositStatus>? Details(int? id)
		{
			return await _context.DepositStatus.FirstOrDefaultAsync(m => m.PaymentStatusId == id);
		}

		public async Task? Inset(DepositStatus? paymentStatus)
		{
			await _context.DepositStatus.AddAsync(paymentStatus);
		}

		public async Task? Edit(DepositStatus? paymentStatus)
		{
			_context.Update(paymentStatus);
		}
		public async Task? Delete(int? id)
		{
			var prepayment = await this.Details(id);
			_context.DepositStatus.Remove(prepayment);

		}

		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}



		public bool PaymentStatusesExists(int? id)
		{
			return (_context.DepositStatus?.Any(e => e.PaymentStatusId == id)).GetValueOrDefault();
		}



	}
}
