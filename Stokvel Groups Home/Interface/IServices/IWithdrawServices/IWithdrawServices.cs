using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;


public interface IWithdrawServices
{
	Task<List<DisplayPaidMember>> PaidMember();
	Task<int> CreditAccount(InvoiceDetails invoiceDetails);
	Task<int> groupId(int accountId);
}
