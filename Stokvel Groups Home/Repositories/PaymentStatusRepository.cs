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

		public async Task<List<PaymentStatus>>? GetAll()
		{
			var allInvoices = await _context.PaymentStatuses.ToListAsync();
			return allInvoices;

		}

		public List<SelectListItem>? PaymentStatusExtendInclude()
		{
			var extend = new SelectList(_context.PaymentStatuses, "PaymentStatusId", "PaymentStatusId").ToList();
			return extend;
		}

		public async Task<PaymentStatus>? Details(int? id)
		{
			return await _context.PaymentStatuses.FirstOrDefaultAsync(m => m.PaymentStatusId == id);
		}

		public async Task? Inset(PaymentStatus? paymentStatus)
		{
			await _context.PaymentStatuses.AddAsync(paymentStatus);
		}

		public async Task? Edit(PaymentStatus? paymentStatus)
		{
			_context.Update(paymentStatus);
		}
		public async Task? Delete(int? id)
		{
			var prepayment = await this.Details(id);
			_context.PaymentStatuses.Remove(prepayment);

		}

		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}



		public bool PaymentStatusesExists(int? id)
		{
			return (_context.PaymentStatuses?.Any(e => e.PaymentStatusId == id)).GetValueOrDefault();
		}



	}
}
