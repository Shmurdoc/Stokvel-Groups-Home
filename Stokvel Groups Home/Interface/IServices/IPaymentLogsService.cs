using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IPaymentLogsService
	{
		Task<List<DepositLog>>? GetAll();

		Task<DepositLog>? GetById(int? id);
		Task Insert(DepositLog? paymentLog);

		Task Edit(DepositLog? paymentLog);

		Task Delete(string? id);

		Task SaveAsync();

		bool PaymentLogExists(string? id);

	}
}
