using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.Finance;

public interface IMemberInvoiceRepository
{

	//GetAll && GetById
	public Task<List<MemberInvoice>>? GetAll();

	public Task<MemberInvoice>? GetById(int? id);

	//CRUD
	public Task Inset(MemberInvoice? memberInvoice);

	public Task Edit(MemberInvoice? memberInvoice);

	public Task Delete(int? id);

	public Task SaveAsync();

	public bool GroupMembersExists(int? id);
}
