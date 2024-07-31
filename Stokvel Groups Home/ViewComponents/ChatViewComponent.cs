using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.Infrastructure;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Repositories;

namespace Stokvel_Groups_Home.ViewComponents
{
    public class ChatViewComponent : ViewComponent
	{
		private readonly IAccountRequestService _accountRequestService;
		private readonly IMessageRepository _messageRepository;
		private readonly IAccountUserCRUDService _accountUserCRUDService;
		private readonly ApplicationDbContext _context;

		public ChatViewComponent(IAccountRequestService accountRequestService, IMessageRepository messageRepository, IAccountUserCRUDService accountUserCRUDService, ApplicationDbContext context)
		{
			_accountRequestService = accountRequestService;
			_messageRepository = messageRepository;
			_accountUserCRUDService = accountUserCRUDService;
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
			int? groupId = TempData["groupId"] as int?;
			TempData.Keep("groupId");
			List<AccountUser>? groupChat = new();
			List<AccountUser>? totalList = new();

			var membersInDb = await _context.Messages.ToListAsync();
			var listId = membersInDb.Where(x => x.Group == groupId.ToString()).DistinctBy(x=>x.UserID).Select(x=>x.UserID).ToList();



			var members = await _messageRepository.GetAll();

			var member = await _context.Groups.ToListAsync();
			var groupManager = member.Where(x => x.GroupId == groupId).Select(x => x.ManagerId).FirstOrDefault();
			var membersInGroup = await _accountUserCRUDService.GetAll();







			foreach (var memberId in listId)
			{
				if (memberId == groupManager) { 
					continue; 
				}
				else
				{
					totalList = membersInGroup.Where(x => x.Id == memberId).ToList();
					if(totalList != null)
					{
						groupChat.AddRange(totalList);
					}
					
				}
			}


			ViewBag.GroupId = groupId;
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
