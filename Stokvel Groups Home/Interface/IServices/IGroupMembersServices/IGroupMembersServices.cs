namespace Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices
{
	public interface IGroupMembersServices
	{
		Task<List<int>> GroupIdList(string? id);
	}
}
