using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models.Temp;

namespace PinewoodGrow.Controllers
{
    public class TempDependantsController : Controller
    {
        private readonly GROWContext _context;
        public PartialViewResult CreateTempDependant(int? ID)
        {

            var tempDependant = new TempDependant()
            {
                HouseholdID = ID.GetValueOrDefault(),
            
            };
            

            return PartialView("_CreateDependant", tempDependant);
        }

        public PartialViewResult EditTempDependant(int? ID)
        {

            var tmp = _context.TempDependents.FirstOrDefault(a => a.ID == ID);


            return PartialView("_EditDependant", tmp);
        }
        public PartialViewResult DeleteTempDependant(int? ID)
        {

            var tmp = _context.TempDependents.FirstOrDefault(a => a.ID == ID);


            return PartialView("_DeleteDependant", tmp);
        }
        [HttpPost]
        public PartialViewResult EditTempDependant(int ID, DateTime DOB)
        {

            var tmp = _context.TempDependents.FirstOrDefault(a => a.ID == ID);

            tmp.DOB = DOB;

            _context.Update(tmp);
            _context.SaveChanges();

            return PartialView("_EditDependant", tmp);
        }
        public TempDependantsController(GROWContext context)
        {
            _context = context;
        }


        #region Basic Stuff

        

   
        // GET: TempDependants
        public async Task<IActionResult> Index()
        {
            var gROWContext = _context.TempDependents.Include(t => t.Household);
            return View(await gROWContext.ToListAsync());
        }

        // GET: TempDependants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempDependant = await _context.TempDependents
                .Include(t => t.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tempDependant == null)
            {
                return NotFound();
            }

            return View(tempDependant);
        }

        // GET: TempDependants/Create
        public IActionResult Create()
        {
            ViewData["HouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "ID");
            return View();
        }

        // POST: TempDependants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DOB,HouseholdID")] TempDependant tempDependant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tempDependant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "ID", tempDependant.HouseholdID);
            return View(tempDependant);
        }

        // GET: TempDependants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempDependant = await _context.TempDependents.FindAsync(id);
            if (tempDependant == null)
            {
                return NotFound();
            }
            ViewData["HouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "ID", tempDependant.HouseholdID);
            return View(tempDependant);
        }

        // POST: TempDependants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DOB,HouseholdID")] TempDependant tempDependant)
        {
            if (id != tempDependant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tempDependant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempDependantExists(tempDependant.ID))
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
            ViewData["HouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "ID", tempDependant.HouseholdID);
            return View(tempDependant);
        }

        // GET: TempDependants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempDependant = await _context.TempDependents
                .Include(t => t.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tempDependant == null)
            {
                return NotFound();
            }

            return View(tempDependant);
        }

        // POST: TempDependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tempDependant = await _context.TempDependents.FindAsync(id);
            _context.TempDependents.Remove(tempDependant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TempDependantExists(int id)
        {
            return _context.TempDependents.Any(e => e.ID == id);
        }
    }
    #endregion
}
