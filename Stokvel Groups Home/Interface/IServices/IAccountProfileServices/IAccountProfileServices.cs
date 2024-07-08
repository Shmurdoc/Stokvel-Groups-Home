namespace Stokvel_Groups_Home.Interface.IServices.AccountProfileServices
{
	public interface IAccountProfileServices
	{
		Task<bool> MemberStatusPaidDepo(int accontId);
		Task<bool> MemberStatusPending(int accontId);
		Task<bool> MemberStatusPlat(int accontId);
		Task<List<string>> ListIdInOrder(List<int> comboGroups);
		Task<List<int>> ByGroupFilter(List<string> memberList, List<int> accountStatus, int groupId);
	}
}
