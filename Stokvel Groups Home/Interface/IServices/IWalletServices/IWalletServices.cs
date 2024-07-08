using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WalletServices
{
	public interface IWalletServices
	{
		Task<IEnumerable<WalletAccount>> WalletAccountFatch();
		Task<decimal> MemberWalletAmount(int id);
	}
}
