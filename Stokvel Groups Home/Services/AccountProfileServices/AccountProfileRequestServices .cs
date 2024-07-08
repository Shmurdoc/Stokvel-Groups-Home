using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;

namespace Stokvel_Groups_Home.Services.AccountProfileServices;

public class AccountProfileRequestServices : IAccountProfileRequestServices
{
	private readonly IAccountProfileServices _accountProfileServices;
	public AccountProfileRequestServices(IAccountProfileServices accountProfileServices)
	{
		_accountProfileServices = accountProfileServices;
	}
	public async Task<List<int>> ArrangingStokvelQueue(List<int> MemberNotPaid, List<int> MemberPending, List<int> MemberPaid, int groupId)
	{

		//Join Tables into one 
		List<int> paidGroup = Enumerable.Concat(MemberPending, MemberPaid).ToList();
		List<int> comboGroups = Enumerable.Concat(MemberNotPaid, paidGroup).ToList();

		// get list in order of All member in List
		var memberList = await _accountProfileServices.ListIdInOrder(comboGroups);

		// MemberNotPaid
		var pendingMember = await _accountProfileServices.ByGroupFilter(memberList, MemberNotPaid, groupId);


		//MemberPending
		var memberPending = await _accountProfileServices.ByGroupFilter(memberList, MemberNotPaid, groupId);


		//MemberPaid
		var memberPaid = await _accountProfileServices.ByGroupFilter(memberList, MemberPaid, groupId);

		List<int> members = Enumerable.Concat(pendingMember, memberPending).ToList();
		List<int> joinedMembers = Enumerable.Concat(members, memberPaid).ToList();


		return joinedMembers;


	}





}