using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.Infrastructure
{
	public interface IInputFoldersRepository
	{
		Task UploadImage(AccountUser accountUser, IFormFile uploadedImage);
		
	}
}
