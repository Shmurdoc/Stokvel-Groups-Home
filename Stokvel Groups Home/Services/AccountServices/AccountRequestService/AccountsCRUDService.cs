using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountServices.AccountRequestService
{
	public sealed class AccountsCRUDService : IAccountsCRUDService
	{
		private readonly IAccountsRepository _accountsRepository;

		public AccountsCRUDService(IAccountsRepository accountsRepository)
		{
			_accountsRepository = accountsRepository;
		}

		public Task? Delete(int? id) => _accountsRepository.Delete(id);

		public Task? Edit(Account account) => _accountsRepository.Edit(account);

		public bool Exists(int? id) => _accountsRepository.Exists(id);

		public async Task<List<Account>>? GetAll() => await _accountsRepository.GetAll();

		public async Task<Account>? GetById(int? id) => await _accountsRepository.GetById(id);

		public async Task<List<Account>>? GetByUserId(string? id) => await _accountsRepository.GetByUserId(id);

		public async Task? Insert(Account account) => await _accountsRepository.Insert(account);

		public async Task? SaveAsync() => await _accountsRepository.SaveAsync();
		public bool AccountExists(string? id, string GroupVerifyKey) => _accountsRepository.AccountExists(id, GroupVerifyKey);

	}
}
