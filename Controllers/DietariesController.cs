using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models;

namespace PinewoodGrow.Controllers
{
    public class DietariesController : Controller
    {
        private readonly GROWContext _context;

        public DietariesController(GROWContext context)
        {
            _context = context;
        }

        // GET: Dietaries
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dietaries.ToListAsync());
        }

        // GET: Dietaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietary = await _context.Dietaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dietary == null)
            {
                return NotFound();
            }

            return View(dietary);
        }

        // GET: Dietaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dietaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Dietary dietary)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(dietary);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            
            return View(dietary);
        }

        // GET: Dietaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietary = await _context.Dietaries.FindAsync(id);
            if (dietary == null)
            {
                return NotFound();
            }
            return View(dietary);
        }

        // POST: Dietaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var dietaryToUpdate = await _context.Dietaries.FirstOrDefaultAsync(d => d.ID == id);

            if (dietaryToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Dietary>(dietaryToUpdate, "", d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietaryExists(dietaryToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(dietaryToUpdate);
        }

        // GET: Dietaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietary = await _context.Dietaries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dietary == null)
            {
                return NotFound();
            }

            return View(dietary);
        }

        // POST: Dietaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dietary = await _context.Dietaries.FindAsync(id);
            try
            {
                _context.Dietaries.Remove(dietary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Dietary Restriction. You cannot delete a Dietary Restriction that Members have.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(dietary);
        }

        private bool DietaryExists(int id)
        {
            return _context.Dietaries.Any(e => e.ID == id);
        }
    }
}
