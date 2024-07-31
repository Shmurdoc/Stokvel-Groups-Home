using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class DepositStatus
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PaymentStatusId { get; set; }
		public string? PaymentStatusName { get; set; }
		public string? PaymentStatesDescription { get; set; }

		public virtual ICollection<Deposit>? Deposits { get; set; }
	}
}
