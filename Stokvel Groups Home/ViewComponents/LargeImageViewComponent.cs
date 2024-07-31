using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.ViewComponents
{
	public class LargeImageViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;
		private readonly IAccountUserCRUDService _accountUserCRUDService;
		public LargeImageViewComponent(IHomeRequestService homeRequestService, IAccountUserCRUDService accountUserCRUDService)
		{
			_homeRequestService = homeRequestService;
			_accountUserCRUDService = accountUserCRUDService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
			var accountUser = await _accountUserCRUDService.GetById(userId);
			var groupId = await _homeRequestService.MemberAccountGroupId(userId);

            
            ViewBag.image = "~/wwwroot/images/Profile";


			List<AccountUser> profilePic = new List<AccountUser>();


			profilePic.Add(accountUser);
            

            return await Task.FromResult<IViewComponentResult>(View(profilePic));
        }
	}
}
