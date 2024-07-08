using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
	public class GroupMembersRepository : IGroupMembersRepository
	{
		private readonly ApplicationDbContext _context;
		public GroupMembersRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task? Delete(int? id)
		{
			var delGroupInDb = await this.GetById(id);
			_context.Remove(delGroupInDb);
		}

		public async Task<GroupMembers>? GetById(int? id)
		{
			var groupMemberInDb = await _context.GroupMembers.FirstOrDefaultAsync(x => x.GroupId == id);
			return groupMemberInDb;
		}

		public async Task? Edit(GroupMembers? groupMembers)
		{
			_context.Update(groupMembers);
		}

		public async Task<List<GroupMembers>>? GetAll()
		{
			var allGroupMembersInDb = await _context.GroupMembers.ToListAsync();
			return allGroupMembersInDb;
		}

		public bool GroupMembersExists(int? id)
		{
			throw new NotImplementedException();
		}



		public async Task? Inset(GroupMembers? groupMembers)
		{
			await _context.AddAsync(groupMembers);
		}

		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<List<int>> GroupIdList(string id)
		{
			var accountIdList = await _context.Accounts.Where(x => x.Id == id).Select(x => x.AccountId).ToListAsync();
			List<int>? groupid = new();

			foreach (var accountid in accountIdList)
			{
				var groupList = await _context.GroupMembers.ToListAsync();
				var groupIdList = groupList.Where(x => x.AccountId == accountid).Select(x => x.GroupId).FirstOrDefault();
				groupid.Add(groupIdList);
			}
			return groupid;
		}
	}
}
