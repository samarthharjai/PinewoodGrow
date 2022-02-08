using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PinewoodGrow.Data;
using PinewoodGrow.Models;
using PinewoodGrow.Utilities;
using PinewoodGrow.ViewModels;

namespace PinewoodGrow.Controllers
{
    public class HouseholdsController : Controller
    {
        private readonly GROWContext _context;

        public HouseholdsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Households
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            var households = from h in _context.Households
                             .Include(h => h.Members)
                             .Include(h => h.Address)
                             .Include(h => h.MemberHouseholds)
                             select h;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Household>.CreateAsync(households.AsNoTracking(), page ?? 1, pageSize);

            //return View(await households.ToListAsync());
            return View(pagedData);
        }

        // GET: Households/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .Include(h => h.Members)
                .Include(h => h.Address)
                .Include(h => h.MemberHouseholds)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // GET: Households/Create
        public IActionResult Create()
        {
            Household household = new Household();
            PopulateAssignedMemberData(household);
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HouseIncome,LICO")] Household household, string[] selectedOptions)
        {
            UpdateHouseholdMembers(selectedOptions, household);

            try
            {
                //Nothing to do for the BONUS becuase no Athletes can have this as their main sport
                //since it has not been created yet.
                UpdateHouseholdMembers(selectedOptions, household);

                if (ModelState.IsValid)
                {
                    _context.Add(household);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { household.ID });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            PopulateAssignedMemberData(household);
            return View(household);
        }

        // GET: Households/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .Include(d => d.Address)
                .Include(d => d.MemberHouseholds)
                .ThenInclude(d => d.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (household == null)
            {
                return NotFound();
            }

            PopulateAssignedMemberData(household);
            return View(household);
        }

        // POST: Households/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HouseIncome,LICO")] Household household, string[] selectedOptions)
        {
            var householdToupdate = await _context.Households
                .Include(d => d.Address)
                .Include(d => d.MemberHouseholds)
                .ThenInclude(d => d.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            //Check that you got it or exit with a not found error
            if (householdToupdate == null)
            {
                return NotFound();
            }

            UpdateHouseholdMembers(selectedOptions, householdToupdate);

            //Try updating it with the values posted
            if (await TryUpdateModelAsync<Household>(householdToupdate, "",
                d => d.ID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { householdToupdate.ID });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(householdToupdate.ID))
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

            PopulateAssignedMemberData(householdToupdate);
            return View(householdToupdate);
        }

        // GET: Households/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .Include(h => h.MemberHouseholds)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (household == null)
            {
                return NotFound();
            }

            return View(household);
        }

        // POST: Households/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var household = await _context.Households.FindAsync(id);
            _context.Households.Remove(household);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedMemberData(Household household)
        {
            //For this to work, you must have Included the child collection in the parent object
            //For the BONUS we leave out Athletes with this sport as their main one.
            var allOptions = _context.Members.Where(m => m.HouseholdID != household.ID);
            var currentOptionsHS = new HashSet<int>(household.MemberHouseholds.Select(b => b.MemberID));
            //Instead of one list with a boolean, we will make two lists
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.FullName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.FullName
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        private void UpdateHouseholdMembers(string[] selectedOptions, Household householdToUpdate)
        {
            //This is an alternate approach to what I demonstrated in class.
            //Instetad of trying to follow the logic in the tutorial, we are
            //just clearing them out and adding the selected ones back in.
            //Note: the earlier code is shown below in comments
            
            if (selectedOptions == null)
            {
                householdToUpdate.MemberHouseholds = new List<MemberHousehold>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(householdToUpdate.MemberHouseholds.Select(b => b.MemberID));
            foreach (var s in _context.Members)
            {
                if (selectedOptionsHS.Contains(s.ID.ToString()))//it is selected
                {
                    if (!currentOptionsHS.Contains(s.ID))//but not currently in the Doctor's collection - Add it!
                    {
                        householdToUpdate.MemberHouseholds.Add(new MemberHousehold
                        {
                            MemberID = s.ID,
                            HouseholdID = householdToUpdate.ID
                        });
                    }
                }
                else //not selected
                {
                    if (currentOptionsHS.Contains(s.ID))//but is currently in the Doctor's collection - Remove it!
                    {
                        MemberHousehold specToRemove = householdToUpdate.MemberHouseholds.FirstOrDefault(d => d.MemberID == s.ID);
                        _context.Remove(specToRemove);
                    }
                }
            }
        }

        private bool HouseholdExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }

        private bool MemberExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }
    }
}
