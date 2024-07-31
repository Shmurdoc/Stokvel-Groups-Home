
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Interface.IRepo.UserArea;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Repositories
{
    public class CalendarRepository:ICalendarRepository
    {
        private readonly ApplicationDbContext _context;

        public CalendarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Calendar>>? GetAll()
        {
            return await _context.Calendar.ToListAsync();
		}

        public async Task<Calendar>? Details(int? id)
        {
            return await _context.Calendar.FirstOrDefaultAsync(m => m.CalendarId == id);
        }

        public async Task? Inset(Calendar? calendar)
        {
            await _context.Calendar.AddAsync(calendar);
        }

        public async Task? Edit(Calendar? calendar)
        {
            _context.Update(calendar);
        }
        public async Task? Delete(int? id)
        {
            var prepayment = await Details(id);
            _context.Calendar.Remove(prepayment);

        }

        public async Task? SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CalendarExists(int? id)
        {
            return (_context.Calendar?.Any(e => e.CalendarId == id)).GetValueOrDefault();
        }
    }
}
