using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Invoice
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int InvoiceId { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime? InvoiceDate { get; set; }

	public string? Discription { get; set; }

	public decimal TotalAmount { get; set; }



	public virtual ICollection<MemberInvoice>? MemberInvoices { get; set; }

	public virtual ICollection<Deposit>? Deposits { get; set; }

	public virtual ICollection<InvoiceDetails>? InvoiceDetails { get; set; }
}
