using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Models.Tables;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IDepositRepository
	{
		Task<List<Deposit>>? GetAll();

		Task<Deposit>? Details(int? id);

		Task Inset(Deposit? deposit);

		Task Edit(Deposit? deposit);

		Task Delete(int? id);

		Task SaveAsync();

		bool DepositExists(int? id);

		Task<List<DepositSystem>> Profile();
	}
}
