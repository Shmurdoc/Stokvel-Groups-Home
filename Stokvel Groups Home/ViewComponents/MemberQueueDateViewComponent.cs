using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
namespace Stokvel_Groups_Home.ViewComponents
{
	public class MemberQueueDateViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;

		public MemberQueueDateViewComponent(IHomeRequestService homeRequestService)
		{
			_homeRequestService = homeRequestService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
			var groupId = await _homeRequestService.MemberAccountGroupId(userId);

			var dateToNextInQueue = await _homeRequestService.MemberQueueList();
			if (groupId.Count > 0)
			{
				dateToNextInQueue = dateToNextInQueue.Where(x => x.GroupMembers.GroupId == groupId[0]).Take(12).ToList();
				return await Task.FromResult<IViewComponentResult>(View(dateToNextInQueue));
			}
			return View();
		}
	}
}
