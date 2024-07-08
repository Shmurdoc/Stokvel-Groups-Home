using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Controllers
{
	public class PaymentStatusController : Controller
	{
		private readonly IPaymentStatusRepository _paymentStatusRepository;

		public PaymentStatusController(IPaymentStatusRepository paymentStatusRepository)
		{
			_paymentStatusRepository = paymentStatusRepository;
		}

		// GET: PaymentStatus
		public async Task<IActionResult> Index()
		{
			var getAll = _paymentStatusRepository.GetAll();
			return getAll != null ?
						  View(getAll) :
						  Problem("Entity set 'ApplicationDbContext.PaymentStatuses'  is null.");
		}

		// GET: PaymentStatus/Details/5
		public async Task<IActionResult> Details(int id)
		{
			if (id == null || _paymentStatusRepository.Details(id) == null)
			{
				return NotFound();
			}

			var paymentStatus = _paymentStatusRepository.Details(id);
			if (paymentStatus == null)
			{
				return NotFound();
			}

			return View(paymentStatus);
		}

		// GET: PaymentStatus/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PaymentStatus/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PaymentStatusId,PaymentStatusName,PaymentStatesDescription")] PaymentStatus paymentStatus)
		{
			if (ModelState.IsValid)
			{
				_paymentStatusRepository.Inset(paymentStatus);
				return RedirectToAction(nameof(Index));
			}
			return View(paymentStatus);
		}

		// GET: PaymentStatus/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			if (id == null || _paymentStatusRepository.Details(id) == null)
			{
				return NotFound();
			}

			var paymentStatus = _paymentStatusRepository.Details(id);
			if (paymentStatus == null)
			{
				return NotFound();
			}
			return View(paymentStatus);
		}

		// POST: PaymentStatus/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PaymentStatusId,PaymentStatusName,PaymentStatesDescription")] PaymentStatus paymentStatus)
		{
			if (id != paymentStatus.PaymentStatusId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_paymentStatusRepository.Edit(paymentStatus);
					_paymentStatusRepository.SaveAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PaymentStatusExists(paymentStatus.PaymentStatusId))
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
			return View(paymentStatus);
		}

		// GET: PaymentStatus/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			if (id == null || _paymentStatusRepository.Details(id) == null)
			{
				return NotFound();
			}

			var paymentStatus = _paymentStatusRepository.Details(id);
			if (paymentStatus == null)
			{
				return NotFound();
			}

			return View(paymentStatus);
		}

		// POST: PaymentStatus/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id, PaymentStatus paymentStatus)
		{
			if (_paymentStatusRepository.Details(id) == null)
			{
				return Problem("Entity set 'ApplicationDbContext.PaymentStatuses'  is null.");
			}
			var getPaymentStatus = _paymentStatusRepository.Details(id);
			if (getPaymentStatus != null)
			{
				_paymentStatusRepository.Delete(id);
			}
			return RedirectToAction(nameof(Index));
		}

		private bool PaymentStatusExists(int id)
		{
			var exists = _paymentStatusRepository.PaymentStatusesExists(id);
			return (exists);
		}
	}
}
