using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountUserServices
{
	public class AccountUserRequestServices : IAccountUserRequestServices
	{

		private readonly IAccountUserServices _accountUserServices;

		public AccountUserRequestServices(IAccountUserServices accountUserServices)
		{
			_accountUserServices = accountUserServices;
		}


		public async Task<PagedList.IPagedList<AccountUser>> FilterAccountUsers(string sortOrder, string currentFilter, string searchString, int? page) => await _accountUserServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);


	}
}
