using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.ViewComponents
{
    public class DailyGroupUpdatesViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;

		public DailyGroupUpdatesViewComponent(IHomeRequestService homeRequestService)
		{
			_homeRequestService = homeRequestService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
			List<DisplayMemberTurn> displayDisplayRecentDeposit = new();
			var groupId = await _homeRequestService.MemberAccountGroupId(userId);


			displayDisplayRecentDeposit.AddRange(await _homeRequestService.DisplayRecentDeposit());
			if (groupId.Count > 1)
			{
				displayDisplayRecentDeposit = displayDisplayRecentDeposit.Where(x => x.GroupMembers.GroupId == groupId[1]).Where(x => x.Deposit.DepositDate.Day >= DateTime.Now.Day - 5).OrderBy(x=>x.Deposit.DepositDate).Take(12).ToList();
				return await Task.FromResult<IViewComponentResult>(View(displayDisplayRecentDeposit));
			}
			return View();
		}


	}
}
