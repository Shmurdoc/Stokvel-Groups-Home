using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Interface.IServices.IMessageServices;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.MessageServices
{
	public class MessageRequestService : IMessageRequestService
	{
		private readonly IMessageServices _messageServices;

        public MessageRequestService(IMessageServices messageServices)
        {
            _messageServices = messageServices;
        }



		public async Task<List<Message>> UserMessages(string id, string managerId, int groupId) => await _messageServices.UserMessages(id,managerId,groupId);

		public async Task<List<Message>> MemberMessages(string id, string managerId, int groupId) => await _messageServices.MemberMessages(id,managerId,groupId);

		public string Status(int accountId)
		{
			string? result;
			if(accountId != 0)
			{
				result = "memberMessages";
			}
			else
			{
				result = "memberMessages";
			}
			return result;
		}
	}
}
