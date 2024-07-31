using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.common.Alert.TempData;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Interface.IServices.IDepositServices;
using Stokvel_Groups_Home.Interface.IServices.IPrepaymentServices;
using Stokvel_Groups_Home.Interface.IServices.IWalletServices;
using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Services.WalletServices;

namespace Stokvel_Groups_Home.Controllers
{
	public class DepositsController : Controller
	{


	
		private readonly IPrepaymentsCRUDService _prepaymentsCRUDService;
		private readonly IWalletRequestServices _walletRequestServices;

		private readonly IDepositCRUDService _depositCRUDService;
		private readonly IDepositRequestService _depositRequestService;
		private readonly IAccountRequestService _accountRequestService;
		private readonly IWalletCRUDService _walletCRUDService;

		public DepositsController(
			
			IDepositRequestService depositRequestService,
			IDepositCRUDService depositCRUDService,
			IPrepaymentsCRUDService prepaymentsCRUDService,
			IWalletRequestServices walletRequestServices,
			IAccountRequestService accountRequestService,
			IWalletCRUDService walletCRUDService

			)
		{

			
			_prepaymentsCRUDService = prepaymentsCRUDService;
			_depositRequestService = depositRequestService;
			_depositCRUDService = depositCRUDService;
			_walletRequestServices = walletRequestServices;
			_accountRequestService = accountRequestService;
			_walletRequestServices = walletRequestServices;
		}

		// GET: Deposits
		public async Task<IActionResult> Index()
		{

			var listAllDeposits = await _depositCRUDService.GetAll();
			return View(listAllDeposits);
		}

		// GET: Deposits/Details/5
		public async Task<IActionResult> Details(int id)
		{
			var deposit = await _depositCRUDService.Details(id);

			if (deposit == null)
			{
				return NotFound();
			}
			return View(deposit);
		}

		// GET: Deposits/Create
		[HttpGet]
		public async Task<IActionResult> Create(int accountId, int accountProfileId, string groupName)
		{
            decimal preDeposit = 0;
			var memberPrepaymentDeposit = await _depositRequestService.PreDeposit(accountId);
			var AccountProfile = await _depositRequestService.Details(accountProfileId);
			if (AccountProfile != null)
			{
				ViewBag.MembershipRank = AccountProfile.MembershipRank;
				ViewBag.StatusRank = AccountProfile.StatusRank;
				ViewBag.TotalAmountDeposit = AccountProfile.TotalAmoutDeposited;
			}
			else
			{
				ViewBag.MembershipRank = 0;
				ViewBag.StatusRank = 0;
				ViewBag.TotalAmountDeposit = 0;
			}
			var memberAmount = await _walletRequestServices.MemberWalletAmount(accountId);
			if (memberAmount != 0)
			{
				ViewBag.MemberAmount = memberAmount;
			}
			else
			{
				ViewBag.MemberAmount = 0;
			}

			var groupMembers = await _accountRequestService.PendingMembersInGroup();

			var totalMembers = groupMembers.Where(x => x.Account.AccountId == accountId).Select(x => x.Group.TotalGroupMembers).FirstOrDefault();
			var groupTarget = groupMembers.Where(x => x.Account.AccountId == accountId).Select(x => x.Group.AccountTarget).FirstOrDefault() / (totalMembers-1);
			var groupStatus = groupMembers.Where(x => x.Account.AccountId == accountId).Select(x => x.Group.Active).FirstOrDefault();
            if (memberPrepaymentDeposit != null)
			{
				ViewBag.PreDepoAmount = memberPrepaymentDeposit.Select(x => x.Amount).FirstOrDefault();
                ViewBag.PrepaymentId = memberPrepaymentDeposit.Select(x => x.PrepaymentId).FirstOrDefault();
            }
			else
			{
				ViewBag.PreDepoAmount = 0;
                ViewBag.PrepaymentId = 0;
            }
            ViewBag.accountId = accountId;
            ViewBag.AccountProfileId = accountProfileId;
            ViewBag.MemberTarget = groupTarget;
            ViewBag.GroupNames = groupName;
			ViewBag.GroupStatus = groupStatus;



            return View();
		}

		

		// POST: Deposits/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("DepositId,InvoiceId,DepositAmount,DepositDate,MethodId,PaymentStatusId,DepositReference,GroupVerifyKey")] int accountId, Deposit deposit, int accountProfileId, AccountType accountType, string groupName, int membershipRank, decimal totalAmountDeposit, MemberStatuses statusRank, decimal memberTarget, decimal MemberAmount, int prePaymentId, decimal preDepoAmount)
		{
			var userId = User.Identity.GetUserId();
			int preDepoId = 0;
			decimal preDepositTotal = 0;



		repeat:
			


			if (memberTarget == preDepoAmount || preDepoId > 0)
			{


				if (ModelState.IsValid)
				{
					try
					{

							if (memberTarget == MemberAmount + deposit.DepositAmount || memberTarget < MemberAmount)
							{
								var groupMembers = await _accountRequestService.DisplayMemberTurnProfile();
								var numberOfMembers = groupMembers.Where(x => x.Account.AccountId == accountId).Select(x => x.GroupMembers.Group.TotalGroupMembers).FirstOrDefault() - 1;

								var memberInDb = await _walletCRUDService.Details(accountId);
								var amount = memberInDb.Amount;

								decimal newAmount;

								Wallet? wallet = new Wallet();
								Deposit walletTodeposit = new Deposit();

								if (MemberAmount > memberTarget)
								{
									newAmount = MemberAmount - memberTarget;

									wallet.AccountId = accountId;
									wallet.Amount = newAmount;

									await _walletCRUDService.Edit(wallet);

									var walletDepo = await _depositRequestService.DepositMonthly(accountId, -memberTarget, preDepoAmount);
									deposit.DepositAmount = walletDepo;

								}
								else
								{
									newAmount = MemberAmount;

									wallet.AccountId = accountId;
									wallet.Amount = amount - MemberAmount;

									await _walletCRUDService.Edit(wallet);

									var walletDepo = await _depositRequestService.DepositMonthly(accountId, -newAmount, preDepoAmount);
									deposit.DepositAmount = walletDepo;

								}
								//get deposit reference 
								deposit.DepositReference = await _depositRequestService.ReferenceName(accountId);
								await _depositRequestService.MemberDepositMade(accountId, userId, deposit, accountProfileId, membershipRank, totalAmountDeposit, statusRank, preDepoAmount);

							}
							else
							{
								//get deposit reference 
								deposit.DepositReference = await _depositRequestService.ReferenceName(accountId);
							if (deposit.DepositReference != null)
							{
                                string status = "Success!";
                                this.AddAlertSuccess($"{status} Your Deposit was successful.");
                                await _depositRequestService.MemberDepositMade(accountId, userId, deposit, accountProfileId, membershipRank, totalAmountDeposit, statusRank, preDepoAmount);
							}
							else
							{
                                string status = "Failed!";
                                this.AddAlertDanger($"{status} The Stokvel has not yet begun; please try again when it is active.");
                                return RedirectToAction(nameof(Index));
                            }
                        }
						
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!DepositExists(deposit.DepositId))
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

			}
			else
			{
				if (memberTarget != preDepositTotal && accountId != 0)
					try
					{
						var depo = await _depositRequestService.MembersFirstPreDeposit(accountId, prePaymentId, preDepoAmount, deposit.DepositAmount);
						preDepoId = depo.PrepaymentId;
						deposit.DepositAmount = depo.Amount;
						goto repeat;
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!DepositExists(deposit.DepositId))
						{
                            return RedirectToAction(nameof(Index));
                        }
						else
						{
							
                            return RedirectToAction(nameof(Index));
                        }
					}
				else
				{
					string status = status = "Failed!";
					this.AddAlertDanger($"{status} Something went wrong, Pleace try again.");
					var resultDone = "/Accounts" + "/AcceptedMembersDashboard?" + "AccountType=" + accountType.ToString() + "&" + "GroupName=" + groupName;
					return LocalRedirect(resultDone);
				}

			}
			
			return RedirectToAction(nameof(Index));
		}

		// GET: Deposits/Edit/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult>? Edit(int id)
		{
			var editDepositInDb = _depositCRUDService.Details(id);
			if (id == null || await editDepositInDb == null)
			{
				return NotFound();
			}

			
			return View(editDepositInDb);
		}

		// POST: Deposits/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("DepositId,InvoiceId,DepositAmount,DepositDate,PrepaymentId,MethodId,PaymentStatusId,DepositReference")] Deposit deposit)
		{
			if (id != deposit.DepositId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _depositCRUDService.Edit(deposit);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DepositExists(deposit.DepositId))
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
			
			return View(deposit);
		}

		// GET: Deposits/Delete/5
		[HttpGet]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var displayDelDepositInDb = _depositCRUDService.Details(id);
			if (id == null || displayDelDepositInDb == null)
			{
				return NotFound();
			}


			return View(displayDelDepositInDb);
		}

		// POST: Deposits/Delete/5
		[HttpPost, ActionName("Delete")]
		[Authorize(Roles = "Admin")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var delDepositInDb = await _depositCRUDService.Details(id);
			if (id == null || delDepositInDb == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Deposits'  is null.");
			}

			await _depositCRUDService.Delete(id);
			return RedirectToAction(nameof(Index));
		}

		private bool DepositExists(int id)
		{
			var exists = _depositCRUDService.DepositExists(id);
			return (exists);
		}
	}
}
