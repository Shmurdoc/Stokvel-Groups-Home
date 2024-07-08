using PagedList;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountUserServices
{
	public class AccountUserServices : IAccountUserServices
	{
		private readonly IAccountUserRepository _accountUserRepository;

		public AccountUserServices(IAccountUserRepository accountUserRepository)
		{
			_accountUserRepository = accountUserRepository;
		}

		public async Task<PagedList.IPagedList<AccountUser>> FilterAccountUsers(string sortOrder, string currentFilter, string searchString, int? page)
		{
			var accountUsers = from au in await _accountUserRepository.GetAll()
							   select au;

			if (!String.IsNullOrEmpty(searchString))
			{
				accountUsers = accountUsers.Where(au => au.LastName.Contains(searchString)
									   || au.FirstName.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					accountUsers = accountUsers.OrderByDescending(au => au.LastName);
					break;
				case "Date":
					accountUsers = accountUsers.OrderBy(au => au.Date);
					break;
				case "date_desc":
					accountUsers = accountUsers.OrderByDescending(au => au.Date);
					break;
				default:
					accountUsers = accountUsers.OrderBy(au => au.LastName);
					break;
			}


			int pageSize = 3;
			int pageNumber = (page ?? 1);
			PagedList.IPagedList<AccountUser> resultList = accountUsers.ToPagedList(pageNumber, pageSize);
			return resultList;
		}
	}
}
