using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IMessageServices
{
	public interface IMessageRequestService
	{

		Task<List<Message>> UserMessages(string id, string managerId, int groupId);
		Task<List<Message>> MemberMessages(string id, string managerId, int groupId);
		string Status(int accountId);
	}
}
