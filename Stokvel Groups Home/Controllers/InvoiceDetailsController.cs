using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Controllers
{
	public class InvoiceDetailsController : Controller
	{
		private readonly IWithdrawRequestService _withdrawRequestService;
		private readonly IWithdrawCRUDService _withdrawCRUDService;
		private readonly IAccountRequestService _accountRequestService;

		public InvoiceDetailsController(IWithdrawRequestService withdrawRequestService, IWithdrawCRUDService withdrawCRUDService, IAccountRequestService accountRequestService)
		{
			_withdrawRequestService = withdrawRequestService;
			_withdrawCRUDService = withdrawCRUDService;
			_accountRequestService = accountRequestService;
		}


		public async Task<IActionResult> MemebrResultList(int id, int groupId)
		{

			return View();
		}

		public async Task<IActionResult> Payment(int id, int groupId)
		{
			var memberInDb = await _accountRequestService.DisplayMemberTurnProfile();
			decimal memberTotalAmount = 0;


			// card details

			// list of members paid



			var currnetMember = memberInDb.Where(x => x.GroupMembers.GroupId == groupId).ToList();

			List<string> MemberSet = new();
			List<string> MemberList = new();


			var accountIdList = await _accountRequestService.AcceptedGroupMembers(groupId);

			var paidMemberName = currnetMember.Where(x => x.Account.AccountId == id).Select(x => x.AccountUser.FirstName).FirstOrDefault();

			foreach (var accountId in accountIdList)
			{
				memberTotalAmount += memberInDb.Where(x => x.Account.AccountId == accountId && x.Invoice.Discription == @ViewBag.AccountPayName).Sum(x => x.Invoice.TotalAmount);
			}
			DateTime dateTime = DateTime.Now;
			string month = dateTime.ToString("MMMM");

			ViewBag.MonthlyBill = memberTotalAmount;
			ViewBag.DuePaymentDate = month + " " + 28;
			ViewBag.GroupTarget = memberInDb.Where(x => x.Account.AccountId == id).Select(x => x.GroupMembers.Group.AccountTarget).FirstOrDefault();

			ViewBag.AccountIdList = accountIdList;
			ViewBag.AccountPayName = paidMemberName;

			return View(memberInDb);
		}

		// GET: InvoiceDetails
		public async Task<IActionResult> Index(int accountId)
		{
			var paidMember = await _withdrawRequestService.PaidMember();

			ViewBag.AccountId = accountId;
			ViewBag.MemberCreditAccountList = await _withdrawRequestService.accountIdList(accountId);
			var creditedDateStart = paidMember.Where(x => x.Account.AccountId == accountId).Select(x => x.Account.AccountQueueStart).FirstOrDefault();
			var creditedDateEnd = paidMember.Where(x => x.Account.AccountId == accountId).Select(x => x.Account.AccontQueueEnd).FirstOrDefault();

			paidMember = paidMember.Where(x => x.Invoice.InvoiceDate >= creditedDateStart && x.Invoice.InvoiceDate <= creditedDateEnd).ToList();

			return View(paidMember);
		}


		// GET: InvoiceDetails/Create
		public async Task<IActionResult> Create(int id)
		{
			var memberList = await _withdrawRequestService.ListOfPaidMembers(id);
			//Get account Id list
			ViewBag.MemberList = memberList.Select(x => x.Account.AccountId);
			//total pay collection of funds for the month
			ViewBag.MonthlyAmountTotalPaid = memberList.Where(x => x.Invoice.InvoiceDate.Value.Month == DateTime.Now.Month && x.Invoice.Discription != "Wallet" || x.Invoice.Discription != "Withdraw").Sum(x => x.Invoice.TotalAmount);
			return View();
		}
		// POST: InvoiceDetails/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("DetailedId,InvoiceId,CreditAmount,TaxID,PaymentId")] InvoiceDetails invoiceDetails)
		{
			if (ModelState.IsValid)
			{
				_withdrawRequestService.CreditMember(invoiceDetails);
				return RedirectToAction(nameof(Index));
			}
			return View(invoiceDetails);
		}

	}
}
