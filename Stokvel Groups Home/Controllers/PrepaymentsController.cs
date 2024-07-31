using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Repositories;

namespace Stokvel_Groups_Home.Controllers
{
	public class PrepaymentsController : Controller
	{
		private readonly IPrepaymentsRepository _prepaymentsRepository;

		public PrepaymentsController(PrepaymentsRepository prepaymentsRepository)
		{

			_prepaymentsRepository = prepaymentsRepository;

		}

		// GET: Prepayments
		public async Task<IActionResult> Index()
		{

			var allPrepayment = await _prepaymentsRepository.GetAll();
			return allPrepayment != null ?
						View(allPrepayment) :
						Problem("Entity set 'ApplicationDbContext.Prepayments'  is null.");
		}

		// GET: Prepayments/Details/5
		public async Task<IActionResult> Details(int id)
		{

			var prepayment = await _prepaymentsRepository.Detail(id);
			if (id == null || prepayment == null)
			{
				return NotFound();
			}

			return View(prepayment);
		}

		// GET: Prepayments/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Prepayments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PrepaymentId,PrepaymentType,PrepaymentDate,Amount,Status,AccountId")] PreDeposit prepayment)
		{
			if (ModelState.IsValid)
			{
				await _prepaymentsRepository.Inset(prepayment);
				await _prepaymentsRepository.SaveAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(prepayment);
		}

		// GET: Prepayments/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
			var prepaymentInDb = await _prepaymentsRepository.Detail(id);
			if (prepaymentInDb == null)
			{
				return NotFound();
			}

			return View(prepaymentInDb);
		}

		// POST: Prepayments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("PrepaymentId,PrepaymentType,PrepaymentDate,Amount,Status,AccountId")] PreDeposit prepayment)
		{
			if (id != prepayment.PrepaymentId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _prepaymentsRepository.Inset(prepayment);
					await _prepaymentsRepository.SaveAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PrepaymentExists(prepayment.PrepaymentId))
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
			return View(prepayment);
		}

		// GET: Prepayments/Delete/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{

			var prepayment = await _prepaymentsRepository.Detail(id);
			if (id != 0 || prepayment == null)
			{
				return NotFound();
			}

			return View(prepayment);
		}

		// POST: Prepayments/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{

			var prepayment = _prepaymentsRepository.Delete(id);
			if (prepayment == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Prepayments'  is null.");
			}

			await _prepaymentsRepository.Delete(id);
			await _prepaymentsRepository.SaveAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PrepaymentExists(int id)
		{
			var exists = _prepaymentsRepository.PrepaymentExists(id);
			return exists;
		}
	}
}
