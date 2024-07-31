using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;

public interface IAccountRequestService
{

	Task<int> AccountProfileId(string id);
	Task<List<bool>> TypeAccountExists(string userId);

	Task<List<AccountType>> ValidAccountTypesAdded(string userId);

	Task<IEnumerable<UserGroupMember>> MembersInGroup();

	Task<IEnumerable<UserGroupMember>> PendingMembersInGroup();
	Task<List<UserGroupMember>> CleanListAccountTypesAdded(List<AccountType> accountTypesAdded);
	Task<string> TotalGroupMembersJoined(AccountType? accountType, string groupName, string userId);

	Task<int> CollectGroupIdInDb(List<int> accountIdOfGroups, string groupName);
	Task<List<int>> MembersOfGroup(string userId, AccountType viewTypeAccount);

	Task<List<int>> AccountIdForMemberInGroups(string userId, AccountType? typeAccount);
	Task<List<int>> AcceptedGroupMembers(int groupId);
	Task AcceptGroupMember(Account account);

	Task<(List<int> Paid,
		List<int> NotPaid,
		List<string> UserNames,
		int MembersinDbCount,
		List<int> MemberQueue,
		List<int> MemberNotPaid,
		List<int> MemberPending,
		List<int> MemberPaid)> MembersPaidFilter(int groupId);

	Task StartStokvel(List<int> AccountId, int groupid);
	Task<List<DisplayMemberTurn>> DisplayMemberTurnProfile();
	Task<decimal> DisplayMemberTurnFinance();
	Task<List<int>> accountIdList(string userId);

}


