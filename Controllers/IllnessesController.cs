using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models;
using Microsoft.AspNetCore.Authorization;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class IllnessesController : Controller
    {
        private readonly GROWContext _context;

        public IllnessesController(GROWContext context)
        {
            _context = context;
        }

        // GET: Illnesses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Illnesses.ToListAsync());
        }

        // GET: Illnesses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illness = await _context.Illnesses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (illness == null)
            {
                return NotFound();
            }

            return View(illness);
        }

        // GET: Illnesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Illnesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Illness illness)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(illness);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(illness);
        }

        // GET: Illnesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illness = await _context.Illnesses.FindAsync(id);
            if (illness == null)
            {
                return NotFound();
            }
            return View(illness);
        }

        // POST: Illnesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var illnessToUpdate = await _context.Illnesses.FirstOrDefaultAsync(i => i.ID == id);

            if (illnessToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Illness>(illnessToUpdate, "", i => i.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IllnessExists(illnessToUpdate.ID))
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
            return View(illnessToUpdate);
        }

        // GET: Illnesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var illness = await _context.Illnesses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (illness == null)
            {
                return NotFound();
            }

            return View(illness);
        }

        // POST: Illnesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var illness = await _context.Illnesses.FindAsync(id);
            try
            {
                _context.Illnesses.Remove(illness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Illness Restriction. You cannot delete a Illness Restriction that Members have.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(illness);
        }

        private bool IllnessExists(int id)
        {
            return _context.Illnesses.Any(e => e.ID == id);
        }
    }
}
