namespace Stokvel_Groups_Home.Models
{
	public class DisplayPaidMember
	{
		public AccountUser AccountUser { get; set; }
		public Account Account { get; set; }
		public GroupMembers GroupMembers { get; set; }
		public MemberInvoice MemberInvoice { get; set; }
		public Invoice Invoice { get; set; }
		public InvoiceDetails InvoiceDetails { get; set; }
		public Deposit Deposit { get; set; }

		public PenaltyFee PenaltyFee { get; set; }
	}
}
