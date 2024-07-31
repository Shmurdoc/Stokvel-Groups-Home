using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Controllers
{
	public class DepositMethodsController : Controller
	{
		private readonly IPaymentMethodsRepository _paymentMethodsRepository;

		public DepositMethodsController(IPaymentMethodsRepository paymentMethodsRepository)
		{
			_paymentMethodsRepository = paymentMethodsRepository;
		}

		// GET: PaymentMethods
		public async Task<IActionResult> Index()
		{

			var getAll = _paymentMethodsRepository.GetAll();
			return getAll != null ?
						View(getAll) :
						Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
		}

		// GET: PaymentMethods/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			var PayMethodsInDb = await _paymentMethodsRepository.Details(id);
			if (id == null || PayMethodsInDb == null)
			{
				return NotFound();
			}

			return View(PayMethodsInDb);
		}

		// GET: PaymentMethods/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: PaymentMethods/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("MethodId,MethodName,Description")] DepositMethod paymentMethod)
		{
			if (ModelState.IsValid)
			{

				_paymentMethodsRepository.Inset(paymentMethod);
				return RedirectToAction(nameof(Index));
			}
			return View(paymentMethod);
		}

		// GET: PaymentMethods/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			var editPayMethodInDb = await _paymentMethodsRepository.Details(id);
			if (id == null || editPayMethodInDb == null)
			{
				return NotFound();
			}

			return View(editPayMethodInDb);
		}

		// POST: PaymentMethods/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("MethodId,MethodName,Description")] DepositMethod paymentMethod)
		{
			if (id != paymentMethod.MethodId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_paymentMethodsRepository.Edit(paymentMethod);

				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PaymentMethodExists(paymentMethod.MethodId))
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
			return View(paymentMethod);
		}

		// GET: PaymentMethods/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			var displayDelPayMethodInDb = await _paymentMethodsRepository.Details(id);
			if (id == null || displayDelPayMethodInDb == null)
			{
				return NotFound();
			}

			return View(displayDelPayMethodInDb);
		}

		// POST: PaymentMethods/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int? id)
		{
			var delPayMethodInDb = await _paymentMethodsRepository.Details(id);
			if (id == null || delPayMethodInDb == null)
			{
				return Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
			}

			_paymentMethodsRepository.Delete(id);

			return RedirectToAction(nameof(Index));
		}

		private bool PaymentMethodExists(int? id)
		{
			var exists = _paymentMethodsRepository.PaymentMethodExists(id);
			return (exists);
		}
	}
}
