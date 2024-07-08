using Microsoft.AspNetCore.Mvc.Rendering;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IPrepaymentServices
{
	public interface IPrepaymentsCRUDService
	{

		Task<List<Prepayment>>? GetAll();

		Task<Prepayment>? GetById(int? id);
		Task<Prepayment>? Detail(int? id);
		List<SelectListItem>? PaymentStatusExtendInclude();

		Task Inset(Prepayment? prepayment);

		Task Edit(Prepayment? prepayment);

		Task Delete(int? id);

		Task SaveAsync();

		bool PrepaymentExists(int? id);
	}
}
