using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WalletServices
{
	public class WalletServices : IWalletServices
	{

		private readonly IWalletRepository _walletRepository;
		private readonly IAccountsRepository _accountsRepository;

		public WalletServices(IWalletRepository walletRepository, IAccountsRepository accountsRepository)
		{
			_walletRepository = walletRepository;
			_accountsRepository = accountsRepository;
		}

		public async Task<IEnumerable<WalletAccount>> WalletAccountFatch()
		{

			List<Account>? accounts = await _accountsRepository.GetAll();
			List<Wallet>? wallet = await _walletRepository.GetAll();

			var memberAccount = (from a in accounts
								 join w in wallet on a.AccountId equals w.AccountId into table
								 from w in table.ToList()

								 select new WalletAccount
								 {
									 Account = a,
									 Wallet = w
								 }).ToList();


			return memberAccount;
		}
		public async Task<decimal> MemberWalletAmount(int id)
		{
			var membersInDb = await this.WalletAccountFatch();

			var amount = membersInDb.Where(x => x.Account.AccountId == id).Select(x => x.Wallet.Amount).FirstOrDefault();

			return amount;
		}



	}
}
