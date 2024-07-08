using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IGroupServices;

public interface IGroupsCRUDService
{

	//GetAll && GetById
	Task<List<Group>>? GetAll();
	Task<Group>? GetById(int? id);
	Task<int> GetGroupId(string? groupVerifyKey);

	//CRUD
	Task Inset(Group? group);

	Task Edit(Group? group);

	Task Delete(int? id);

	Task SaveAsync();

	Task<Group>? Details(int? id);

	bool GroupExists(string? id);
}
