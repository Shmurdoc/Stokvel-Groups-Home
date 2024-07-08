using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models
{
	public class PaymentLog
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LogId { get; set; }
		public Deposit? Deposit { get; set; }
		public DateTime TimeStamp { get; set; }
		public string? LogMessage { get; set; }


		public int DepositId { get; set; }





	}
}
