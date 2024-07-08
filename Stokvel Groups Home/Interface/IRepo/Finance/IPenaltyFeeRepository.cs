using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IPenaltyFeeRepository
	{
		Task<List<PenaltyFee>>? GetAll();

		Task<PenaltyFee> Details(int? id);
		Task Insert(PenaltyFee? penaltyFee);

		Task Edit(PenaltyFee? penaltyFee);

		Task Delete(int? id);

		bool PenaltyFeeExists(int? id);
	}
}
