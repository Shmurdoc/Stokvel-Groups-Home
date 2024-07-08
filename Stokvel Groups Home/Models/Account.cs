using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Account
{

	

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int AccountId { get; set; }

	public string? Id { get; set; }
	public AccountUser? AccountUser { get; set; }


	public string? GroupVerifyKey { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime AccountCreated { get; set; }

	public int AccountQueue { get; set; }

	public DateTime AccountQueueStart { get; set; }

	public DateTime AccontQueueEnd { get; set; }
	public bool Blocked { get; set; }
	public bool Accepted { get; set; }








	public virtual ICollection<MemberInvoice>? MemberInvoices { get; set; }
	public virtual ICollection<GroupMembers>? GroupMembers { get; set; }
	public virtual ICollection<Prepayment>? Prepayments { get; set; }
	public virtual ICollection<Wallet>? Wallets { get; set; }

}

