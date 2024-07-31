using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.Infrastructure;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class MessagesRepository:IMessageRepository
	{

		private readonly ApplicationDbContext _context;

        public MessagesRepository(ApplicationDbContext context)
        {
            _context = context;
        }


		public async Task<List<Message>> GetAll()
        {
           var result = await _context.Messages.ToListAsync();
            return result;
        }

		public async Task Insert(Message? message)
		{
			await _context.AddAsync(message);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

	}
}
