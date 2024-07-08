using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.DepositServices;


public class DepositRequestService : IDepositRequestService
{

	private readonly IDepositServices _depositServices;
	private readonly IAccountServices _accountServices;
	private readonly IWithdrawServices _withdrawServices;
	public DepositRequestService(IDepositServices depositServices, IAccountServices accountServices, IWithdrawServices withdrawServices)

	{
		_depositServices = depositServices;
		_accountServices = accountServices;
		_withdrawServices = withdrawServices;
	}

	//make monthly deposits  
	public async Task MemberDepositMade(int id, string? userId, Deposit deposit, int accountProfileId, int membershipRank, decimal totalAmountDeposit, MemberStatuses statusRank)
	{
		//get deposit amount form (Deposit deposit) and filter to see if excess exists. return  excess
		deposit.DepositAmount = await this.DepositMonthly(id, deposit.DepositAmount);
		await _depositServices.MemeberDepositMade(id, userId, deposit, accountProfileId, membershipRank, totalAmountDeposit, statusRank);
	}

	public async Task<AccountProfile> Details(int accountProfileId) => await _depositServices.Details(accountProfileId);

	public async Task<Deposit> MembersFirstPreDeposit(int id, Prepayment userPrepaymentDeposit, decimal depositAmount)
	{
		Deposit deposit = new();
		var depositAmountUpdate = await this.DepositMonthly(id, depositAmount);
		var result = await _depositServices.MemebersFirstPreDeposit(id, userPrepaymentDeposit, depositAmountUpdate);

		deposit.DepositId = result;
		deposit.DepositAmount = depositAmountUpdate;
		return deposit;
	}

	public async Task<decimal> DepositMonthly(int accountId, decimal deposit)
	{
		var toDeposit = await _depositServices.MonthlyDeposit(accountId, deposit);
		return toDeposit;
	}

	// deposit reference name
	public async Task<string> ReferenceName(int accountId)
	{
		string? referenceName;

		//get list of members 
		var membersInDb = await _accountServices.NextPaidMember();
		var members = membersInDb.Where(x => x.Account.AccountId == accountId).ToList();
		var groupId = await _withdrawServices.groupId(accountId);
		membersInDb = membersInDb.Where(x => x.GroupMembers.GroupId == groupId).DistinctBy(x => x.GroupMembers.GroupId).ToList();
		int dateIncrement = 0;
		var depositExists = members.Any(x => x.Deposit.DepositAmount == x.Deposit.DepositAmount);

	// get this months deposit member name
	depo:
		if (depositExists)
		{
			referenceName = membersInDb.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month + dateIncrement).Select(x => x.AccountUser.FirstName).FirstOrDefault();
		}
		else
		{
			referenceName = "deposit";
		}
		if (referenceName == null)
		{
			dateIncrement = dateIncrement + 1;
			goto depo;
		}
		return referenceName;
	}

}
