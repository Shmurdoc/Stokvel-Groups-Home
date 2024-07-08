using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea;

public interface IAccountsRepository : IDisposable
{
	//GetALL or GetByID
	Task<List<Account>>? GetAll();

	Task<Account>? GetById(int? id);

	Task<List<Account>>? GetByUserId(string? id);

	// Crud
	Task Insert(Account? account);

	Task Edit(Account? account);

	Task Delete(int? id);

	Task SaveAsync();

	//Filter

	bool AccountExists(string id, string GroupVerifyKey);

	bool Exists(int? id);


}
