using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Interface.Infrastructure;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Models;
using X.PagedList;


namespace Stokvel_Groups_Home.Controllers
{
	public class AccountUsersController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IAccountUserCRUDService _accountUserCRUDService;
		private readonly IAccountUserRequestServices _accountUserRequestServices;

		public AccountUsersController(IUnitOfWork unitOfWork, IAccountUserCRUDService accountUserCRUDService, IAccountUserRequestServices accountUserRequestServices)
		{

			_unitOfWork = unitOfWork;
			_accountUserCRUDService = accountUserCRUDService;
			_accountUserRequestServices = accountUserRequestServices;
		}

		// GET: AccountUsers
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<ViewResult> AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
		{
			if (!User.IsInRole("Admin"))
			{
				return View(RedirectToAction("Index", "Home"));

			}

            ViewBag.image = "/wwwroot/images/Profile";
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

			var accountUsers = await _accountUserRequestServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);

			return View(accountUsers.ToPagedList());
		}



		// GET: AccountUsers
		public async Task<ViewResult> Index()
		{
			var id = User.Identity.GetUserId();
			var accountUsers = await _accountUserCRUDService.GetAll();

			var member = accountUsers.Where(x => x.Id == id).ToList();
			ViewBag.image = "/wwwroot/images/Profile";

			return View(member);
		}

		// GET: AccountUsers/Details/5
		[HttpGet]
		public async Task<IActionResult> Details(string? id)
		{
			var accountUser = await _accountUserCRUDService.GetById(id);
			if (id == null || accountUser == null)
			{
				return NotFound();
			}

			return View(accountUser);
		}

		// GET: AccountUsers/Create
		public async Task<IActionResult> Create()
		{
			var UserId = User.Identity.GetUserId();
			var newUserVerify = await _accountUserCRUDService.GetById(UserId);

			if (newUserVerify != null)
			{
				return RedirectToAction(nameof(AdminIndex));
			}
			return View();
		}

		// POST: AccountUsers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,MemberPhotoPath,MemberFileName,Address,City,Province,Zip,Date")] AccountUser? accountUser)
		{


			if (ModelState.IsValid)
			{
				//Gets User Id
				var UserId = User.Identity.GetUserId();

				//Checks If User Already Exists
				var newUserVerify = await _accountUserCRUDService.GetById(UserId);

				if (newUserVerify != null || accountUser is not null)
				{
					if (HttpContext.Request.Form.Count > 0 && HttpContext.Request.Form.Files.Count > 0)
					{
						var uploadedImage = HttpContext.Request.Form.Files[0];
						accountUser.Id = UserId;
						accountUser.Date = DateTime.Now;

						await _unitOfWork.UploadImage(accountUser, uploadedImage);
					}
					else
					{
						accountUser.Id = UserId;
						accountUser.Date = DateTime.Now;
						accountUser.MemberPhotoPath = "~/wwwroot/images/Profile";
						await _accountUserCRUDService.Insert(accountUser);
						await _accountUserCRUDService.SaveAsync();
					}
					return RedirectToAction(nameof(Index));
				}
			}
			else
			{
				return View();
			}
			return View(accountUser);
		}

		// GET: AccountUsers/Edit/5
		public async Task<IActionResult> Edit(string id)
		{
			var accountUser = await _accountUserCRUDService.GetById(id);
			if (id == null || accountUser == null)
			{
				return NotFound();
			}
			return View(accountUser);
		}

		// POST: AccountUsers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,MemberPhotoPath,MemberFileName,Address,City,Province,Zip,Date")] AccountUser accountUser)
		{
			if (id != accountUser.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_accountUserCRUDService.Edit(accountUser);
					await _accountUserCRUDService.SaveAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!AccountUserExists(accountUser.Id))
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
			return View(accountUser);
		}

		// GET: AccountUsers/Delete/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(string id)
		{
			var accountUser = await _accountUserCRUDService.GetById(id);
			if (id == null || accountUser == null)
			{
				return NotFound();
			}

			return View(accountUser);
		}

		// POST: AccountUsers/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var accountUser = await _accountUserCRUDService.GetById(id);

			if (id == null || accountUser == null)
			{
				return Problem("Entity set 'ApplicationDbContext.AccountUsers'  is null.");
			}
			await _accountUserCRUDService.Delete(id);
			await _accountUserCRUDService.SaveAsync();


			return RedirectToAction(nameof(Index));
		}

		private bool AccountUserExists(string id)
		{
			return (_accountUserCRUDService.AccountUserExists(id));
		}
	}
}
