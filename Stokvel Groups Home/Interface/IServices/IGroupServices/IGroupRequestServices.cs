using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IGroupServices
{
	public interface IGroupRequestServices
	{

		Task<PagedList.IPagedList<Group>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page);
	}
}
