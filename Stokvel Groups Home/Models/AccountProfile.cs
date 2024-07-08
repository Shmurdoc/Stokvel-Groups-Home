
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class AccountProfile
{
	[Key]
	public int AccountProfileId { get; set; }

	[ForeignKey("AccountUser")]
	public string? Id { get; set; }
	public AccountUser? AccountUser { get; set; }
	public int GroupsJoined { get; set; }
	public int GroupsLeft { get; set; }
	public int EmergencyCancel { get; set; }
	public MemberStatuses StatusRank { get; set; }
	public int MembershipRank { get; set; }

	public decimal TotalAmoutDeposited { get; set; }

	public decimal TotalPenaltyFee { get; set; }

	public int GroupWarnings { get; set; }
}
