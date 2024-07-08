using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IWithdrawServices
{
	public interface IWithdrawRequestService
	{
		Task<List<DisplayPaidMember>> PaidMember();
		Task<List<int>> accountIdList(int accountId);
		void CreditMember(InvoiceDetails invoiceDetails);
		Task<List<DisplayMemberTurn>> ListOfPaidMembers(int accountId);
	}
}
