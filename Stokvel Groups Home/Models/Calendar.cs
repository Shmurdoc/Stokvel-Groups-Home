using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stokvel_Groups_Home.Models;

public class Calendar
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int CalendarId { get; set; }

	[Required]
	[StringLength(30)]
	public string? Title { get; set; }
	public DateTime Start { get; set; }
	public DateTime End { get; set; }
	public Boolean AllDay { get; set; }
	[StringLength(30)]
	public string? ClassName { get; set; }
}
