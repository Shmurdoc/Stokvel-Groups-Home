using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WithdrawServices
{
	public sealed class WithdrawCRUDService : IWithdrawCRUDService
	{
		private readonly IWithdrawRepository _withdrawRepository;

		public WithdrawCRUDService(IWithdrawRepository withdrawRepository)
		{
			_withdrawRepository = withdrawRepository;
		}

		public async Task Delete(int? id) => await _withdrawRepository.Delete(id);

		public async Task Edit(WithdrawDetails? invoiceDetails) => _withdrawRepository.Edit(invoiceDetails);

		public async Task Insert(WithdrawDetails? invoiceDetails) => _withdrawRepository.Insert(invoiceDetails);

		public async Task<List<WithdrawDetails>>? GetAll() => await _withdrawRepository.GetAll();
		public bool InvoiceDetailsExists(int? id) => _withdrawRepository.InvoiceDetailsExists(id);
	}
}
