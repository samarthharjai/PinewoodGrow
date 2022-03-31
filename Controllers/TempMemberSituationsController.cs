using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models.Temp;
using Microsoft.AspNetCore.Authorization;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class TempMemberSituationsController : Controller
    {
        private readonly GROWContext _context;

        public TempMemberSituationsController(GROWContext context)
        {
            _context = context;
        }

        // GET: TempMemberSituations
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Members");
        }
        public PartialViewResult CreateMemberSituation(int? ID)
        {
            var unusedSituations = from si in _context.Situations
                where !(from p in _context.TempMemberSituations
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
            //Get the TempMemberSituation to edit
            var TempMemberSituation = _context.TempMemberSituations.Find(ID);

            var unusedSituations = from si in _context.Situations
                where !(from p in _context.TempMemberSituations
                        where p.MemberID == TempMemberSituation.MemberID
                          select p.SituationID).Contains(si.ID)
                      || si.ID == TempMemberSituation.SituationID
                select si;

            ViewData["SituationID"] = new
                SelectList(unusedSituations
                    .OrderBy(a => a.Name), "ID", "Name", TempMemberSituation.SituationID);

            return PartialView("_EditMemberSituation", TempMemberSituation);
        }

        public PartialViewResult EditTempMemberSituation(int ID, int Income)
        {
            //Get the TempMemberSituation to edit
            var TempMemberSituation = _context.MemberSituations.Find(ID);

            var unusedSituations = from si in _context.Situations
                where !(from p in _context.TempMemberSituations
                        where p.MemberID == TempMemberSituation.MemberID
                          select p.SituationID).Contains(si.ID)
                      || si.ID == TempMemberSituation.SituationID
                select si;

            ViewData["SituationID"] = new
                SelectList(unusedSituations
                    .OrderBy(a => a.Name), "ID", "Name", TempMemberSituation.SituationID);

            return PartialView("_EditMemberSituation", TempMemberSituation);
        }

        public PartialViewResult DeleteMemberSituation(int Id)
        {
            //Get the one to delete
            var TempMemberSituation = _context.TempMemberSituations
                .Include(p => p.Situation)
                .FirstOrDefault(p => p.ID == Id);

            return PartialView("_DeleteMemberSituation", TempMemberSituation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberID,SituationID,SituationIncome")] TempMemberSituation TempMemberSituation)
        {



            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(TempMemberSituation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(TempMemberSituation);
        }

        // POST: MemberSituations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID)
        {
            var memberSituationToUpdate = await _context.TempMemberSituations.FindAsync(ID);
            if (await TryUpdateModelAsync<TempMemberSituation>(memberSituationToUpdate, "",
                p => p.SituationID, p => p.SituationIncome))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempMemberSituationExists(memberSituationToUpdate.ID))
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
            var TempMemberSituation = await _context.TempMemberSituations.FindAsync(ID);
            try
            {
                _context.TempMemberSituations.Remove(TempMemberSituation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(TempMemberSituation);
        }

        private bool TempMemberSituationExists(int id)
        {
            return _context.TempMemberSituations.Any(e => e.SituationID == id);
        }
    }
}
