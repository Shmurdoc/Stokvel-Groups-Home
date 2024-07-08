using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;

namespace Stokvel_Groups_Home.ViewComponents
{
	public class DailyGroupUpdateViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;

		public DailyGroupUpdateViewComponent(IHomeRequestService homeRequestService)
		{
			_homeRequestService = homeRequestService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();

			var groupId = await _homeRequestService.MemberAccountGroupId(userId);

			var displayDisplayRecentDeposit = await _homeRequestService.DisplayRecentDeposit();
			if (groupId.Count > 0)
			{
				displayDisplayRecentDeposit = displayDisplayRecentDeposit.Where(x => x.GroupMembers.GroupId == groupId[0]).Take(5).ToList();
				return await Task.FromResult<IViewComponentResult>(View(displayDisplayRecentDeposit));
			}
			return View();
		}
	}
}
