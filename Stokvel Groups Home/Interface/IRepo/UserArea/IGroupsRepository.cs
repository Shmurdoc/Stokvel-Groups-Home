using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea
{
	public interface IGroupsRepository
	{
		//GetAll && GetById
		Task<List<Group>>? GetAll();
		Task<Group>? GetById(int? id);

		//CRUD
		Task Inset(Group? group);

		Task Edit(Group? group);

		Task Delete(int? id);

		Task SaveAsync();

		Task<Group>? Details(int? id);

		bool GroupExists(string? id);

		// other

		Task<int> GetGroupId(string groupVerifyKey);
		Task<bool> GroupIdExists(string verifyKey);


	}
}
