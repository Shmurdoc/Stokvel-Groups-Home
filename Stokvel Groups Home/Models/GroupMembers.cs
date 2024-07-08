using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class GroupMembers
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int GroupId { get; set; }
	public Group? Group { get; set; }

	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int AccountId { get; set; }
	public Account? Account { get; set; }




}
