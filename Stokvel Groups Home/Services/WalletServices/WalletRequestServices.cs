namespace Stokvel_Groups_Home.Services.WalletServices
{
	public class WalletRequestServices : IWalletRequestServices
	{
		private readonly IWalletServices _walletServices;

		public WalletRequestServices(IWalletServices walletServices)
		{
			_walletServices = walletServices;
		}



		public async Task<decimal> MemberWalletAmount(int id) => await _walletServices.MemberWalletAmount(id);
	}
}
