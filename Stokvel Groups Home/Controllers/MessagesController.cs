using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Models;
using System.Diagnostics;

namespace Stokvel_Groups_Home.Controllers
{
	[Authorize]
	public class MessagesController : Controller
	{
		public readonly ApplicationDbContext _context;
		public readonly UserManager<IdentityUser> _userManager;
		public readonly IAccountRequestService _accountRequestService;
		public readonly IAccountUserCRUDService _accountUserCRUDService;

		public MessagesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IAccountRequestService accountRequestService, IAccountUserCRUDService accountUserCRUDService)
		{
			_context = context;
			_userManager = userManager;
			_accountRequestService = accountRequestService;
            _accountUserCRUDService = accountUserCRUDService;

        }

		// GET: Messages
		public async Task<IActionResult> Index(int Id, int groupId)
		{


            

            TempData["groupId"] = groupId;
            TempData.Keep("groupId");
            var members = await _accountRequestService.PendingMembersInGroup();

			var memberName = members.Where(x => x.Account.AccountId == Id).Select(x => x.AccountUser.FirstName).FirstOrDefault() +" "+
				members.Where(x => x.Account.AccountId == Id).Select(x => x.AccountUser.LastName).FirstOrDefault();

            if (User.IsInRole("Admin"))
            {
				var adminId = await _userManager.GetUserAsync(User);

                var admin = await _accountUserCRUDService.GetById(adminId.Id);
                ViewBag.GroupId = groupId;
                ViewBag.CurrentUser = await _userManager.GetUserAsync(User);
                ViewBag.MemberName = admin.FirstName +" "+admin.LastName;
                ViewBag.FileName = admin.MemberFileName;
                ViewBag.PathName = admin.MemberPhotoPath;
                ViewBag.image = "/wwwroot/images/Profile";
            }
			else
			{
                var memberImageName = members.Where(x => x.Account.AccountId == Id).Select(x => x.AccountUser.MemberFileName).FirstOrDefault();
                var memberImagePath = members.Where(x => x.Account.AccountId == Id).Select(x => x.AccountUser.MemberPhotoPath).FirstOrDefault();


                ViewBag.GroupId = groupId;
                ViewBag.CurrentUser = await _userManager.GetUserAsync(User);
                ViewBag.MemberName = memberName;
                ViewBag.FileName = memberImageName;
                ViewBag.PathName = memberImagePath;
                ViewBag.image = "/wwwroot/images/Profile";
            }
            

			var currentUser = await _userManager.GetUserAsync(User);
			if (User.Identity.IsAuthenticated)
			{
				ViewBag.CurrentUserName = currentUser.UserName;
			}
			var messages = await _context.Messages.ToListAsync();

			messages = messages.Where(x=>x.Group == groupId.ToString()).ToList();
			return View(messages);
		}

		// GET: Messages
		public async Task<IActionResult> chats()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (User.Identity.IsAuthenticated)
			{
				ViewBag.CurrentUserName = currentUser.UserName;
			}
			var messages = await _context.Messages.ToListAsync();
			return View(messages);
		}

		// POST: Messages/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

		public async Task<IActionResult> Create(Message message)
		{

			{
				message.UserName = User.Identity.Name;
				var sender = await _userManager.GetUserAsync(User);
				message.UserID = sender.Id;
				await _context.Messages.AddAsync(message);
				await _context.SaveChangesAsync();
				return Ok();
			}
			return Error();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
