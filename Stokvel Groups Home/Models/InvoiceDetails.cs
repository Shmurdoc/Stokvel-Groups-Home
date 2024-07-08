using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class InvoiceDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DetailedId { get; set; }
		public int InvoiceId { get; set; }
		public Invoice? Invoice { get; set; }
		public int PenaltyFeeId { get; set; }
		public PenaltyFee? PenaltyFee { get; set; }
		public decimal CreditAmount { get; set; } = default;
		public int TaxID { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime? CreditedDate { get; set; }
		public int PaymentId { get; set; }


	}
}
