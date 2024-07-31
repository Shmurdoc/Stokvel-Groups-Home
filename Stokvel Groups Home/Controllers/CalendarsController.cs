using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stokvel_Groups_Home.Data;
using Stokvel_Groups_Home.Models;

namespace Stokvel_Groups_Home.Controllers
{
    public class CalendarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Calendars
        public async Task<IActionResult> Index(int groupId)
        {
            var memberList = await _context.Calendar.ToListAsync();
            var dataMemberList = memberList.Where(x => x.GroupId == groupId).ToList();

            return dataMemberList != null ? 
                          View(dataMemberList) :
                          Problem("Entity set 'ApplicationDbContext.Calendar'  is null.");
        }

        // GET: Calendars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calendar == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .FirstOrDefaultAsync(m => m.CalendarId == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // GET: Calendars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calendars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalendarId,GroupId,Title,Start,End,AllDay,ClassName,Private")] Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                calendar.GroupId = 1;
                _context.Add(calendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calendar);
        }

        // GET: Calendars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calendar == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return View(calendar);
        }

        // POST: Calendars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalendarId,Title,Start,End,AllDay,ClassName,GroupId")] Calendar calendar)
        {
            if (id != calendar.CalendarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarExists(calendar.CalendarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calendar);
        }

        // GET: Calendars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calendar == null)
            {
                return NotFound();
            }

            var calendar = await _context.Calendar
                .FirstOrDefaultAsync(m => m.CalendarId == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // POST: Calendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calendar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Calendar'  is null.");
            }
            var calendar = await _context.Calendar.FindAsync(id);
            if (calendar != null)
            {
                _context.Calendar.Remove(calendar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarExists(int id)
        {
          return (_context.Calendar?.Any(e => e.CalendarId == id)).GetValueOrDefault();
        }
    }
}
