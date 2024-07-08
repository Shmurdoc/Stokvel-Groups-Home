using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IAccountServices.IAccountRequestService;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.ViewComponents;

public class MemberTurnProfileViewComponent : ViewComponent
{

	private readonly IAccountRequestService _accountRequestService;

	public MemberTurnProfileViewComponent(IAccountRequestService accountRequestService)
	{
		_accountRequestService = accountRequestService;
	}
	[HttpGet]
	public async Task<IViewComponentResult> InvokeAsync()
	{
		AccountType? accountType = TempData["accountType"] as AccountType?;
		TempData.Keep("accountType");
		string? groupName = TempData["groupName"] as string;
		TempData.Keep("groupName");

		decimal Deposited = 0;
		string Id = User.Identity.GetUserId();
		var profileL = await _accountRequestService.MembersInGroup();
		var accountIdOfGroups = await _accountRequestService.AccountIdForMemberInGroups(Id, accountType);
		var groupId = await _accountRequestService.CollectGroupIdInDb(accountIdOfGroups, groupName);

		// Display the next member turn profile
		var memberTurn = await _accountRequestService.DisplayMemberTurnProfile();
		var groupTargetAmount = profileL.Where(x => x.GroupMembers.GroupId == groupId).Select(x => x.GroupMembers.Group.AccountTarget).FirstOrDefault();
		decimal numberOfMembers = profileL.Where(x => x.GroupMembers.GroupId == groupId).Count() - 1;
		var namesInDb = memberTurn.Where(x => x.GroupMembers.GroupId == groupId)
			.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month + 1).ToList();

		if (memberTurn.Any(x => x.GroupMembers.Group.Active == true))
		{
			var firstName = memberTurn.Select(x => x.AccountUser.FirstName).FirstOrDefault().ToString();
			var lastName = memberTurn.Select(x => x.AccountUser.LastName).FirstOrDefault().ToString();

			var memberNames = firstName + " " + lastName;
			var memberNumber = memberTurn.Where(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month + 1).Select(x => x.Account.AccountQueue).FirstOrDefault();

			ViewBag.MemberTurnProfileNames = memberNames;
			ViewBag.MemberNames = memberNames;

			Deposited = memberTurn.Where(x => x.Deposit.DepositDate.Month == DateTime.Now.Month && x.Deposit.DepositReference == firstName).Sum(x => x.Deposit.DepositAmount) * -1;
			ViewBag.DepositedAmount = Deposited;

			var memberDateTurn = memberTurn.Any(x => x.Account.AccountQueueStart.Month == DateTime.Now.Month);

			var outstandingAmount = (groupTargetAmount / numberOfMembers) + Deposited;
			if (memberDateTurn == false)
			{
				outstandingAmount = 0;
			}
			ViewBag.OutstandingAmount = outstandingAmount;
			ViewBag.GroupTargetAmount = groupTargetAmount;
		}
		else
		{
			ViewBag.MemberNames = null;
			return View();
		}

		return await Task.FromResult<IViewComponentResult>(View(profileL));
	}

}
