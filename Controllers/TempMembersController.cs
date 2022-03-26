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
    public class TempMembersController : Controller
    {
        private readonly GROWContext _context;

        public TempMembersController(GROWContext context)
        {
            _context = context;
        }

        // GET: TempMembers
        public async Task<IActionResult> Index()
        {
            var gROWContext = _context.TempMembers.Include(t => t.Gender).Include(t => t.Household).Include(t => t.TempHousehold).Include(t => t.Volunteer);
            return View(await gROWContext.ToListAsync());
        }

        // GET: TempMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempMember = await _context.TempMembers
                .Include(t => t.Gender)
                .Include(t => t.Household)
                .Include(t => t.TempHousehold)
                .Include(t => t.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tempMember == null)
            {
                return NotFound();
            }

            return View(tempMember);
        }

        // GET: TempMembers/Create
        public IActionResult Create()
        {
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name");
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName");
            ViewData["TempHouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "FamilyName");
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name");
            return View();
        }

        // POST: TempMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,DOB,Telephone,Email,Income,Notes,Consent,VolunteerID,CompletedOn,TempHouseholdID,HouseholdID,GenderID")] TempMember tempMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tempMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", tempMember.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", tempMember.HouseholdID);
            ViewData["TempHouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "FamilyName", tempMember.TempHouseholdID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name", tempMember.VolunteerID);
            return View(tempMember);
        }

        // GET: TempMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempMember = await _context.TempMembers.FindAsync(id);
            if (tempMember == null)
            {
                return NotFound();
            }
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", tempMember.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", tempMember.HouseholdID);
            ViewData["TempHouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "FamilyName", tempMember.TempHouseholdID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name", tempMember.VolunteerID);
            return View(tempMember);
        }

        // POST: TempMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,DOB,Telephone,Email,Income,Notes,Consent,VolunteerID,CompletedOn,TempHouseholdID,HouseholdID,GenderID")] TempMember tempMember)
        {
            if (id != tempMember.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tempMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempMemberExists(tempMember.ID))
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
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", tempMember.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName", tempMember.HouseholdID);
            ViewData["TempHouseholdID"] = new SelectList(_context.TempHouseholds, "ID", "FamilyName", tempMember.TempHouseholdID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name", tempMember.VolunteerID);
            return View(tempMember);
        }

        // GET: TempMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempMember = await _context.TempMembers
                .Include(t => t.Gender)
                .Include(t => t.Household)
                .Include(t => t.TempHousehold)
                .Include(t => t.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tempMember == null)
            {
                return NotFound();
            }

            return View(tempMember);
        }

        // POST: TempMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tempMember = await _context.TempMembers.FindAsync(id);
            _context.TempMembers.Remove(tempMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public PartialViewResult MemberSituationList(int id)
        {
            ViewBag.TempMemberSituations = _context.TempMemberSituations
                .Include(s => s.Situation)
                .Where(s => s.MemberID == id)
                .OrderBy(s => s.Situation.Name)
                .ToList();
            return PartialView("_MemberSituationList");
        }
        private bool TempMemberExists(int id)
        {
            return _context.TempMembers.Any(e => e.ID == id);
        }
    }
}
