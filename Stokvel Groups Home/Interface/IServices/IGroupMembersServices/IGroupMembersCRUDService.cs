using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices
{
	public interface IGroupMembersCRUDService
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

	}
}
