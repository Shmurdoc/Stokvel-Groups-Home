using Stokvel_Groups_Home.Data;

namespace Stokvel_Groups_Home.Repositories;

public class MemberDepositAccountRepository
{
	private readonly ApplicationDbContext _context;
	public MemberDepositAccountRepository(ApplicationDbContext context)
	{
		_context = context;
	}


	/*public async Task<List<MemberDepositAccount>> GetMemberDepositAccountList()
    {
        List<GroupMembers> groupMembers = await _context.GroupMembers.ToListAsync();
        List<MemberInvoice> memberInvoices = await _context.MemberInvoices.ToListAsync();
        List<DepositSystem> depositSystems = new();


        var profile = ( from gm in  groupMembers
                       join ai in memberInvoices on gm.AccountId equals ai.AccountId into table1
                       from mi in table1.ToList()
                       join ds in depositSystems on mi.InvoceId equals ds.Invoice.InvoiceId into table2
                       from ds in table2.ToList()
                       select new MemberDepositAccount
                       {
                           MemberInvoice = mi,
                           GroupMembers =gm,
                           DepositSystem=ds,

                       }).ToList();
        return profile;

    }*/

}
