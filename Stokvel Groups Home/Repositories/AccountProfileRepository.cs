using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class AccountProfileRepository : IAccountProfileRepository
	{
		private readonly ApplicationDbContext _context;

		public AccountProfileRepository(ApplicationDbContext context)
		{
			_context = context;
		}



		public async Task<List<AccountProfile>> GetAll()
		{
			return await _context.AccountProfiles.ToListAsync();
		}


		public async Task? Delete(int? id)
		{
			var accountProfile = await this.Details(id);
			_context.Remove(accountProfile);
		}

		public async Task<AccountProfile>? Details(int? id)
		{
			var result = await _context.AccountProfiles.FirstOrDefaultAsync(m => m.AccountProfileId == id);

			return result;
		}

		public async Task? Edit(AccountProfile? accountProfile)
		{
			_context.Update(accountProfile);
		}


		public async Task? Inset(AccountProfile? accountProfile)
		{
			await _context.AccountProfiles.AddAsync(accountProfile);
		}

		public bool PaymentMethodExists(int? id)
		{
			return (_context.AccountProfiles?.Any(e => e.AccountProfileId == id)).GetValueOrDefault();
		}

		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}


	}
}
