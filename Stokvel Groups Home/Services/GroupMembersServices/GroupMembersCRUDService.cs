using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.IGroupMembersServices;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Services.GroupMembersServices
{
	public class GroupMembersCRUDService : IGroupMembersCRUDService
	{
		private readonly IGroupMembersRepository _groupMembersRepository;

		public GroupMembersCRUDService(IGroupMembersRepository groupMembersRepository)
		{
			_groupMembersRepository = groupMembersRepository;
		}

		public Task? Delete(int? id) => _groupMembersRepository.Delete(id);

		public Task? Edit(GroupMembers? groupMembers) => _groupMembersRepository.Edit(groupMembers);

		public Task<List<GroupMembers>>? GetAll() => _groupMembersRepository.GetAll();

		public Task<GroupMembers>? GetById(int? id) => _groupMembersRepository.GetById(id);

		public bool GroupMembersExists(int? id) => _groupMembersRepository.GroupMembersExists(id);


		public Task? Inset(GroupMembers? groupMembers) => _groupMembersRepository.Inset(groupMembers);

		public Task? SaveAsync() => _groupMembersRepository.SaveAsync();
	}
}
