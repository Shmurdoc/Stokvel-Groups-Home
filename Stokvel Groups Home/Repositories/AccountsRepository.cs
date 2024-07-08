using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;
using X.PagedList;

namespace Stokvel_Groups_Home.Repositories
{
	public class AccountsRepository : IAccountsRepository
	{
		private readonly ApplicationDbContext? _context;

		public AccountsRepository(ApplicationDbContext? context)
		{
			_context = context;
		}


		// GetAll or GetBy

		public async Task<List<Account>>? GetAll()
		{
			var getAll = await _context.Accounts.ToListAsync();
			return getAll;
		}

		public async Task<Account>? GetById(int? id)
		{
			var result = await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);

			return result;
		}

		public async Task<List<Account>>? GetByUserId(string? id)
		{
			var result = await _context.Accounts.Where(m => m.Id == id).ToListAsync();

			return result;
		}

		//----- End Of GetAll or GetBy


		// CRUD  Create Remove Update Delete 
		public async Task Insert(Account? account)
		{
			await _context.Accounts.AddAsync(account);
			await _context.SaveChangesAsync();

			var accountId = account.AccountId;
			var verifyKey = account.GroupVerifyKey;


			// move to member groups
			GroupMembers groupMembers = new();
			List<GroupMembers> groupMembersId = await _context.GroupMembers.ToListAsync();
			List<Group> groups = await _context.Groups.ToListAsync();

			// Get Group ID Using Account Id
			var groupId = groups.Where(x => x.VerifyKey == verifyKey).Select(x => x.GroupId).FirstOrDefault();

			groupMembers.AccountId = accountId;
			groupMembers.GroupId = groupId;
			await _context.AddAsync(groupMembers);
			await _context.SaveChangesAsync();

		}
		public async Task Edit(Account account)
		{
			_context.Update(account);
			await _context.SaveChangesAsync();

		}

		public async Task Delete(int? id)
		{
			var account = await this.GetById(id);
			_context.Remove(account);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
		//------- End Of CRUD ------//

		//Exists and Filters 
		public bool AccountExists(string? id, string GroupVerifyKey)
		{
			var accountExists = (_context.Accounts?.Any(e => e.Id == id && e.GroupVerifyKey == GroupVerifyKey)).GetValueOrDefault();
			return accountExists;

		}

		public bool Exists(int? id)
		{
			var accountExists = (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
			return accountExists;

		}


		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			this.disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
