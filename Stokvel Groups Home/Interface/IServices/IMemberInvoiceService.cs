using Stokvel_Groups_Home.Models.MainTable;

namespace Stokvel_Groups_Home.Interface.IServices
{
	public interface IMemberInvoiceService
	{
		Task<List<MemberDepositAccount>>? GetMemberDepositAccountList();
	}
}
