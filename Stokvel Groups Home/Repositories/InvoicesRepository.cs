using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.Finance;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
    public class InvoicesRepository : IInvoicesRepository
	{
		private readonly ApplicationDbContext _context;
		public InvoicesRepository(ApplicationDbContext context)
		{

			_context = context;

		}

		public async Task? Delete(int? id)
		{
			var delInvoiceInDb = await this.Details(id);
			_context.Remove(delInvoiceInDb);
		}

		public async Task<Invoice>? Details(int? id)
		{
			return await _context.Invoices.FirstOrDefaultAsync(m => m.InvoiceId == id);
		}

		public async Task Edit(Invoice invoice)
		{
			_context.Update(invoice);
		}

		public async Task<List<Invoice>>? GetAll()
		{
			return await _context.Invoices.ToListAsync();
		}

		public async Task? Inset(Invoice? invoice)
		{

			await _context.AddAsync(invoice);

		}
		public async Task? SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task InsetDeposit(int AccontId, int InvoceId)
		{


			MemberInvoice memberInvoice = new();

			// required Lists to add 

			memberInvoice.InvoceId = InvoceId;
			memberInvoice.AccountId = AccontId;



			await _context.AddAsync(memberInvoice);
			await _context.SaveChangesAsync();


		}

		public bool InvoiceExists(int? id)
		{
			return (_context.Invoices?.Any(e => e.InvoiceId == id)).GetValueOrDefault();
		}



		public async Task<List<InvoiceGroupDetails>> Profile()
		{
			List<AccountUser>? accountUsers = await _context.AccountUsers.ToListAsync();
			List<Account>? accounts = await _context.Accounts.ToListAsync();
			List<GroupMembers>? groupMembers = await _context.GroupMembers.ToListAsync();
			List<MemberInvoice>? memberInvoices = await _context.MemberInvoices.ToListAsync();
			List<Invoice>? invoices = await _context.Invoices.ToListAsync();


			var profile = (from au in accountUsers
						   join a in accounts on au.Id equals a.Id into table1
						   from a in table1.ToList()
						   join gm in groupMembers on a.AccountId equals gm.AccountId into table2
						   from gm in table2.ToList()
						   join mi in memberInvoices on gm.AccountId equals mi.AccountId into table3
						   from mi in table3.ToList()
						   join i in invoices on mi.InvoceId equals i.InvoiceId into table4
						   from i in table4.ToList()

						   select new InvoiceGroupDetails
						   {
							   AccountUser = au,
							   Account = a,
							   GroupMembers = gm,
							   MemberInvoice = mi,
							   Invoice = i


						   }).ToList();
			return profile;



		}


	}
}
