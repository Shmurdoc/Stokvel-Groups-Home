using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IWalletRepository
	{

		Task<List<Wallet>>? GetAll();

		Task<Wallet>? Details(int? id);

		Task Insert(Wallet? wallet);

		Task Edit(Wallet? wallet);



		bool WalletExists(int? id);

	}
}
