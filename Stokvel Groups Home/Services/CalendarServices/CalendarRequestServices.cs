using Stokvel_Groups_Home.Interface.IServices.ICalendarServices;

namespace Stokvel_Groups_Home.Services.CalendarServices
{

	
	public class CalendarRequestServices: ICalendarRequestServices
	{
		private readonly ICalendarServices _calendarServices;

        public CalendarRequestServices(ICalendarServices calendarServices)
        {
            _calendarServices = calendarServices;
        }

        public async Task<string> GetData() => await _calendarServices.GetData();
    }
}
