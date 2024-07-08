using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.AccountProfileServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.AccountProfileServices
{
	public class AccountProfileServices : IAccountProfileServices
	{
		private readonly IAccountProfileRepository _accountProfileRepository;
		private readonly IAccountsRepository _accountsRepository;
		private readonly IGroupMembersRepository _groupMembersRepository;
		public AccountProfileServices(IAccountProfileRepository accountProfileRepository, IAccountsRepository accountsRepository, IGroupMembersRepository groupMembersRepository)
		{
			_accountProfileRepository = accountProfileRepository;
			_accountsRepository = accountsRepository;
			_groupMembersRepository = groupMembersRepository;
		}

		// Paid Deposit payment Bool, checks to see if anyone in group Paid the Deposit
		public async Task<bool> MemberStatusPaidDepo(int accontId)
		{
			List<Account> accounts = await _accountsRepository.GetAll();
			List<AccountProfile> accountProfiles = await _accountProfileRepository.GetAll();
			bool result = false;
			var id = accounts.Where(x => x.AccountId == accontId).Select(x => x.Id).FirstOrDefault();
			result = accountProfiles.Any(x => x.Id == id && x.StatusRank == MemberStatuses.Bronz || x.StatusRank == MemberStatuses.Silver || x.StatusRank == MemberStatuses.Gold);

			return result;
		}

		// Pending Deposit payment Bool, checks to see if anyone in group uses method Type
		public async Task<bool> MemberStatusPending(int accontId)
		{
			List<Account> accounts = await _accountsRepository.GetAll();
			List<AccountProfile> accountProfiles = await _accountProfileRepository.GetAll();
			bool result = false;

			var id = accounts.Where(x => x.AccountId == accontId).Select(x => x.Id).FirstOrDefault();
			result = accountProfiles.Any(x => x.Id == id && x.StatusRank == MemberStatuses.PendingPayment);

			return result;
		}

		// Platnum Deposit payment Bool, checks to see if anyone is a trusted user
		public async Task<bool> MemberStatusPlat(int accontId)
		{
			List<Account> accounts = await _accountsRepository.GetAll();
			List<AccountProfile> accountProfiles = await _accountProfileRepository.GetAll();
			bool result = false;

			var id = accounts.Where(x => x.AccountId == accontId).Select(x => x.Id).FirstOrDefault();
			result = accountProfiles.Any(x => x.Id == id && x.StatusRank == MemberStatuses.Platnum);

			return result;
		}
		public async Task<List<string>> ListIdInOrder(List<int> comboGroups)
		{
			List<string> idOrder = new();
			List<string> memberId = new();
			List<string> memberAddedId = new();
			List<Account> accounts = await _accountsRepository.GetAll();
			List<AccountProfile> statusValue = await _accountProfileRepository.GetAll();

			foreach (var accountId in comboGroups)
			{
				var Id = accounts.Where(x => x.AccountId == accountId).Select(x => x.Id).FirstOrDefault();
				memberId.Add(Id);
				/*if (memberId.Count == comboGroups.Count)
                    foreach (var memeber in statusValue.OrderByDescending(x => x.MembershipRank).Select(x => x.Id).ToList())
                    {
                        if (memeber == null) continue;
                        if (memeber.Any() == memberId.Any())
                        {
                            memberAddedId.Add(memeber);
                        }
                    }*/
			}
			return memberId;
		}
		public async Task<List<int>> ByGroupFilter(List<string> memberList, List<int> accountStatus, int groupId)
		{
			List<GroupMembers> members = await _groupMembersRepository.GetAll();

			List<int> paidMember = new();

			foreach (var items in memberList)
			{

				var accountIdList = members.Where(x => x.Account.Id == items && x.GroupId == groupId).Select(x => x.AccountId).ToList();
				foreach (var item in accountIdList)
				{
					if (accountStatus.Any(x => x.Equals(item)))
					{
						paidMember.Add(item);
					}
				}
			}
			return paidMember;
		}
	}
}
