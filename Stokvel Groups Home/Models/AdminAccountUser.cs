using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class AdminAccountUser
{

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string? Id { get; set; }


	[Required(ErrorMessage = "Please Fill In Your Name")]
	[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
	[Column("FirstName")]
	[Display(Name = "First Name")]
	public string? FirstName { get; set; }



	[Required(ErrorMessage = "Please Fill In Your Lastname")]
	[Display(Name = "Last Name")]
	[StringLength(50)]
	public string? LastName { get; set; }

	[NotMapped]
	[DisplayName("Upload File")]
	[DataType(DataType.Upload)]
	[FileExtensions(Extensions = "jpg,png,gif,jpeg,bmp,svg")]
	public IFormFile? ProfilePicture { get; set; }
	public string? MemberPhotoPath { get; set; }
	public string? MemberFileName { get; set; }


	[Required(ErrorMessage = "Please Fill In Your address")]
	public string? Address { get; set; }
	[Required(ErrorMessage = "Please Fill In Your City")]
	public string? City { get; set; }


	[Required(ErrorMessage = "Please Fill In Your Province")]
	public string? Province { get; set; }
	[Required(ErrorMessage = "Please Fill In Your Zip")]
	public int Zip { get; set; }


	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime Date { get; set; }



	public virtual IEnumerable<AccountProfile>? AccountProfiles { get; set; }


}
