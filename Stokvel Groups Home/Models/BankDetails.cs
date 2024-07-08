using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class BankDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BankDetailId { get; set; }
		public string? BankName { get; set; }
		public int AccountNumber { get; set; }

		public int InvestmentId { get; set; }
		public Deposit? Deposit { get; set; }
		public DateTime ExpiryDate { get; set; }


	}
}
