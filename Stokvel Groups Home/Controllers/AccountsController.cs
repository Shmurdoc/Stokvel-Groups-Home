using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.common.Alert.TempData;
using Stokvel_Groups_Home.Interface.IService;
using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IGroupServices;
using Stokvel_Groups_Home.Models;
using X.PagedList;

namespace Stokvel_Groups_Home.Controllers
{
	public class AccountsController : Controller
	{
		private readonly IAccountRequestService _accountRequestService;
		private readonly IAccountProfileRequestServices _accountProfileRequestServices;
		private readonly IAccountsCRUDService _accountsCRUDService;
		private readonly IAccountProfileCRUDService _accountProfileCRUDService;
		private readonly IGroupsCRUDService _groupsCRUDService;

		public AccountsController(
			IAccountRequestService accountRequestService,
			IAccountProfileRequestServices accountProfileRequestServices,
			IAccountsCRUDService accountsCRUDService,
			IAccountProfileCRUDService accountProfileCRUDService,
			IGroupsCRUDService groupsCRUDService
			)
		{

			_accountProfileRequestServices = accountProfileRequestServices;
			_accountRequestService = accountRequestService;
			_accountsCRUDService = accountsCRUDService;
			_accountProfileCRUDService = accountProfileCRUDService;
			_groupsCRUDService = groupsCRUDService;
		}

		// GET: Accounts
		[HttpGet]
		public async Task<IActionResult> IndexAdmin()
		{
			List<AccountType> accountTypes = new List<AccountType>();
            List<AccountType>? groupsType = new();
			List<UserGroupMember>? userGroupMembers = new();

            accountTypes = Enum.GetValues(typeof(AccountType))
			   .Cast<AccountType>()
			   .ToList();


			var profile = await _accountRequestService.PendingMembersInGroup();

			// loop for each to get the types of accounts
			foreach (var item in accountTypes) 
			{
				var groupExists = profile.Any(x =>x.Group.TypeAccount == item);
				var listOfGroup = profile.Where(x => x.Group.TypeAccount == item).FirstOrDefault();

				if (listOfGroup == null) continue;
				userGroupMembers.Add(listOfGroup);

                if (groupExists == true)
				{
					groupsType.Add(item);
                }
            }
			// list of type of accounts
            ViewBag.GroupExists = groupsType;

            return userGroupMembers != null ?
						  View(userGroupMembers) :
						  Problem("Entity set 'ApplicationDbContext.Accounts'  is null.");
		}

		// GET: Accounts
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var userId = User.Identity.GetUserId();

			ViewBag.AccountType = await _accountRequestService.ValidAccountTypesAdded(userId);

			ViewBag.AccountExists = await _accountRequestService.TypeAccountExists(userId);
			var validAccounts = await _accountRequestService.ValidAccountTypesAdded(userId);
			var profile = await _accountRequestService.CleanListAccountTypesAdded(validAccounts);

			return profile != null ?
						  View(profile) :
						  Problem("Entity set 'ApplicationDbContext.Accounts'  is null.");
		}

		[HttpGet]
		public async Task<IActionResult> Index1(AccountType? accountType, string? groupName, int groupId)
		{
            TempData["groupAMDId"] = groupId;
            TempData["groupName"] = groupName;
			TempData.Keep("groupName");
			TempData["accountType"] = accountType;
			TempData.Keep("accountType");
			string Id = User.Identity.GetUserId();
			int memberGroupId ;

            var accountIdOfGroups = await _accountRequestService.AccountIdForMemberInGroups(Id, accountType);
			if (User.IsInRole("Admin"))
			{
                memberGroupId = groupId;
            }
			else
			{
                memberGroupId = await _accountRequestService.CollectGroupIdInDb(accountIdOfGroups, groupName);
            }
		
			ViewBag.UserId = Id;
			var AccountIdList = await _accountRequestService.AcceptedGroupMembers(memberGroupId);
			ViewBag.AccountIdList = AccountIdList;
			ViewBag.m = groupName;
			ViewBag.at = accountType;
			ViewBag.image = "/wwwroot/images/Profile";

			string resultLocalPage;


            if (User.IsInRole("Admin"))
			{
				var groupList = await _groupsCRUDService.GetAll();
				var member = await _groupsCRUDService.GetById(groupId);
				var managerId = member.ManagerId;
				resultLocalPage = await _accountRequestService.TotalGroupMembersJoined(accountType, groupName, managerId);
			}
			else
			{
                resultLocalPage = await _accountRequestService.TotalGroupMembersJoined(accountType, groupName, Id);
            }
			 
			if (resultLocalPage != null)
			{
				return LocalRedirect(resultLocalPage);
			}

			var membersInGroup = await _accountRequestService.PendingMembersInGroup();
			membersInGroup = membersInGroup.Where(x => x.GroupMembers.Group.GroupId == memberGroupId).ToList();
			return View(membersInGroup);
		}
		[HttpGet]
		public async Task<IActionResult> AcceptedMembersDashboard(AccountType? accountType, String groupName)
		{
			TempData["accountType"] = accountType;
			TempData.Keep("accountType");

			int? groupAMDId = TempData["groupAMDId"] as int?;
            TempData.Keep("accountType");

            string? Id; 

            if (User.IsInRole("Admin"))
            {
                var groupList = await _groupsCRUDService.GetAll();
                var member = await _groupsCRUDService.GetById(groupAMDId);
                var managerId = member.ManagerId;
				Id = managerId;

            }
			else
			{
                Id = User.Identity.GetUserId();
            }

            var accountIdOfGroups = await _accountRequestService.AccountIdForMemberInGroups(Id, accountType);

			var profile = await _accountRequestService.MembersInGroup();
			var groupId = await _accountRequestService.CollectGroupIdInDb(accountIdOfGroups, groupName);
			ViewBag.AccountProfileId = await _accountRequestService.AccountProfileId(Id);
			ViewBag.GroupNames = groupName;
			ViewBag.UserId = Id;
			ViewBag.AccountType = accountType;
			ViewBag.GroupId = groupId;
			ViewBag.image = "/wwwroot/images/Profile";
			ViewBag.StokvelActive = profile.Any(x => x.Group.GroupId == groupId && x.Group.Active == true);


			//get which users paid and which didn't
			var memberDepositStatus = await _accountRequestService.MembersPaidFilter(groupId);
			List<int>? memberPaid = memberDepositStatus.MemberPaid;

			if (memberPaid != null)
			{
				ViewBag.MemberPaid = memberPaid;
			}
			else
			{
				ViewBag.MemberPaid = 0;
			}

			// accepted member list
			var AccountIdList = await _accountRequestService.AcceptedGroupMembers(groupId);

			ViewBag.AccountIdList = AccountIdList;
			profile = profile.Where(x => x.GroupMembers.Group.GroupId == groupId).ToList();

			return View(profile);
		}


		[HttpGet]
		public async Task<IActionResult> StartStokvel(int groupId, AccountType accountType, string groupName)
		{
			var userId = User.Identity.GetUserId();

			if (groupId > 0)
			{
				var status = await _accountRequestService.MembersPaidFilter(groupId);
				var accountIdOfGroups = status.Paid;
				var depositNotPaid = status.NotPaid;
				var userName = status.UserNames;
				var membersInDbCount = status.MembersinDbCount;
				var memberQueue = status.MemberQueue;
				var memberNotPaid = status.MemberNotPaid;
				var memberPending = status.MemberPending;
				var memberPaid = status.MemberPaid;
				List<string> AccountMessage = new();

				// if deposit doesn't exist the depositNotPaid will be triggered  
				if (depositNotPaid.Count > 0)
				{
					for (int i = 0; i < userName.Count; i++)
					{
						AccountMessage.Add(userName[i]);
					}
					foreach (var member in AccountMessage)
					{
						this.AddAlertDanger($"{member} Has Not Yet Paid The Deposit, Please Remind Member To Pay");
					}

					var resultStart = "/Accounts" + "/AcceptedMembersDashboard?" + "AccountType=" + accountType.ToString() + "&" + "GroupName=" + groupName;
					return LocalRedirect(resultStart);
				}

				if (accountIdOfGroups.Count == membersInDbCount)
				{
					// Reminder that MemberNotPaid is only for premium users
					memberQueue = await _accountProfileRequestServices.ArrangingStokvelQueue(memberNotPaid, memberPending, memberPaid, groupId);

					//start stokvel after all conditions are met
					await _accountRequestService.StartStokvel(memberQueue, groupId);

				}
			}
			else
			{
				string status = status = "Failed!";
				this.AddAlertDanger($"{status} Something went wrong, Pleace try again.");
			}
			var resultDone = "/Accounts" + "/AcceptedMembersDashboard?" + "AccountType=" + accountType.ToString() + "&" + "GroupName=" + groupName;
			return LocalRedirect(resultDone);
		}

		[HttpGet]
		public async Task<IActionResult> Index2(AccountType accountType)
		{
			var user = User.Identity.GetUserId();
			List<int> listAccountID = new();


			//get accountId List
			var memberOfGroup = await _accountRequestService.MembersOfGroup(user, accountType);
			
			if (User.IsInRole("Admin"))
			{
				var membersInDb =  await _groupsCRUDService.GetAll();

				var groupManagerId = membersInDb.Select(x => x.ManagerId).ToList();

				foreach(var item in groupManagerId)
				{
                    var memberAccountId = await _accountRequestService.MembersOfGroup(item, accountType);
					foreach (var accountId in memberAccountId)
					{
						listAccountID.Add(accountId);
					}
                }
				var allAccountList = listAccountID.Distinct();
                ViewBag.Member = allAccountList;
            }
			else
			{
                ViewBag.Member = memberOfGroup;
            }
			var profile = await _accountRequestService.PendingMembersInGroup();
			return profile != null ?
						  View(profile) :
						  Problem("Entity set 'ApplicationDbContext.Accounts'  is null.");
		}


		// GET: Accounts/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var account = await _accountsCRUDService.GetById(id);
			if (id == null || account == null)
			{
				return NotFound();
			}

			return View(account);
		}

		// GET: Accounts/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Accounts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("AccountId,Id,GroupVerifyKey,AccountCreated")] Account? account)
		{


			var userId = User.Identity.GetUserId();
			var accountExists = _accountsCRUDService.AccountExists(userId, account.GroupVerifyKey);
			var exists = _groupsCRUDService.GroupExists(account.GroupVerifyKey);
			var groupId = await _groupsCRUDService.GetGroupId(account.GroupVerifyKey);
			GroupMembers groupMembers = new();


			if (!accountExists)
			{
				if (exists)
				{
					// check if user has AccountProfile 
					List<AccountProfile> accountProfiles = await _accountProfileCRUDService.GetAll();
					if (!accountProfiles.Any(x => x.Id == userId))
					{
						AccountProfile? accountProfile = new()
						{
							Id = userId,
							GroupsJoined = 1,
							GroupsLeft = 0,
							EmergencyCancel = 0,
							StatusRank = MemberStatuses.Bronz,
							MembershipRank = 0,
							TotalAmoutDeposited = 0,
							TotalPenaltyFee = 0,
							GroupWarnings = 0,
						};

						await _accountProfileCRUDService.Inset(accountProfile);
						await _accountProfileCRUDService.SaveAsync();
					}
					else
					{
						var updateProfile = accountProfiles.Where(x => x.Id == userId).ToList();
						var groupsJoined = updateProfile.Select(x => x.GroupsJoined).FirstOrDefault();
						AccountProfile? accountProfile = new()
						{
							Id = userId,
							GroupsJoined = groupsJoined + 1,
						};


						await _accountProfileCRUDService.Edit(accountProfile);
						await _accountProfileCRUDService.SaveAsync();
					}

					account.Id = userId;
					account.AccountCreated = DateTime.Now;
					if (account is not null)
					{
						await _accountsCRUDService.Insert(account);
					}


					return RedirectToAction(nameof(Index));
				}
				else
				{

					ModelState.MarkFieldValid("GroupVerifyKey");
					ModelState.AddModelError("GroupVerifyKey", "Group Does Not Exist, Please Fill in the correct Group Verification Key");
				}
			}
			else
			{
				ModelState.ClearValidationState("GroupVerifyKey");
				ModelState.AddModelError("GroupVerifyKey", "Verify Key Has Already Been Used");
			}
			return View(account);
		}


		// GET: Accounts/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id)
		{
			var account = await _accountsCRUDService.GetById(id);

			if (id == null || account == null)
			{
				return NotFound();
			}
			return View(account);
		}

		// POST: Accounts/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("AccountId,Id,GroupVerifyKey,AccountCreated")] Account account)
		{
			if (id != account.AccountId)
			{
				return NotFound();
			}

			account.Accepted = true;

			if (ModelState.IsValid)
			{
				try
				{
					await _accountsCRUDService.Edit(account);
					await _accountsCRUDService.SaveAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!Exists(account.AccountId))
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
			return View(account);
		}

		// GET: Accounts/Delete/5
		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var account = await _accountsCRUDService.GetById(id);
			if (id == null || account == null)
			{
				return NotFound();
			}

			return View(account);
		}

		// POST: Accounts/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{

			var account = await _accountsCRUDService.GetById(id);
			if (account == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Accounts'  is null.");
			}

			await _accountsCRUDService.Delete(id);
			await _accountsCRUDService.SaveAsync();
			return RedirectToAction(nameof(Index));
		}



		[HttpGet]
		public async Task<IActionResult> AccptedMembers(int id, string groupName, int groupId, AccountType accountType, bool acceptStatus)
		{


			var AccountIdList = await _accountRequestService.AcceptedGroupMembers(groupId);



			if (AccountIdList.Count <= 12)
			{
				{

					var account = await _accountsCRUDService.GetById(id);

					if (account == null)
					{
						return NotFound();
					}

					account.Accepted = acceptStatus;

					try
					{
						await _accountRequestService.AcceptGroupMember(account);
						string status = "Success!";

						TempData["AcceptStatus"] = acceptStatus;
						//Error Message
						if (acceptStatus == true)
						{

							this.AddAlertSuccess($"{status} A new member has been added successful.");
						}

						if (acceptStatus == false)
						{

							this.AddAlertSuccess($"{status} A member has been removed successful.");
						}
					}
					catch (DbUpdateConcurrencyException)
					{

						if (!Exists(id))
						{
							//return NotFound();

						}
						else
						{
							string status = status = "Failed!";
							this.AddAlertDanger($"{status} Something went wrong, Please try again.");
							throw;

						}
					}

				}
				groupName = groupName.Replace(" ", "+");
				var result = "/Accounts" + "/Index1?" + "AccountType=" + accountType.ToString() + "&" + "GroupName=" + groupName;
				return LocalRedirect(result);


			}

			return View();
		}



		private bool Exists(int? id)
		{
			return (_accountsCRUDService.Exists(id));
		}



		[HttpGet]
		public async Task<IActionResult> JoinGroup(string key)
		{
			Account account = new()
			{
				Id = User.Identity.GetUserId(),
				GroupVerifyKey = key,
				AccountCreated = DateTime.Now
			};


			var details = await _accountsCRUDService.GetByUserId(User.Identity.GetUserId());
			var exists = details.Any(x => x.GroupVerifyKey == key);

			if (exists)
			{
				await this.Create(account);

			}
			else
			{
				// Add Error message
				string status = status = "Failed!";
				this.AddAlertDanger($"{status} Something went wrong, Please try again.");

			}



			return RedirectToAction("Index", "Groups");

		}


		public class MyModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}

		[HttpPost]
		public JsonResult SaveMyModel([FromBody] MyModel model)
		{
			var b = model;
			// Process and save the model data
			return Json(new { success = true, message = "Data saved successfully" });
		}




	}

}
