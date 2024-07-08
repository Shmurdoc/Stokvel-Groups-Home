using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class MemberInvoice
	{

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AccountId { get; set; }
		public Account? Account { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InvoceId { get; set; }
		public Invoice? Invoice { get; set; }

	}
}
