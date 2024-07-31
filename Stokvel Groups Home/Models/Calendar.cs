using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Calendar
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int CalendarId { get; set; }

	[Required]
	[StringLength(50)]
	public string? Title { get; set; }
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public Boolean AllDay { get; set; }
	[StringLength(50)]
	public string? ClassName { get; set; }
	public Boolean Private { get; set; }

	public int GroupId { get; set; }
	public Group? Group { get; set; }
}
