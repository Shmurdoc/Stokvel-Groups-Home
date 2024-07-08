namespace Stokvel_Groups_Home.Models.Tables
{
	public class DepositSystem
	{
		public Invoice? Invoice { get; set; }
		public Deposit? Deposit { get; set; }
		public PaymentStatus? PaymentStatus { get; set; }
		public PaymentMethod? PaymentMethod { get; set; }
		public Prepayment? Prepayment { get; set; }

	}
}
