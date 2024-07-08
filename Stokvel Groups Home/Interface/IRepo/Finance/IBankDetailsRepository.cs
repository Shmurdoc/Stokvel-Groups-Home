using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IBankDetailsRepository
	{

		Task<List<BankDetails>>? GetAll();

		Task<BankDetails>? GetById(int? id);
		Task Insert(BankDetails? bankDetails);

		Task Edit(BankDetails? bankDetails);

		Task Delete(int? id);

		Task SaveAsync();

		bool BankDetailsExists(int? id);

	}
}
