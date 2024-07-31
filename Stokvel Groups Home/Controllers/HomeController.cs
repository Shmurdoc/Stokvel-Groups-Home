using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.ICalendarServices;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
using Stokvel_Groups_Home.Models;
using System.Diagnostics;

namespace Stokvel_Groups_Home.Controllers;
[Authorize]
public class HomeController : Controller
{
	


	private readonly IHomeRequestService _homeRequestService;
	private readonly IAccountUserCRUDService _accountUserCRUDService;
	private readonly IAccountRequestService _accountRequestService;
	private readonly ICalendarRequestServices _calendarRequestServices;
	private readonly ICalendarRepository _calendarRepository;
	private readonly IMemoryCache _cache;
    private readonly ILogger<HomeController> _logger;

    private readonly string cacheKey = "memberDisplayCacheKey";

	public HomeController(ILogger<HomeController> logger,
		IDepositServices depositServices, IHomeRequestService homeRequestService,
		IAccountRequestService accountRequestService,
		IAccountUserCRUDService accountUserCRUDService,
		IMemoryCache cache,
		ICalendarRequestServices calendarRequestServices,
		ICalendarRepository calendarRepository)
	{
		_logger = logger;
		_homeRequestService = homeRequestService;
		_accountRequestService = accountRequestService;
		_accountUserCRUDService = accountUserCRUDService;
		_calendarRequestServices = calendarRequestServices;
		_cache = cache;
		_calendarRepository = calendarRepository;
	}


	public async Task<IActionResult> Index()
	{
		

		List<DisplayMemberTurn> memberTurn = new();


		var roleAccount = User.IsInRole("Admin");
		var userId = User.Identity.GetUserId();


        var account = await _homeRequestService.NumberOfAccounts(userId);
		var dateToNextDeposit = await _homeRequestService.DateToNextDeposit(userId);
		var totalAmountOwed = await _homeRequestService.TotalOwed(userId);
		var depositDue = await _homeRequestService.TotalAmountDue(userId);

		
	

		if (account > 0 && dateToNextDeposit != null)
		{

			ViewBag.NumberOfAccounts = account;
			ViewBag.DateToNextDeposit = dateToNextDeposit;
			ViewBag.TotalAmountOwed = totalAmountOwed;
			ViewBag.DepositDue = depositDue;

		}
		else
		{
			ViewBag.NumberOfAccounts = 0;
			ViewBag.DateToNextDeposit = 0;
			ViewBag.TotalAmountOwed = 0;
			ViewBag.DepositDue = 0;

		}

		// get groupIds of active Accounts  
		var groupId = await _homeRequestService.MemberAccountGroupId(userId);




		var accountUser = _accountUserCRUDService.AccountUserExists(User.Identity.GetUserId());
		if (!accountUser)
		{
			return RedirectToAction("Create", "AccountUsers");
		}

		if (roleAccount)
		{
			return RedirectToAction("AdminIndex");
		}
		return View();
	}

	public async Task<string> GetData()
	{
		IEnumerable<Calendar> events = await _calendarRepository.GetAll();
		var eventData = events.Select(x => new Calendar { CalendarId = x.CalendarId, Title = x.Title, Start = x.Start, AllDay = x.AllDay, ClassName = x.ClassName, End = x.End, GroupId = x.GroupId, }).ToList();
		return Newtonsoft.Json.JsonConvert.SerializeObject(eventData);
	}

	[Authorize(Roles = "Admin")]
	public IActionResult AdminIndex()
	{
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
