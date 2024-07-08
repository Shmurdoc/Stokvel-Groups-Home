using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Group
{


	[Key]
	[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
	public int GroupId { get; set; }

	public string? ManagerId { get; set; }


	public string? GroupName { get; set; }


	[MaxLength(50)]
	public string? VerifyKey { get; set; }

	public AccountType TypeAccount { get; set; }


	[Range(1, 12)]
	[Required(ErrorMessage = "Please Fill in The Number of Members You Wish to Have")]
	public int TotalGroupMembers { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime GroupDate { get; set; }

	[Column(TypeName = "decimal(18,2)")]
	public decimal AccountTarget { get; set; }

	public bool GroupStatus { get; set; }

	public bool GroupBlocked { get; set; }

	public bool CycleStart { get; set; }
	public bool Active { get; set; }
	public bool Private { get; set; }




	public virtual ICollection<GroupMembers>? GroupMembers { get; set; }


}
