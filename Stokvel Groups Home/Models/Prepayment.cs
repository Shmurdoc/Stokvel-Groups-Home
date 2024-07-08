using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class Prepayment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PrepaymentId { get; set; }

		public string? PrepaymentType { get; set; }
		public string? PrepaymentDate { get; set; }
		public decimal Amount { get; set; }
		public bool? Status { get; set; }

		[ForeignKey("AccountId")]
		public int AccountId { get; set; }
		public Account? Account { get; set; }



	}
}
