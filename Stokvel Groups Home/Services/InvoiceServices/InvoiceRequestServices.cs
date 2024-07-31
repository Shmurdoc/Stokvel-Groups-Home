using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.InvoiceServices
{
    public class InvoiceRequestServices : IInvoiceRequestServices
	{
		private readonly IInvoiceServices _invoiceServices;

		public InvoiceRequestServices(IInvoiceServices invoiceServices)
		{
			_invoiceServices = invoiceServices;
		}

		public async Task<PagedList.IPagedList<InvoiceGroupDetails>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page) => await _invoiceServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);

		public async Task InsetDeposit(int AccontId, int InvoceId) => await _invoiceServices.InsetDeposit(AccontId, InvoceId);
	}
}
