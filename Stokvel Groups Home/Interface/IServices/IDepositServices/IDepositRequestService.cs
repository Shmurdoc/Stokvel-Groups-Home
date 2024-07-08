
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IDepositServices
{
	public interface IDepositRequestService
	{
		Task<Deposit> MembersFirstPreDeposit(int id, Prepayment userPrepaymentDeposit, decimal DepositAmount);
		Task MemberDepositMade(int id, string userId, Deposit deposit, int accountProfileId, int membershipRank, decimal totalAmountDeposit, MemberStatuses statusRank);
		Task<AccountProfile> Details(int accountProfileId);
		Task<decimal> DepositMonthly(int accountId, decimal deposit);
		Task<string> ReferenceName(int accountId);
	}
}
