using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Wallet
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int WalletId { get; set; }
	public decimal Amount { get; set; }

	[ForeignKey("AccountId")]
	public int AccountId { get; set; }
	public Account? Account { get; set; }


}
