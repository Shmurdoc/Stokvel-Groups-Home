using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IDepositServices
{
	public interface IDepositCRUDService
	{
		Task<List<Deposit>>? GetAll();

		Task<Deposit>? Details(int? id);

		Task Inset(Deposit? deposit);

		Task Edit(Deposit? deposit);

		Task Delete(int? id);

		Task SaveAsync();

		bool DepositExists(int? id);
	}
}
