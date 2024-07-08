using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{

	public class WalletRepository : IWalletRepository
	{



		private readonly ApplicationDbContext _context;

		public WalletRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Edit(Wallet? wallet)
		{
			_context.Update(wallet);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Wallet>>? GetAll()
		{
			return await _context.Wallets.ToListAsync();
		}

		public async Task<Wallet>? Details(int? id)
		{
			return await _context.Wallets.FirstOrDefaultAsync(x => x.AccountId == id);
		}

		public async Task Insert(Wallet? wallet)
		{
			await _context.AddAsync(wallet);
			await _context.SaveChangesAsync();
		}



		public bool WalletExists(int? id)
		{
			throw new NotImplementedException();
		}
	}
}
