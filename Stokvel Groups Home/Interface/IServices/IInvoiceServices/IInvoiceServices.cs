using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.InvoiceServices
{
    public interface IInvoiceServices
	{

		Task<PagedList.IPagedList<InvoiceGroupDetails>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page);

		Task InsetDeposit(int AccontId, int InvoceId);
	}
}
