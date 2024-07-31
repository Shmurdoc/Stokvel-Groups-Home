using Stokvel_Groups_Home.Models;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Interface.IServices.ICalendarServices;

namespace Stokvel_Groups_Home.Services.CalendarServices
{
	public class CalendarServices: ICalendarServices
	{
		private readonly ICalendarRepository _calendar;

        public CalendarServices(ICalendarRepository calendar)
        {
            _calendar = calendar;
		}

		public async Task<string> GetData()
		{
			IEnumerable<Calendar> events = await _calendar.GetAll();
			var eventData = events.Select(x => new Calendar { CalendarId = x.CalendarId, Title = x.Title, Start = x.Start, AllDay = x.AllDay, ClassName = x.ClassName, End = x.End, GroupId = x.GroupId, }).ToList();
			return Newtonsoft.Json.JsonConvert.SerializeObject(eventData);
		}

	}
}
