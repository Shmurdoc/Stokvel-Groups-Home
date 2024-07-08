using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class PenaltyFeeRepository : IPenaltyFeeRepository
	{

		private readonly ApplicationDbContext _context;
		public PenaltyFeeRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Delete(int? id)
		{
			var penaltyFee = await this.Details(id);
			_context.Remove(penaltyFee);
		}
		public async Task<PenaltyFee>? Details(int? id)
		{
			return await _context.PenaltyFees.FirstOrDefaultAsync(m => m.PenaltyFeeId == id);
		}
		public async Task Edit(PenaltyFee? penaltyFee)
		{
			_context.Update(penaltyFee);
		}

		public async Task<List<PenaltyFee>>? GetAll()
		{
			var getAll = await _context.PenaltyFees.ToListAsync();
			return getAll;
		}


		public async Task Insert(PenaltyFee? penaltyFee)
		{
			await _context.AddAsync(penaltyFee);
			await _context.SaveChangesAsync();
		}

		public bool PenaltyFeeExists(int? id)
		{
			throw new NotImplementedException();
		}


	}
}
