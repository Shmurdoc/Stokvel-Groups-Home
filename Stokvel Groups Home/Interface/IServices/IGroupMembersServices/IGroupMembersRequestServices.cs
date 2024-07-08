namespace Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices
{
	public interface IGroupMembersRequestServices
	{
		Task<List<int>> GroupIdList(string? id);
	}
}
