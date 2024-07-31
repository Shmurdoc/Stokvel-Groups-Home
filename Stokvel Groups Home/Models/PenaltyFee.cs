using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class PenaltyFee
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PenaltyFeeId { get; set; }
		public DateTime PenaltyDate { get; set; }
		public decimal PenaltyAmount { get; set; }
		public string? PenaltyLevel { get; set; }




		public virtual ICollection<WithdrawDetails>? InvoiceDetails { get; set; }

	}
}
