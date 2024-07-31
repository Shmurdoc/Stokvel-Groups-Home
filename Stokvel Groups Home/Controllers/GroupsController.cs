using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.common.Alert.TempData;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IAccountUserServices;
using Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Models;
using X.PagedList;

namespace Stokvel_Groups_Home.Controllers
{
	public class GroupsController : Controller
	{

		private readonly IAccountsCRUDService _accountsCRUDService;
		private readonly IGroupMembersCRUDService _groupMembersCRUDService;
		private readonly IGroupMembersRequestServices _groupMembersRequestServices;
		private readonly IGroupRequestServices _groupGroupRequestServices;
		private readonly IGroupsCRUDService _groupsCRUDService;
		private readonly IAccountUserCRUDService _accountUserCRUDService;

		private readonly IAccountRequestService _accountRequestService;

		public GroupsController(ApplicationDbContext context,
			IGroupRequestServices groupRequestServices,
			IAccountsCRUDService accountsCRUDService,
			IGroupMembersCRUDService groupMembersCRUDService,
			IAccountRequestService accountRequestService,
			IGroupsCRUDService groupsCRUDService,
			IGroupMembersRequestServices groupMembersRequestServices,
            IAccountUserCRUDService accountUserCRUDService

            )
		{


			_groupGroupRequestServices = groupRequestServices;
			_accountsCRUDService = accountsCRUDService;
			_groupMembersCRUDService = groupMembersCRUDService;
			_accountRequestService = accountRequestService;
			_groupsCRUDService = groupsCRUDService;
			_groupMembersRequestServices = groupMembersRequestServices;
            _accountUserCRUDService = accountUserCRUDService;
		}

		// GET: Groups
		public async Task<IActionResult> AdminIndex(string sortOrder, string currentFilter, string searchString, int? page)
		{
			if (!User.IsInRole("Admin"))
			{
				return View(RedirectToAction("Index", "Home"));

			}
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

			var accountUsers = await _groupGroupRequestServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);

			return View(accountUsers.ToPagedList());
		}

		// GET: Groups
		[HttpGet]
		public async Task<IActionResult> MyIndex(string sortOrder, string currentFilter, string searchString, int? page)
		{
            var members = await _accountRequestService.PendingMembersInGroup();

            

            var userId = User.Identity.GetUserId();
            var accountUser = await _accountUserCRUDService.GetById(userId);
            var byId = await _accountRequestService.accountIdList(userId);
			var groupMember = await _groupMembersCRUDService.GetAll();
            var accountIdList = await _groupMembersRequestServices.GroupIdList(userId);

			List<int> groupIdList = new();
			



            foreach(var id in accountIdList)
			{
				var memberAccountId = groupMember.Where(x => x.AccountId == id).Select(x => x.GroupId).FirstOrDefault();
				groupIdList.Add(memberAccountId);
            }

            ViewBag.image = "~/wwwroot/images/Profile";
            ViewBag.MemberPhotoPath = accountUser.MemberPhotoPath;
            ViewBag.GroupIdList = groupIdList;
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

			var accountUsers = await _groupGroupRequestServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);
			


			return View(accountUsers.ToPagedList());
		}

		[HttpGet]
		public async Task<IActionResult> PrivateIndex(string sortOrder, string currentFilter, string searchString, int? page)
		{
			var userId = User.Identity.GetUserId();


            var accountUser = await _accountUserCRUDService.GetById(userId);

            ViewBag.image = "~/wwwroot/images/Profile";
            ViewBag.MemberPhotoPath = accountUser.MemberPhotoPath;
            ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.UserId = userId;

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			ViewBag.CurrentFilter = searchString;

			var accountUsers = await _groupGroupRequestServices.FilterAccountUsers(sortOrder, currentFilter, searchString, page);

			return View(accountUsers.ToPagedList());
		}

		
		// GET: Groups
		public async Task<IActionResult> ManagerIndex()
		{
			var userId = User.Identity.GetUserId();
			var groups = await _accountsCRUDService.GetByUserId(userId);
			var groupsInDb = await _groupMembersCRUDService.GetAll();
			var allGroupsCreated = await _groupsCRUDService.GetAll();
            var accountUser = await _accountUserCRUDService.GetById(userId);
			List<int> groupIdList = new();

			foreach (var item in groups)
			{
				var result = groupsInDb.Where(x => x.AccountId == item.AccountId).Select(x => x.GroupId).FirstOrDefault();
				groupIdList.Add(result);

			}
            ViewBag.image = "~/wwwroot/images/Profile";
			ViewBag.MemberPhotoPath = accountUser.MemberPhotoPath;
            ViewBag.GroupIdList = groupIdList;

			return View(allGroupsCreated);
		}

		// GET: Groups/Details/5
		public async Task<IActionResult> Details(int? id)
		{

			var group = await _groupsCRUDService.Details(id);
			if (id == null || group == null)
			{
				return NotFound();
			}

			return View(group);
		}

		// GET: Groups/Create
		public IActionResult Create()
		{
			var userId = User.Identity.GetUserId();
			ViewBag.Id = userId;
			return View();
		}

		// POST: Groups/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("GroupId,ManagerId,GroupName,VerifyKey,TypeAccount,TotalGroupMembers,GroupDate,AccountTarget,GroupStatus,Private")] Group @group)
		{
			var userId = User.Identity.GetUserId();
			var groupProfile = await _accountRequestService.PendingMembersInGroup();
			var accountUser = await _accountUserCRUDService.GetById(userId);

			var groupExists = groupProfile.Any(x => x.GroupMembers.Group.VerifyKey == @group.VerifyKey || x.GroupMembers.Group.GroupName == @group.GroupName);

			if (groupExists != true)
			{
				if (ModelState.IsValid)
				{
					Account account = new();


					List<int> MemberList = new();

					var accountList = await _accountsCRUDService.GetByUserId(userId);

					var accountAccpted = _accountsCRUDService.GetAll();
					List<int> countAccountUsers = new();

					foreach (var item in accountList)
					{
						if (item == null) continue;
						var accountId = accountList.Where(x => x.Id == item.Id && x.Accepted == true).Select(x => x.AccountId).FirstOrDefault();
						MemberList.Add(accountId);

					}

					if (MemberList.Count < 2)
					{
                        // Create Group
                        @group.ManagerId = userId;
                        @group.GroupDate = DateTime.Now;
						@group.Private = true;
						@group.GroupImage = accountUser.MemberFileName;
                        @group.GroupStatus = true;
                        await _groupsCRUDService.Inset(@group);
						await _groupsCRUDService.SaveAsync();

						account.Id = userId;
						account.GroupVerifyKey = group.VerifyKey;
						account.AccountCreated = DateTime.Now;
						account.Accepted = true;

						try
						{
							// Create Manager Account Accepted
							TempData["GroupStatusCreate"] = true;
							string status = "Success!";
							this.AddAlertSuccess($"{status} You have created a new group successfully.");
							await _accountsCRUDService.Insert(account);
						}
						catch (DbUpdateConcurrencyException)
						{
							string status = "Failed!";
							if (!GroupExists(@group.VerifyKey))
							{

								this.AddAlertSuccess($"{status} Please try again.");
								throw;
							}
							else
							{
								this.AddAlertSuccess($"{status} something went wrong.");
							}
						}

						return RedirectToAction("Index", "Accounts");
					}
					else
					{
						string status = "Failed!";
						this.AddAlertDanger($"{status} You Have Reached Your Limit Of Creating New Groups At Your Current Level.");
					}

				}
			}
			else
			{
				string status = "Failed!";
				this.AddAlertSuccess($"{status} Group Already Exists. Place change Group Name and VerifyKey");
			}
			return View(@group);
		}

		// GET: Groups/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{

			var @group = await _groupsCRUDService.Details(id);
			if (id == null || @group == null)
			{
				return NotFound();
			}
			return View(@group);
		}

		// POST: Groups/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("GroupId,ManagerId,GroupName,VerifyKey,TypeAccount,TotalGroupMembers,GroupDate,AccountTarget,GroupStatus,Private")] Group @group)
		{
			if (id != @group.GroupId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var userId = User.Identity.GetUserId();
                    var accountUser = await _accountUserCRUDService.GetById(userId);
                    @group.ManagerId = userId;
                    @group.GroupDate = DateTime.Now;
                    @group.Private = true;
                    @group.GroupImage = accountUser.MemberFileName;
                    @group.GroupStatus = true;
                    await _groupsCRUDService.Edit(group);
					await _groupsCRUDService.SaveAsync();
					string status = "Success!";
					this.AddAlertSuccess($"{status} You have successfuly updated a document.");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GroupExists(@group.VerifyKey))
					{
						string status = "Failed!";
						this.AddAlertDanger($"{status} Something went wrong, data was not saved.");
						return RedirectToAction(nameof(Edit));
					}
					else
					{
						string status = "Failed!";
						this.AddAlertDanger($"{status} Something went wrong.");
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(@group);
		}

		// GET: Groups/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{

			var @group = await _groupsCRUDService.Details(id);
			if (id == null || @group == null)
			{
				return NotFound();
			}

			return View(@group);
		}

		// POST: Groups/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var account = await _groupMembersCRUDService.GetAll();

			account = account.Where(x => x.GroupId == id).ToList();
			var @group = await _groupsCRUDService.Details(id);
			if (@group == null)
			{
				string warning = "Warning!";
				this.AddAlertWarning($"{warning} Entity set 'ApplicationDbContext.Groups'  is null.");
				return Problem("Entity set 'ApplicationDbContext.Groups'  is null.");
			}


			foreach (var item in account)
			{
				await _accountsCRUDService.Delete(item.AccountId);
				await _accountsCRUDService.SaveAsync();

			}
			await _groupsCRUDService.Delete(id);
			await _groupsCRUDService.SaveAsync();
			string status = "Success!";
			this.AddAlertSuccess($"{status} You have deleted a group sucsessfuly. All members in this group were deleted successfuly .");

			return RedirectToAction(nameof(Index));
		}

		private bool GroupExists(string id)
		{

			var exists = _groupsCRUDService.GroupExists(id);
			return exists;
		}
	}
}
