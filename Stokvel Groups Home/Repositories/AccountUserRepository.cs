using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class AccountUserRepository : IAccountUserRepository
	{
		private readonly ApplicationDbContext _context;
		public AccountUserRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Delete(string? id)
		{
			var delAccountUserInDb = await this.GetById(id);
			_context.Remove(delAccountUserInDb);
		}

		public async Task<List<AccountUser>> GetAll()
		{
			var result = await _context.AccountUsers.ToListAsync();

			return result;

		}

		public async Task<AccountUser> GetById(string? id)
		{
			return await _context.AccountUsers.FirstOrDefaultAsync(m => m.Id == id);
		}

		public async Task Insert(AccountUser? accountUser)
		{
			await _context.AddAsync(accountUser);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Edit(AccountUser? accountUser)
		{
			_context.Update(accountUser);
		}

		public bool AccountUserExists(string? id)
		{
			return (_context.AccountUsers?.Any(e => e.Id == id)).GetValueOrDefault();
		}



	}
}
