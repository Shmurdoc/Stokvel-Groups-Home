using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IWalletServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WalletServices
{
	public class WalletCRUDService : IWalletCRUDService
	{
		private readonly IWalletRepository _walletRepository;

		public WalletCRUDService(IWalletRepository walletRepository)
		{
			_walletRepository = walletRepository;
		}

		public async Task<Wallet>? Details(int? id) => await _walletRepository.Details(id);

		public async Task Edit(Wallet? wallet) => await _walletRepository.Edit(wallet);

		public async Task<List<Wallet>>? GetAll() => await _walletRepository.GetAll();

		public async Task Insert(Wallet? wallet) => await _walletRepository.Insert(wallet);

		public bool WalletExists(int? id)
		{
			throw new NotImplementedException();
		}
	}
}
