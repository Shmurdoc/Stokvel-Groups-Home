
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IDepositServices
{
	public interface IDepositRequestService
	{
		Task<PreDeposit> MembersFirstPreDeposit(int id, int PrepaymentId ,decimal preDepoAmount, decimal DepositAmount);
		Task MemberDepositMade(int id, string userId, Deposit deposit, int accountProfileId, int membershipRank, decimal totalAmountDeposit, MemberStatuses statusRank, decimal preDepoAmount);
		Task<AccountProfile> Details(int accountProfileId);
		Task<decimal> DepositMonthly(int accountId, decimal preDepoAmount , decimal deposit);
		Task<string> ReferenceName(int accountId);
		Task<List<PreDeposit>> PreDeposit(int accountId);

    }
}
