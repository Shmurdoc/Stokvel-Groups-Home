using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Stokvel_Groups_Home.Interface.IServices.IHomeService;
namespace Stokvel_Groups_Home.ViewComponents
{
	public class PaidMembersViewComponent : ViewComponent
	{
		private readonly IHomeRequestService _homeRequestService;

		public PaidMembersViewComponent(IHomeRequestService homeRequestService)
		{
			_homeRequestService = homeRequestService;
		}

		// GET: DisplayPaidMemberController
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var userId = User.Identity.GetUserId();
			var groupId = await _homeRequestService.MemberAccountGroupIdWithdrawal(userId);

			var printList = await _homeRequestService.CreditedMembers(groupId);
			return await Task.FromResult<IViewComponentResult>(View(printList));
		}

	}
}
