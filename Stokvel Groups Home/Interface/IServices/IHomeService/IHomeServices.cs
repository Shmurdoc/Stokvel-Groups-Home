namespace Stokvel_Groups_Home.Interface.IServices.IHomeService
{
	public interface IHomeServices
	{

		Task<int> NumberOfAccounts(string id);
		Task<List<decimal>> TotalOwed(string id);

	}
}
