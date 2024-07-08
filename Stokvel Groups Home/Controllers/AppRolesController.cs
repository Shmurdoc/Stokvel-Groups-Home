using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Stokvel_Groups_Home.Controllers
{
	public class AppRolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public AppRolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		//Get Roles From DataBase
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			var roles = _roleManager.Roles;
			return View(roles);
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{

			return View();
		}


		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create([Bind("Name")] IdentityRole model)
		{
			//avoid duplication
			if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
			{


				_roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
			}

			return RedirectToAction("Index");
		}


	}
}
