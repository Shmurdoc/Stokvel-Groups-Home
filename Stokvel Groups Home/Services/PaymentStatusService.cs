using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Interface.IServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services
{
	public class PaymentStatusService : IPaymentStatusService
	{
		public Task Delete(int? id)
		{
			throw new NotImplementedException();
		}

		public Task<PaymentStatus>? Details(int? id)
		{
			throw new NotImplementedException();
		}

		public Task Edit(PaymentStatus? paymentStatus)
		{
			throw new NotImplementedException();
		}

		public Task<List<PaymentStatus>>? GetAll()
		{
			throw new NotImplementedException();
		}

		public Task Inset(PaymentStatus? paymentStatus)
		{
			throw new NotImplementedException();
		}

		public bool PaymentStatusesExists(int? id)
		{
			throw new NotImplementedException();
		}

		public List<SelectListItem>? PaymentStatusExtendInclude()
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
