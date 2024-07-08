using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.Infrastructure
{
	public interface IUnitOfWork
	{
		Task UploadImage(AccountUser accountUser, IFormFile uploadedImage);
		Task UploadImageAdmin(AdminAccountUser? AdminAccountUser, IFormFile? uploadedImage);
	}
}
