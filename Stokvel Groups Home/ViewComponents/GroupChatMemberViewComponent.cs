using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;


namespace Stokvel_Groups_Home.ViewComponents
{
	public class GroupChatMemberViewComponent : ViewComponent
	{
		private readonly IAccountRequestService _accountRequestService;

		public GroupChatMemberViewComponent(IAccountRequestService accountRequestService)
		{
			_accountRequestService = accountRequestService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
            int? groupId = TempData["groupId"] as int?;
            TempData.Keep("groupId");

            var membersInGroup = await _accountRequestService.MembersInGroup();

			var groupChat = membersInGroup.Where(x => x.Group.GroupId == groupId).ToList();

			var groupManager = groupChat.Select(x=>x.Group.ManagerId).FirstOrDefault();

			ViewBag.GroupManager = groupManager;
			ViewBag.image = "/wwwroot/images/Profile";

			if (groupId > 0)
			{
			

				return await Task.FromResult<IViewComponentResult>(View(groupChat));
			}
			return View();
		}


	}
}
