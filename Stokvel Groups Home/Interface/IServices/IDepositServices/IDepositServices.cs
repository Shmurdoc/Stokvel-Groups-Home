﻿using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IDepositServices;

public interface IDepositServices
{
	Task<int> MemebersFirstPreDeposit(int id, int PrepaymentId, decimal preDepoAmount, decimal DepositAmount);
	Task MemeberDepositMade(int id, string userId, Deposit deposit, int accountProfileId, int membershipRank, decimal totalAmountDeposit, MemberStatuses statusRank);

	Task Update(AccountProfile accountProfile);
	Task<AccountProfile> Details(int id);
	Task<decimal> MonthlyDeposit(int accountId, decimal deposit, decimal preDepoAmount);
	Task<MemberStatuses> StatusClassCalculation(int membershipRank);
	Task<List<PreDeposit>> PreDeposit(int accountId);
	Task<PreDeposit> GetAnomaly(int accountId);
}
