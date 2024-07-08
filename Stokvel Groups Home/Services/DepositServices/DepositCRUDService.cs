using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services
{
	public class DepositCRUDService : IDepositCRUDService
	{
		private readonly IDepositRepository _depositRepository;
		public DepositCRUDService(IDepositRepository depositRepository)
		{
			_depositRepository = depositRepository;
		}

		public Task Delete(int? id)
		{
			throw new NotImplementedException();
		}

		public bool DepositExists(int? id)
		{
			throw new NotImplementedException();
		}

		public Task<Deposit>? Details(int? id)
		{
			throw new NotImplementedException();
		}

		public Task Edit(Deposit? deposit)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Deposit>>? GetAll() => await _depositRepository.GetAll();

		public Task Inset(Deposit? deposit)
		{
			throw new NotImplementedException();
		}

		public Task SaveAsync()
		{
			throw new NotImplementedException();
		}
	}
}
