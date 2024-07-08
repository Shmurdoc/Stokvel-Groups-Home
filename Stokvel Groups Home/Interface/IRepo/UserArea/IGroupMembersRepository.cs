using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea
{
	public interface IGroupMembersRepository
	{
		//GetAll && GetById
		Task<List<GroupMembers>>? GetAll();

		Task<GroupMembers>? GetById(int? id);

		//CRUD
		Task Inset(GroupMembers? groupMembers);

		Task Edit(GroupMembers? groupMembers);

		Task Delete(int? id);

		Task SaveAsync();

		bool GroupMembersExists(int? id);

		Task<List<int>> GroupIdList(string id);
	}
}
