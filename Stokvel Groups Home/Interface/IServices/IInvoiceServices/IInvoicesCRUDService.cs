using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IInvoiceServices
{
	public interface IInvoicesCRUDService
	{
		// GetAll or GetById
		Task<List<Invoice>>? GetAll();

		Task<Invoice>? Details(int? id);


		//CRUD
		Task Inset(Invoice? invoice);

		Task Edit(Invoice? invoice);

		Task Delete(int? id);

		Task SaveAsync();

		bool InvoiceExists(int? id);

	}
}
