using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance
{
	public interface IPrepaymentsRepository
	{

		Task<List<Prepayment>>? GetAll();

		Task<Prepayment>? Detail(int? id);


		Task Inset(Prepayment? prepayment);

		Task Edit(Prepayment? prepayment);

		Task Delete(int? id);

		Task SaveAsync();

		bool PrepaymentExists(int? id);

		Task<Prepayment>? GetById(int? id);

	}
}
