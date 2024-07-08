
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea
{
	public interface IAccountUserRepository
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

		//Other

	}
}
