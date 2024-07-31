using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IPrepaymentServices
{
	public interface IPrepaymentsCRUDService
	{

        Task<List<PreDeposit>>? GetAll();

		Task<PreDeposit>? GetById(int? id);
		Task<PreDeposit>? Detail(int? id);
		List<SelectListItem>? PaymentStatusExtendInclude();

		Task Inset(PreDeposit? prepayment);

		Task Edit(PreDeposit? prepayment);

		Task Delete(int? id);

		Task SaveAsync();

		bool PrepaymentExists(int? id);
	}
}
