using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IWithdrawServices
{
	public interface IWithdrawCRUDService
	{

		Task Insert(WithdrawDetails? invoiceDetails);

		Task Edit(WithdrawDetails? invoiceDetails);

		Task Delete(int? id);

		Task<List<WithdrawDetails>>? GetAll();
		bool InvoiceDetailsExists(int? id);

	}
}
