using System.ComponentModel.DataAnnotations;

namespace Stokvel_Groups_Home.Models
{
	public class Message
	{
		public int Id { get; set; }
		[Required]
		public string? UserName { get; set; }

		[Required]
		public string? Group {  get; set; }

		public string? MemberIdPath { get; set; }
		public string? MemberIdFileName { get; set; }

		[Required]
		public string? Text { get; set; }
		public DateTime When { get; set; }

		public string? UserID { get; set; }
		public virtual AppUser? Sender { get; set; }
		public Message()
		{
			When = DateTime.Now;
		}
	}
}
