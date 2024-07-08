using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IAccountUserServices
{
	public interface IAccountUserCRUDService
	{

		//GetAll && GetById
		Task<List<AccountUser>>? GetAll();

		Task<AccountUser>? GetById(string? id);

		//CRUD
		Task Insert(AccountUser? accountUser);

		void Edit(AccountUser? accountUser);

		Task Delete(string? id);

		Task SaveAsync();

		bool AccountUserExists(string? id);
	}
}
