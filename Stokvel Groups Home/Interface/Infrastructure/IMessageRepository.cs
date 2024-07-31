using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.Infrastructure
{
    public interface IMessageRepository
    {
		Task<List<Message>> GetAll();
		Task Insert(Message? message);
		Task SaveAsync();

	}
}
