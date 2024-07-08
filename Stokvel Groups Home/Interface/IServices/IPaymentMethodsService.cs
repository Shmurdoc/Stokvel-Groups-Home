using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentMethodsService
	{
		Task<List<PaymentMethod>>? GetAll();
		Task<PaymentMethod>? Details(int? id);

		List<SelectListItem>? PaymentMethodExtendInclude();

		Task Inset(PaymentMethod? paymentMethod);

		Task Edit(PaymentMethod? paymentMethod);

		Task Delete(int? id);

		Task SaveAsync();

		bool PaymentMethodExists(int? id);
	}
}
