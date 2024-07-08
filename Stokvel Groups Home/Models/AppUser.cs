using Microsoft.AspNetCore.Identity;

namespace Stokvel_Groups_Home.Models
{
	public class AppUser : IdentityUser
	{
		public AppUser()
		{
			Messages = new HashSet<Message>();
		}
		// 1 - * AppUSer || Message
		public virtual ICollection<Message>? Messages { get; set; }
	}
}