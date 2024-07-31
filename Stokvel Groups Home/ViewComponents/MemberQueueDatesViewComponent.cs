using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.ViewComponents
{
	public class MemberQueueDatesViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;
        private readonly IAccountUserCRUDService _accountUserCRUDService;
        public MemberQueueDatesViewComponent(IHomeRequestService homeRequestService, IAccountUserCRUDService accountUserCRUDService)
		{
			_homeRequestService = homeRequestService;
			_accountUserCRUDService = accountUserCRUDService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
            var accountUser = await _accountUserCRUDService.GetById(userId);
            var groupId = await _homeRequestService.MemberAccountGroupId(userId);

            ViewBag.MemberPhotoPath = accountUser.MemberPhotoPath;
            ViewBag.image = "~/wwwroot/images/Profile";

            var dateToNextInQueue = await _homeRequestService.MemberQueueList();
			if (groupId.Count > 0)
			{
				dateToNextInQueue = dateToNextInQueue.Where(x => x.GroupMembers.GroupId == groupId[1]).DistinctBy(x=>x.Account.AccountQueue).ToList().Take(12);
				return await Task.FromResult<IViewComponentResult>(View(dateToNextInQueue));
			}
			return View();
		}
	}
}
