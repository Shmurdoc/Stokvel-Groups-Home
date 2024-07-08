using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Stokvel_Groups_Home.Models
{
	public class ApplicationUser : IdentityUser
	{



		[Required]
		[Display(Name = "First Name")]
		public string? FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string? LastName { get; set; }



	}
}
