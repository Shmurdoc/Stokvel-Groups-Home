using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea;

public interface IAccountProfileRepository
{
	//GetAll && GetById
	Task<List<AccountProfile>>? GetAll();

	//CRUD
	Task<AccountProfile>? Details(int? id);
	Task Inset(AccountProfile? paymentMethod);

	Task Edit(AccountProfile? paymentMethod);

	Task Delete(int? id);

	Task SaveAsync();

	//Other

	bool PaymentMethodExists(int? id);


}
