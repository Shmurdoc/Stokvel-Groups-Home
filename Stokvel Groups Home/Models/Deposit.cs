using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class Deposit
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DepositId { get; set; }



		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }
		public Invoice? Invoice { get; set; }



		public decimal DepositAmount { get; set; }

		public DateTime DepositDate { get; set; }




		[ForeignKey("PaymentMethod")]
		public int MethodId { get; set; }
		public DepositMethod? PaymentMethod { get; set; }




		public int PaymentStatusId { get; set; }
		public DepositStatus? PaymentStatus { get; set; }


		[StringLength(50)]
		public string? DepositReference { get; set; }
		[StringLength(50)]


		public virtual ICollection<DepositLog>? PaymentLogs { get; set; }

		public virtual ICollection<BankDetails>? BankDetails { get; set; }


	}
}
