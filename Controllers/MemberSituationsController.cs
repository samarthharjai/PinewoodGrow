using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinewoodGrow.Controllers
{
	public class MemberSituationsController : Controller
	{
        private readonly GROWContext _context;

        public MemberSituationsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Situations
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Members");
        }

        public PartialViewResult CreateMemberSituation(int? ID)
        {
            var unusedSituations = from si in _context.Situations
                                 where !(from p in _context.MemberSituations
                                         where p.MemberID == ID
                                         select p.SituationID).Contains(si.ID)
                                 select si;

            ViewData["SituationID"] = new
                SelectList(unusedSituations
                .OrderBy(a => a.Name), "ID", "Name");

            ViewData["MemberID"] = ID.GetValueOrDefault();

            return PartialView("_CreateMemberSituation");
        }

        public PartialViewResult EditMemberSituation(int ID)
        {
            //Get the MemberSituation to edit
            var memberSituation = _context.MemberSituations.Find(ID);

            var unusedSituations = from si in _context.Situations
                                 where !(from p in _context.MemberSituations
                                         where p.MemberID == memberSituation.MemberID
                                         select p.SituationID).Contains(si.ID)
                                    || si.ID == memberSituation.SituationID
                                 select si;

            ViewData["SituationID"] = new
                SelectList(unusedSituations
                .OrderBy(a => a.Name), "ID", "Name", memberSituation.SituationID);

            return PartialView("_EditMemberSituation", memberSituation);
        }

        public PartialViewResult DeleteMemberSituation(int Id)
        {
            //Get the one to delete
            MemberSituation memberSituation = _context.MemberSituations
                .Include(p => p.Situation)
                .Where(p => p.ID == Id)
                .FirstOrDefault();

            return PartialView("_DeleteMemberSituation", memberSituation);
        }


        // POST: MemberSituations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,SituationID,SituationIncome")] MemberSituation memberSituation)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(memberSituation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(memberSituation);
        }

        // POST: MemberSituations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID)
        {
            MemberSituation memberSituationToUpdate = await _context.MemberSituations.FindAsync(ID);
            if (await TryUpdateModelAsync<MemberSituation>(memberSituationToUpdate, "",
                p => p.SituationID, p => p.SituationIncome))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberSituationExists(memberSituationToUpdate.ID))
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
            return View(memberSituationToUpdate);
        }

        // POST: MemberSituations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ID)
        {
            var memberSituation = await _context.MemberSituations.FindAsync(ID);
            try
            {
                _context.MemberSituations.Remove(memberSituation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(memberSituation);
        }

        private bool MemberSituationExists(int id)
        {
            return _context.MemberSituations.Any(e => e.ID == id);
        }

    }
}

