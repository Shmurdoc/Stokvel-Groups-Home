using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.Infrastructure;
using Stokvel_Groups_Home.Models;


namespace Stokvel_Groups_Home.Services.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{

		private readonly ApplicationDbContext _context;
		private IWebHostEnvironment _webHostEnvironment;

		public UnitOfWork(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}





		public async Task UploadImage(AccountUser? accountUser, IFormFile? uploadedImage)
		{


			if (uploadedImage != null)
			{
				string? uniqueFileName = null;

				string? ImageUploadedFoleder = Path.Combine
					(_webHostEnvironment.WebRootPath, "images/MemberProfile");

				uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedImage.FileName;


				string? filepath = Path.Combine(ImageUploadedFoleder, uniqueFileName);

				using (var fileStream = new FileStream(filepath, FileMode.Create))
				{
					uploadedImage.CopyTo(fileStream);
				}

				accountUser.MemberPhotoPath = "~/wwwroot/images/MemberProfile";
				accountUser.MemberFileName = uniqueFileName;

				await _context.AccountUsers.AddAsync(accountUser);
				await _context.SaveChangesAsync();
			}


		}
		public async Task UploadImageAdmin(AdminAccountUser adminAccountUser, IFormFile? uploadedImage)
		{
			if (uploadedImage != null)
			{
				string? uniqueFileName = null;

				string? ImageUploadedFoleder = Path.Combine
					(_webHostEnvironment.WebRootPath, "images/MemberProfile");

				uniqueFileName = Guid.NewGuid().ToString() + "_" + uploadedImage.FileName;


				string? filepath = Path.Combine(ImageUploadedFoleder, uniqueFileName);

				using (var fileStream = new FileStream(filepath, FileMode.Create))
				{
					uploadedImage.CopyTo(fileStream);
				}

				adminAccountUser.MemberPhotoPath = "~/wwwroot/images/MemberProfile";
				adminAccountUser.MemberFileName = uniqueFileName;

				await _context.AdminAccountUsers.AddAsync(adminAccountUser);
				await _context.SaveChangesAsync();
			}
		}


	}

}
