using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Interface.IRepo.UserArea
{
	public interface ICalendarRepository
	{
		Task<List<Calendar>>? GetAll();
		Task<Calendar>? Details(int? id);
		Task? Inset(Calendar? calendar);
		Task? Edit(Calendar? calendar);
		Task? Delete(int? id);
		Task? SaveAsync();
		bool CalendarExists(int? id);
	}
}
