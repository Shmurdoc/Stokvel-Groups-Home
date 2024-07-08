using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IInvoicesRepository
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

		Task InsetDeposit(int AccontId, int InvoceId);


		Task<List<InvoiceGroupDetails>> Profile();
	}
}