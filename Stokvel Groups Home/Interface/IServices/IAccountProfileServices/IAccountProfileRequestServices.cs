namespace Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;

public interface IAccountProfileRequestServices
{

	Task<List<int>> ArrangingStokvelQueue(List<int> memberNotPaid, List<int> memberPending, List<int> memberPaid, int groupId);

}
