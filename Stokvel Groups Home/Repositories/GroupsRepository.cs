using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;
using X.PagedList;

namespace Stokvel_Groups_Home.Repositories
{
	public class GroupsRepository : IGroupsRepository
	{
		private readonly ApplicationDbContext _context;
		public GroupsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task Delete(int? id)
		{
			var delGropInDb = await this.GetById(id);
			_context.Remove(delGropInDb);
		}

		public async Task Edit(Group? group)
		{
			_context.Update(group);
		}

		public async Task<List<Group>>? GetAll()
		{
			var getAll = await _context.Groups.ToListAsync();
			return getAll;
		}

		public async Task<int> GetGroupId(string? groupVerifyKey)
		{
			var groups = await _context.Groups.ToListAsync();

			var groupid = groups.Where(g => g.VerifyKey == groupVerifyKey).Select(x => x.GroupId).FirstOrDefault();
			return groupid;
		}

		public async Task<Group> Details(int? id)
		{
			var group = await _context.Groups
			   .FirstOrDefaultAsync(m => m.GroupId == id);

			return group;
		}

		//need to change this method name
		public async Task<bool> GroupIdExists(string verifyKey)
		{
			List<Group> groups = await _context.Groups.ToListAsync();
			var exists = groups.Any(g => g.VerifyKey == verifyKey);
			return exists;
		}

		public async Task Inset(Group? group)
		{
			await _context.AddAsync(group);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}



		public async Task<Group> GetById(int? id)
		{
			var groupsInDb = await _context.Groups.FirstOrDefaultAsync(x => x.GroupId == id);
			return groupsInDb;
		}




		public bool GroupExists(string? id)
		{
			return (_context.Groups?.Any(e => e.VerifyKey == id)).GetValueOrDefault();
		}


	}
}
