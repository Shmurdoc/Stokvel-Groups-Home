using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Interface.IServices.IMessageServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.MessageServices
{
	public class MessageServices : IMessageServices
	{
		public readonly ApplicationDbContext _context;
		public readonly UserManager<IdentityUser> _userManager;
		public readonly IAccountRequestService _accountRequestService;
		public readonly IAccountUserCRUDService _accountUserCRUDService;

		public MessageServices(ApplicationDbContext context, UserManager<IdentityUser> userManager, IAccountRequestService accountRequestService, IAccountUserCRUDService accountUserCRUDService)
		{
			_context = context;
			_userManager = userManager;
			_accountRequestService = accountRequestService;
			_accountUserCRUDService = accountUserCRUDService;

		}

		public async Task<List<Message>> UserMessages(string id , string managerId, int groupId)
		{
			var messages = await _context.Messages.ToListAsync();
			messages = messages.Where(x => x.UserID == id && x.UserID == managerId).Where(x => x.Group == groupId.ToString()).ToList();

			messages = messages.Where(x => x.Group == groupId.ToString()).ToList();
			return messages;
		}

        public async Task<List<Message>> MemberMessages(string id, string managerId, int groupId)
		{
			var messages = await _context.Messages.ToListAsync();

			messages = messages.Where(x => x.Group == groupId.ToString()).ToList();
			return messages;
		}






	}
}
