using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountUserServices;

public class AccountUserCRUDService : IAccountUserCRUDService
{

	private readonly IAccountUserRepository _accountUserRepository;

	public AccountUserCRUDService(IAccountUserRepository accountUserRepository)
	{
		_accountUserRepository = accountUserRepository;
	}

	public bool AccountUserExists(string? id) => _accountUserRepository.AccountUserExists(id);

	public Task Delete(string? id) => _accountUserRepository.Delete(id);

	public void Edit(AccountUser? accountUser) => _accountUserRepository.Edit(accountUser);

	public Task<List<AccountUser>> GetAll() => _accountUserRepository.GetAll();

	public Task<AccountUser> GetById(string? id) => _accountUserRepository.GetById(id);

	public Task Insert(AccountUser? accountUser) => _accountUserRepository.Insert(accountUser);

	public Task SaveAsync() => _accountUserRepository.SaveAsync();
}
