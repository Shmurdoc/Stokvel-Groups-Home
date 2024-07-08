using PagedList;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.GroupServices;

public class GroupServices : IGroupServices
{
	private readonly IGroupsRepository _groupsRepository;

	public GroupServices(IGroupsRepository groupsRepository)
	{
		_groupsRepository = groupsRepository;
	}

	public async Task<PagedList.IPagedList<Group>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page)
	{
		var groups = from g in await _groupsRepository.GetAll()
					 select g;
		if (!String.IsNullOrEmpty(searchString))
		{
			groups = groups.Where(g => g.GroupName.Contains(searchString)
								   || g.VerifyKey.Contains(searchString));
		}
		switch (sortOrder)
		{
			case "name_desc":
				groups = groups.OrderByDescending(g => g.TypeAccount);
				break;
			case "Date":
				groups = groups.OrderBy(g => g.TypeAccount);
				break;
			case "date_desc":
				groups = groups.OrderByDescending(g => g.GroupDate);
				break;
			default:
				groups = groups.OrderBy(g => g.GroupDate);
				break;
		}


		int pageSize = 3;
		int pageNumber = (page ?? 1);
		PagedList.IPagedList<Group> resultList = groups.ToPagedList(pageNumber, pageSize);
		return resultList;
	}
}
