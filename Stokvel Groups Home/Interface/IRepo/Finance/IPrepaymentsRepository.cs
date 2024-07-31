using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IPrepaymentsRepository
	{

		Task<List<PreDeposit>>? GetAll();

		Task<PreDeposit>? Detail(int? id);


		Task Inset(PreDeposit? prepayment);

		Task Edit(PreDeposit? prepayment);

		Task Delete(int? id);

		Task SaveAsync();

		bool PrepaymentExists(int? id);

		Task<PreDeposit>? GetById(int? id);

	}
}
