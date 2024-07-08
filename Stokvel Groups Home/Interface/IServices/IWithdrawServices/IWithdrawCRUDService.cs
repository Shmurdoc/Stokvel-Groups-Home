using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IWithdrawServices
{
	public interface IWithdrawCRUDService
	{

		Task Insert(InvoiceDetails? invoiceDetails);

		Task Edit(InvoiceDetails? invoiceDetails);

		Task Delete(int? id);

		Task<List<InvoiceDetails>>? GetAll();
		bool InvoiceDetailsExists(int? id);

	}
}
