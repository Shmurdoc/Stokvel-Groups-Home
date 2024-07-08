using PagedList;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.GroupServices
{
	public class GroupRequestServices : IGroupRequestServices
	{

		private readonly IGroupServices _groupServices;

		public GroupRequestServices(IGroupServices groupServices)
		{
			_groupServices = groupServices;
		}

		public Task<IPagedList<Group>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page) => _groupServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);
	}
}
