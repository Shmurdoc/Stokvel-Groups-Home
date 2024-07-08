using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class PaymentMethodsRepository : IPaymentMethodsRepository
	{

		private readonly ApplicationDbContext _context;

		public PaymentMethodsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<List<PaymentMethod>>? GetAll()
		{
			var allInvoices = await _context.PaymentMethods.ToListAsync();
			return allInvoices;

		}

		public List<SelectListItem> PaymentMethodExtendInclude()
		{
			var FatchExtendedData = new SelectList(_context.PaymentMethods, "MethodId", "MethodId").ToList();
			return FatchExtendedData;
		}

		public async Task<PaymentMethod>? Details(int? id)
		{
			return await _context.PaymentMethods.FirstOrDefaultAsync(m => m.MethodId == id);
		}

		public async Task Inset(PaymentMethod? paymentMethod)
		{
			await _context.PaymentMethods.AddAsync(paymentMethod);
		}

		public async Task Edit(PaymentMethod? paymentMethod)
		{
			_context.Update(paymentMethod);
		}

		public async Task Delete(int? id)
		{
			var paymentMethod = await this.Details(id);

			_context.PaymentMethods.Remove(paymentMethod);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public bool PaymentMethodExists(int? id)
		{
			return (_context.PaymentMethods?.Any(e => e.MethodId == id)).GetValueOrDefault();
		}

	}
}
