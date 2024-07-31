using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IHomeService
{
    public interface IHomeRequestService
	{

		Task<List<DisplayPaidMember>> CreditedMembers(List<int> groupId);

		Task<List<int>> MemberAccountGroupIdWithdrawal(string userId);
		Task<List<int>> MemberAccountGroupId(string userId);

		Task<IEnumerable<DisplayMemberTurn>> DisplayRecentDeposit();
		Task<IEnumerable<DisplayMemberTurn>> MemberQueueList();
		Task<int> NumberOfAccounts(string id);
		Task<List<DateOnly>> DateToNextDeposit(string id);
		Task<List<decimal>> TotalOwed(string id);
		Task<List<decimal>> TotalAmountDue(string id);

	}
}
