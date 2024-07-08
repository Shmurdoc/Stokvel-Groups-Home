using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IInvoiceServices;
using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Services.InvoiceServices;
using X.PagedList;

namespace Stokvel_Groups_Home.Controllers
{
	public class InvoicesController : Controller
	{


		private readonly IInvoiceRequestServices _invoiceRequestServices;
		private readonly IInvoicesCRUDService _invoicesCRUDService;

		public InvoicesController(IInvoicesRepository invoicesRepository, IInvoiceRequestServices invoiceRequestServices, IInvoicesCRUDService invoicesCRUDService)
		{
			_invoicesCRUDService = invoicesCRUDService;
			_invoiceRequestServices = invoiceRequestServices;
		}


		// GET: Invoices
		public async Task<IActionResult> AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			ViewBag.CurrentFilter = searchString;


			return View();
		}
		[HttpGet]
		// GET: Invoices
		public async Task<IActionResult> Index(int id, string sortOrder, string currentFilter, string searchString, int? page)
		{


			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";


			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var accountUsers = await _invoiceRequestServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);
			ViewBag.MemberCount = accountUsers.Count;
			return View(accountUsers.ToPagedList());
		}

		// GET: Invoices/Details/5
		public async Task<IActionResult?> Details(int? id)
		{
			var invoice = await _invoicesCRUDService.Details(id);
			if (id == null || invoice == null)
			{
				return NotFound();
			}

			return View(invoice);
		}

		// GET: Invoices/Create
		[HttpGet]

		public IActionResult Create()
		{
			return View();
		}

		// POST: Invoices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("InvoiceDate,DueDate,Discription,TotalAmount")] int id, Invoice invoice)
		{

			if (ModelState.IsValid)
			{
				var userId = User.Identity.GetUserId();


				await _invoicesCRUDService.Inset(invoice);
				await _invoicesCRUDService.SaveAsync();

				await _invoiceRequestServices.InsetDeposit(id, invoice.InvoiceId);



				var model = invoice.InvoiceId;
				return RedirectToAction(nameof(Index));

			}

			return View(invoice);
		}

		// GET: Invoices/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id)
		{
			var editInvoiceInDb = await _invoicesCRUDService.Details(id);
			if (id == null || editInvoiceInDb == null)
			{
				return NotFound();
			}

			return View(editInvoiceInDb);
		}

		// POST: Invoices/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		// POST: Invoices/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, [Bind("GroupId,InvoiceDate,DueDate,TotalAmount")] Invoice invoice)
		{
			if (id != invoice.InvoiceId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _invoicesCRUDService.Edit(invoice);
					await _invoicesCRUDService.SaveAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!InvoiceExists(invoice.InvoiceId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(invoice);
		}

		// GET: Invoices/Delete/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			var displayDelInvoiceInDb = await _invoicesCRUDService.Details(id);
			if (id == null || displayDelInvoiceInDb == null)
			{
				return NotFound();
			}
			return View(displayDelInvoiceInDb);
		}

		// POST: Invoices/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			var delInvoiceInDb = await _invoicesCRUDService.Details(id);
			if (id == null || delInvoiceInDb == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Invoices'  is null.");
			}

			await _invoicesCRUDService.Delete(id);
			await _invoicesCRUDService.SaveAsync();

			return RedirectToAction(nameof(Index));
		}

		private bool InvoiceExists(int? id)
		{
			var exists = _invoicesCRUDService.InvoiceExists(id);

			return exists;
		}


	}
}

