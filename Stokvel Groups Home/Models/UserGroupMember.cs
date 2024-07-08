namespace Stokvel_Groups_Home.Models;

public class UserGroupMember
{


	public AccountUser? AccountUser { get; set; }
	public Account? Account { get; set; }
	public GroupMembers? GroupMembers { get; set; }
	public Group? Group { get; set; }
}

