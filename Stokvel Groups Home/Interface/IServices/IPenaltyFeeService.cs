using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPenaltyFeeService
	{
		Task<List<PenaltyFee>>? GetAll();

		Task<Account> GetById(string? id);
		Task Insert(PenaltyFee? penaltyFee);

		Task Edit(PenaltyFee? penaltyFee);

		Task Delete(string? id);

		Task SaveAsync();

		bool PenaltyFeeExists(string? id);

	}
}
