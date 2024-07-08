using PagedList;
using Stokvel_Groups_Home.Models;
namespace Stokvel_Groups_Home.Interface.IServices.IAccountUserServices
{
	public interface IAccountUserRequestServices
	{
		Task<IPagedList<AccountUser>> FilterAccountUsers(string sortOrder, string currentFilter, string searchString, int? page);
	}
}
