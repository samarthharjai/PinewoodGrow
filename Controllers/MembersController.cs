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
    public class MembersController : Controller
    {
        private readonly GROWContext _context;

        public MembersController(GROWContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var gROWContext = _context.Members.Include(m => m.Address).Include(m => m.Gender).Include(m => m.Household);
            return View(await gROWContext.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City");
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name");
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "ID");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Age,Telephone,Email,FamilySize,Income,Notes,Consent,CompletedBy,CompletedOn,HouseholdID,GenderID,AddressID")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City", member.AddressID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", member.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "ID", member.HouseholdID);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City", member.AddressID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", member.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "ID", member.HouseholdID);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Age,Telephone,Email,FamilySize,Income,Notes,Consent,CompletedBy,CompletedOn,HouseholdID,GenderID,AddressID")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
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
            ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City", member.AddressID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", member.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "ID", member.HouseholdID);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Household)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }
    }
}
