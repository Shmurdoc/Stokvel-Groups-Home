using Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices;

namespace Stokvel_Groups_Home.Services.GroupMembersServices
{
	public class GroupMembersRequestServices : IGroupMembersRequestServices
	{
		private readonly IGroupMembersServices _groupMembersServices;
		public GroupMembersRequestServices(IGroupMembersServices groupMembersServices)
		{
			_groupMembersServices = groupMembersServices;
		}

		public async Task<List<int>> GroupIdList(string? id) => await _groupMembersServices.GroupIdList(id);

	}
}
