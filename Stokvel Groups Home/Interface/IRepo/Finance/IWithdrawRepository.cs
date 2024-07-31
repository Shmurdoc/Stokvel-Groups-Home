using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IWithdrawRepository
	{
		Task<List<WithdrawDetails>>? GetAll();

		Task<WithdrawDetails>? Details(int? id);
		Task Insert(WithdrawDetails? invoiceDetails);

		Task Edit(WithdrawDetails? invoiceDetails);

		Task Delete(int? id);

		bool InvoiceDetailsExists(int? id);
	}
}
