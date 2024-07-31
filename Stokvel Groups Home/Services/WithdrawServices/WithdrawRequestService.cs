using Stokvel_Groups_Home.Interface.IServices.IAccountServices;
using Stokvel_Groups_Home.Interface.IServices.IWithdrawServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.WithdrawServices;

public class WithdrawRequestService : IWithdrawRequestService
{

	private readonly IWithdrawServices _withdrawServices;
	private readonly IAccountServices _accountServices;

	public WithdrawRequestService(IWithdrawServices withdrawServices, IAccountServices accountServices)
	{
		_withdrawServices = withdrawServices;
		_accountServices = accountServices;
	}

	public async Task<List<DisplayPaidMember>> PaidMember() => await _withdrawServices.PaidMember();

	public async Task<List<int>> accountIdList(int accountId)
	{

		List<DisplayPaidMember> displayPaidMembers = await _withdrawServices.PaidMember();

		var groupId = displayPaidMembers.Where(x => x.Account.AccountId == accountId).Select(x => x.GroupMembers.GroupId).FirstOrDefault();

		var memberList = displayPaidMembers.Where(x => x.GroupMembers.GroupId == groupId).Select(x => x.GroupMembers.AccountId).ToList();

		return memberList;
	}

	public async Task<List<DisplayMemberTurn>> ListOfPaidMembers(int accountId)
	{
		var groupId = await _withdrawServices.groupId(accountId);

		var memberList = await _accountServices.NextPaidMember();

		memberList = memberList.Where(x => x.GroupMembers.GroupId == groupId).ToList();

		return memberList;
	}

	public async void CreditMember(WithdrawDetails invoiceDetails)
	{
		await _withdrawServices.CreditAccount(invoiceDetails);
	}

}
