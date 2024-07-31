using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.PrepaymentServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.PrepaymentsServices
{
	public class PrepaymentServices : IPrepaymentServices
	{
		private readonly IPrepaymentsRepository _prepaymentsRepository;
		private readonly IGroupsRepository _groupsRepository;
		public PrepaymentServices(IPrepaymentsRepository prepaymentsRepository, IGroupsRepository groupsRepository)
		{
			_prepaymentsRepository = prepaymentsRepository;
			_groupsRepository = groupsRepository;
		}

		// get how much user has deposited.
		public async Task<decimal> PrepaymentDeposit(int? accountId)
		{

			List<PreDeposit> prepayments = await _prepaymentsRepository.GetAll();
			var prepaymentId = prepayments.Where(x => x.AccountId == accountId).Select(x => x.PrepaymentId).FirstOrDefault();
			decimal result = prepayments.Where(x => x.PrepaymentId == prepaymentId).Select(x => x.Amount).FirstOrDefault();

			return result;
		}
		// how much a member needs to pay
		public async Task<decimal> DepositTotal(int? groupId)
		{
			List<Group> groups = await _groupsRepository.GetAll();

			var groupTotalPayout = groups.Where(x => x.GroupId == groupId).Select(x => x.AccountTarget).FirstOrDefault();
			var numberOfGroupMembers = groups.Where(x => x.GroupId == groupId).Select(x => x.TotalGroupMembers).FirstOrDefault();
			decimal TotalMemberes = numberOfGroupMembers;

			var result = (groupTotalPayout / (TotalMemberes-1));
			return result;
		}
	}
}
