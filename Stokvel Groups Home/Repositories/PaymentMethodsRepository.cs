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

		public async Task<List<DepositMethod>>? GetAll()
		{
			var allInvoices = await _context.DepositMethod.ToListAsync();
			return allInvoices;

		}

		public List<SelectListItem> PaymentMethodExtendInclude()
		{
			var FatchExtendedData = new SelectList(_context.DepositMethod, "MethodId", "MethodId").ToList();
			return FatchExtendedData;
		}

		public async Task<DepositMethod>? Details(int? id)
		{
			return await _context.DepositMethod.FirstOrDefaultAsync(m => m.MethodId == id);
		}

		public async Task Inset(DepositMethod? paymentMethod)
		{
			await _context.DepositMethod.AddAsync(paymentMethod);
		}

		public async Task Edit(DepositMethod? paymentMethod)
		{
			_context.Update(paymentMethod);
		}

		public async Task Delete(int? id)
		{
			var paymentMethod = await this.Details(id);

			_context.DepositMethod.Remove(paymentMethod);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public bool PaymentMethodExists(int? id)
		{
			return (_context.DepositMethod?.Any(e => e.MethodId == id)).GetValueOrDefault();
		}

	}
}
