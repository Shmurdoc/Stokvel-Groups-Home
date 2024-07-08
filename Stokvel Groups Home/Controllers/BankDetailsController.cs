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
    public class BankDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BankDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BankDetails
        public async Task<IActionResult> Index()
        {
              return _context.BankDetails != null ? 
                          View(await _context.BankDetails.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.BankDetails'  is null.");
        }

        // GET: BankDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BankDetails == null)
            {
                return NotFound();
            }

            var bankDetails = await _context.BankDetails
                .FirstOrDefaultAsync(m => m.BankDetailId == id);
            if (bankDetails == null)
            {
                return NotFound();
            }

            return View(bankDetails);
        }

        // GET: BankDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BankDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BankDetailId,BankName,AccountNumber,InvestmentId,ExpiryDate")] BankDetails bankDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankDetails);
        }

        // GET: BankDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BankDetails == null)
            {
                return NotFound();
            }

            var bankDetails = await _context.BankDetails.FindAsync(id);
            if (bankDetails == null)
            {
                return NotFound();
            }
            return View(bankDetails);
        }

        // POST: BankDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BankDetailId,BankName,AccountNumber,InvestmentId,ExpiryDate")] BankDetails bankDetails)
        {
            if (id != bankDetails.BankDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankDetailsExists(bankDetails.BankDetailId))
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
            return View(bankDetails);
        }

        // GET: BankDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BankDetails == null)
            {
                return NotFound();
            }

            var bankDetails = await _context.BankDetails
                .FirstOrDefaultAsync(m => m.BankDetailId == id);
            if (bankDetails == null)
            {
                return NotFound();
            }

            return View(bankDetails);
        }

        // POST: BankDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BankDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BankDetails'  is null.");
            }
            var bankDetails = await _context.BankDetails.FindAsync(id);
            if (bankDetails != null)
            {
                _context.BankDetails.Remove(bankDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankDetailsExists(int id)
        {
          return (_context.BankDetails?.Any(e => e.BankDetailId == id)).GetValueOrDefault();
        }
    }
}
