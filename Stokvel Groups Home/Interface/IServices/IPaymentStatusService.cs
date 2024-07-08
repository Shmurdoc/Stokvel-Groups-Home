using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentStatusService
	{

		Task<List<PaymentStatus>>? GetAll();
		Task<PaymentStatus>? Details(int? id);

		List<SelectListItem>? PaymentStatusExtendInclude();

		Task Inset(PaymentStatus? paymentStatus);

		Task Edit(PaymentStatus? paymentStatus);

		Task Delete(int? id);

		Task SaveAsync();

		bool PaymentStatusesExists(int? id);

	}
}
