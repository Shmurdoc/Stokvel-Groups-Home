namespace Stokvel_Groups_Home.Interface.IServices.PrepaymentServices
{
	public interface IPrepaymentServices
	{


		Task<decimal> PrepaymentDeposit(int? accountId);
		Task<decimal> DepositTotal(int? groupId);
	}
}
