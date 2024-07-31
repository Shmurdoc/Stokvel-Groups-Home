using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentStatusService
	{

		Task<List<DepositStatus>>? GetAll();
		Task<DepositStatus>? Details(int? id);

		List<SelectListItem>? PaymentStatusExtendInclude();

		Task Inset(DepositStatus? paymentStatus);

		Task Edit(DepositStatus? paymentStatus);

		Task Delete(int? id);

		Task SaveAsync();

		bool PaymentStatusesExists(int? id);

	}
}
