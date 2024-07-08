using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories;

public class MemberInvoiceRepository : IMemberInvoiceRepository
{

	private readonly ApplicationDbContext _context;
	public MemberInvoiceRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task? Delete(int? id)
	{
		var delGroupInDb = await this.GetById(id);
		_context.Remove(delGroupInDb);
	}

	public async Task<MemberInvoice>? GetById(int? id)
	{
		var groupMemberInDb = await _context.MemberInvoices.FirstOrDefaultAsync(x => x.InvoceId == id);
		return groupMemberInDb;
	}

	public async Task? Edit(MemberInvoice? memberInvoice)
	{
		_context.Update(memberInvoice);
	}

	public async Task<List<MemberInvoice>>? GetAll()
	{
		var allGroupMembersInDb = await _context.MemberInvoices.ToListAsync();
		return allGroupMembersInDb;
	}


	public bool MemberInvoiceExists(int? id)
	{
		throw new NotImplementedException();
	}



	public async Task? Inset(MemberInvoice? memberInvoice)
	{
		await _context.AddAsync(memberInvoice);
	}

	public async Task? SaveAsync()
	{
		await _context.SaveChangesAsync();
	}



	public bool GroupMembersExists(int? id)
	{
		throw new NotImplementedException();
	}
}
