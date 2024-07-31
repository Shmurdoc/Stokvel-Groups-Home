using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentMethodsService
	{
		Task<List<DepositMethod>>? GetAll();
		Task<DepositMethod>? Details(int? id);

		List<SelectListItem>? PaymentMethodExtendInclude();

		Task Inset(DepositMethod? paymentMethod);

		Task Edit(DepositMethod? paymentMethod);

		Task Delete(int? id);

		Task SaveAsync();

		bool PaymentMethodExists(int? id);
	}
}
