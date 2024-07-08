using Microsoft.AspNetCore.Mvc;

namespace Stokvel_Groups_Home.Controllers
{
	public class WalletController : Controller
	{
		// GET: WalletController
		public ActionResult Index()
		{
			return View();
		}

		// GET: WalletController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: WalletController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: WalletController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: WalletController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: WalletController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: WalletController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: WalletController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
