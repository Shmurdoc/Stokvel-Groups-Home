using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IAccountServices
{
    public interface IAccountServices
	{
		Task<bool> GroupNamesExists(string? id, AccountType typeAccont);

		bool FilterAccount(List<bool> DecisionMaker);

		Task<int> CollectGroupId(List<int> accountIdOfGroups, string groupName);

		// this memberturn
		Task<List<int>> AccountIdForMemeberInGroups(string userId, AccountType? typeAccount);
		Task<List<int>> AccptedGroupMembers(int groupId);

		Task<int> MaxNumberOfMembersInGroup(int groupId);

		Task<IEnumerable<UserGroupMember>> Profile();
		Task<List<int>> AccountId(string userId);
		Task<int> AccountProfileId(string id);
		Task StokvelActive(int groupId);
		Task StartStokvel(List<int> AccountId);
		Task<List<DisplayMemberTurn>> NextPaidMember();
		Task AcceptGroupMemeber(Account account);

	}
}
