namespace Stokvel_Groups_Home.Services.WalletServices
{
	public interface IWalletRequestServices
	{
		Task<decimal> MemberWalletAmount(int id);
	}
}
