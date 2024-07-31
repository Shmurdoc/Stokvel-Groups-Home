using Stokvel_Groups_Home.Interface.IServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services
{
	public class PaymentLogsService : IPaymentLogsService
	{
		public Task Delete(string? id)
		{
			throw new NotImplementedException();
		}

		public Task Edit(DepositLog? paymentLog)
		{
			throw new NotImplementedException();
		}

		public Task<List<DepositLog>>? GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<DepositLog>? GetById(int? id)
		{
			throw new NotImplementedException();
		}

		public Task Insert(DepositLog? paymentLog)
		{
			throw new NotImplementedException();
		}

		public bool PaymentLogExists(string? id)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
