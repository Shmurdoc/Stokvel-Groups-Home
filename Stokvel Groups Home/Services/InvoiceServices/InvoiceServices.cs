using PagedList;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.InvoiceServices
{
    public class InvoiceServices : IInvoiceServices
	{
		private readonly IInvoicesRepository _invoicesRepository;

		public InvoiceServices(IInvoicesRepository invoicesRepository)
		{
			_invoicesRepository = invoicesRepository;

		}

		public async Task<PagedList.IPagedList<InvoiceGroupDetails>> FilterAccountUsers(string? sortOrder, string? currentFilter, string? searchString, int? page)
		{

			var profile = await _invoicesRepository.Profile();

			var mrr = profile.AsQueryable();

			var usersInvoice = (from p in mrr
								select p).AsQueryable();



			if (!String.IsNullOrEmpty(searchString))
			{
				usersInvoice = usersInvoice.Where(ui => ui.AccountUser.FirstName.Contains(searchString)
									   || ui.AccountUser.LastName.Contains(searchString));
			}
			switch (sortOrder)
			{
				case "name_desc":
					usersInvoice = usersInvoice.OrderByDescending(g => g.AccountUser.FirstName);
					break;
				case "Date":
					usersInvoice = usersInvoice.OrderBy(g => g.MemberInvoice.Invoice.InvoiceDate);
					break;
				case "date_desc":
					usersInvoice = usersInvoice.OrderByDescending(g => g.MemberInvoice.Invoice.Discription);
					break;
				default:
					usersInvoice = usersInvoice.OrderBy(g => g.MemberInvoice.InvoceId);
					break;
			}


			int pageSize = usersInvoice.Count();
			int pageNumber = (page ?? 1);

			PagedList.IPagedList<InvoiceGroupDetails> resultList = usersInvoice.ToPagedList(pageNumber, pageSize);
			return resultList;
		}

		public async Task InsetDeposit(int AccontId, int InvoceId) => _invoicesRepository.InsetDeposit(AccontId, InvoceId);


	}
}
