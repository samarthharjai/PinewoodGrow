﻿using System;
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
    public class SituationsController : Controller
    {
        private readonly GROWContext _context;

        public SituationsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Situations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Situations.ToListAsync());
        }

        // GET: Situations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situation = await _context.Situations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (situation == null)
            {
                return NotFound();
            }

            return View(situation);
        }

        // GET: Situations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Situations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Situation situation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(situation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(situation);
        }

        // GET: Situations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situation = await _context.Situations.FindAsync(id);
            if (situation == null)
            {
                return NotFound();
            }
            return View(situation);
        }

        // POST: Situations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Situation situation)
        {
            if (id != situation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(situation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SituationExists(situation.ID))
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
            return View(situation);
        }

        // GET: Situations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var situation = await _context.Situations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (situation == null)
            {
                return NotFound();
            }

            return View(situation);
        }

        // POST: Situations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var situation = await _context.Situations.FindAsync(id);
            try
            {
                _context.Situations.Remove(situation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to Delete Living Situation. You cannot delete a Living Situation that Members have.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(situation);
        }

        private bool SituationExists(int id)
        {
            return _context.Situations.Any(e => e.ID == id);
        }
    }
}
