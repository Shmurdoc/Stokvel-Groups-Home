using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IInvoiceServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.InvoiceServices
{
	public class InvoicesCRUDService : IInvoicesCRUDService
	{
		private readonly IInvoicesRepository _invoicesRepository;

		public InvoicesCRUDService(IInvoicesRepository invoicesRepository)
		{
			_invoicesRepository = invoicesRepository;
		}

		public async Task? Delete(int? id)
		{

			await _invoicesRepository.Delete(id);
		}

		public async Task<Invoice>? Details(int? id) => await _invoicesRepository.Details(id);

		public async Task Edit(Invoice? invoice) => _invoicesRepository.Edit(invoice);


		public async Task<List<Invoice>>? GetAll() => await _invoicesRepository.GetAll();


		public async Task? Inset(Invoice? invoice) => await _invoicesRepository.Inset(invoice);



		public bool InvoiceExists(int? id) => _invoicesRepository.InvoiceExists(id);


		public async Task? SaveAsync() => await _invoicesRepository.SaveAsync();


	}
}
