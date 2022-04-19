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
    public class DependantsController : Controller
    {
        private readonly GROWContext _context;

        public DependantsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Dependants
        public async Task<IActionResult> Index()
        {
            var gROWContext = _context.Dependents.Include(d => d.Household);
            return View(await gROWContext.ToListAsync());
        }

        // GET: Dependants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependant = await _context.Dependents
                .Include(d => d.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dependant == null)
            {
                return NotFound();
            }

            return View(dependant);
        }

        // GET: Dependants/Create
        public IActionResult Create(int HouseHoldID)
        {

            var tmp = new Dependant()
            {
                HouseholdID = HouseHoldID
            };

            ViewData["HouseholdID"] = HouseHoldID;
            return View(tmp);
        }

        // POST: Dependants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DOB")] Dependant dependant, int HouseholdID)
        {
            dependant.HouseholdID = HouseholdID;

            if (ModelState.IsValid)
            {
                _context.Add(dependant);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Households", new { id = HouseholdID });
            }
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", dependant.HouseholdID);
            return View(dependant);
        }

        // GET: Dependants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependant = await _context.Dependents.FindAsync(id);
            if (dependant == null)
            {
                return NotFound();
            }
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", dependant.HouseholdID);
            return View(dependant);
        }

        // POST: Dependants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DOB,HouseholdID")] Dependant dependant)
        {
            if (id != dependant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependantExists(dependant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Households", new { id = dependant.HouseholdID });
            }
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", dependant.HouseholdID);
            return View(dependant);
        }

        // GET: Dependants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependant = await _context.Dependents
                .Include(d => d.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dependant == null)
            {
                return NotFound();
            }

            return View(dependant);
        }

        // POST: Dependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dependant = await _context.Dependents.FindAsync(id);
            var HouseholdID = dependant.HouseholdID;
            _context.Dependents.Remove(dependant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Households", new { id = HouseholdID });
        }

        private bool DependantExists(int id)
        {
            return _context.Dependents.Any(e => e.ID == id);
        }
    }
}
