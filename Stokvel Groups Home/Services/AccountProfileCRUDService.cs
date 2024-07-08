using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IService;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services;

public class AccountProfileCRUDService : IAccountProfileCRUDService
{
	private readonly IAccountProfileRepository _accountProfileRepository;
	public AccountProfileCRUDService(IAccountProfileRepository accountProfileRepository)
	{
		_accountProfileRepository = accountProfileRepository;
	}

	Task IAccountProfileCRUDService.Delete(int? id) => _accountProfileRepository.Delete(id);

	Task<AccountProfile> IAccountProfileCRUDService.Details(int? id) => _accountProfileRepository?.Details(id);

	Task IAccountProfileCRUDService.Edit(AccountProfile? paymentMethod) => _accountProfileRepository?.Edit(paymentMethod);

	Task<List<AccountProfile>> IAccountProfileCRUDService.GetAll() => _accountProfileRepository.GetAll();

	Task IAccountProfileCRUDService.Inset(AccountProfile? paymentMethod) => _accountProfileRepository.Inset(paymentMethod);

	Task IAccountProfileCRUDService.SaveAsync() => _accountProfileRepository.SaveAsync();
}
