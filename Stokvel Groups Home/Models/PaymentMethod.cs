using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class PaymentMethod
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MethodId { get; set; }
		public string? MethodName { get; set; }
		public string? Description { get; set; }
		public virtual ICollection<Deposit>? Deposits { get; set; }
	}
}
