using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.GroupMembersServices
{
	public class GroupMembersServices : IGroupMembersServices
	{

		private readonly IGroupMembersRepository _groupMembersRepository;

		public GroupMembersServices(IGroupMembersRepository groupMembersRepository)
		{
			_groupMembersRepository = groupMembersRepository;
		}

		public async Task<List<int>> GroupIdList(string? id)
		{
			List<GroupMembers> groupMembers = await _groupMembersRepository.GetAll();
			var accountIdList = groupMembers.Where(x => x.Account.Id == id).Select(x => x.AccountId).ToList();
			List<int>? groupid = new();

			foreach (var accountid in accountIdList)
			{
				var groupList = groupMembers;
				var groupIdList = groupList.Where(x => x.AccountId == accountid).Select(x => x.GroupId).FirstOrDefault();
				groupid.Add(groupIdList);
			}
			return groupid;
		}
	}
}
