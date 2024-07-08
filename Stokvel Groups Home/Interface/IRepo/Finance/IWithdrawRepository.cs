using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IWithdrawRepository
	{
		Task<List<InvoiceDetails>>? GetAll();

		Task<InvoiceDetails>? Details(int? id);
		Task Insert(InvoiceDetails? invoiceDetails);

		Task Edit(InvoiceDetails? invoiceDetails);

		Task Delete(int? id);

		bool InvoiceDetailsExists(int? id);
	}
}
