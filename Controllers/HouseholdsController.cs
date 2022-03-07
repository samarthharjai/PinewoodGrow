﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var member = new Member();
            PopulateAssignedDietaryData(member);
            PopulateAssignedSituationData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists();
            Household household = new Household();
            PopulateAssignedMemberData(household);
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create(int Dependants, bool LICO, string FirstName, string LastName, DateTime DOB, string Telephone, string Email, double Income, string Notes, bool consent, int VolunteerID, DateTime CompletedOn, int GenderID,
                string[] selectedOptions, string[] selectedDietaryOptions, string[] selectedSituationOptions, string[] selectedIllnessOptions,List<IFormFile> theFiles,
        string Lat, string Lng, string AddressName, string postal, string city)
        {
            var household = new Household
            {
                AddressID = await GetAddressID(Lat, Lng, AddressName, postal, city),
                LICO = true,
                FamilySize = 1,
                Dependants = Dependants
            };

            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                DOB = DOB,
                Telephone = Telephone,
                Email = Email,
                Income = Income,
                Notes = Notes,
                Consent = true,
                VolunteerID = VolunteerID,
                CompletedOn = CompletedOn,
                GenderID = GenderID,
            };

            try
            {


                await _context.AddAsync(household);
                await _context.SaveChangesAsync();

                if (selectedDietaryOptions != null)
                {
                    foreach (var dietary in selectedDietaryOptions)
                    {
                        var dietaryToAdd = new MemberDietary { MemberID = member.ID, DietaryID = int.Parse(dietary) };
                        member.MemberDietaries.Add(dietaryToAdd);
                    }
                }
                if (selectedSituationOptions != null)
                {
                    foreach (var situation in selectedSituationOptions)
                    {
                        var situationToAdd = new MemberSituation { MemberID = member.ID, SituationID = int.Parse(situation) };
                        member.MemberSituations.Add(situationToAdd);
                    }
                }
                if (selectedIllnessOptions != null)
                {
                    foreach (var illness in selectedIllnessOptions)
                    {
                        var illnessToAdd = new MemberIllness { MemberID = member.ID, IllnessID = int.Parse(illness) };
                        member.MemberIllnesses.Add(illnessToAdd);
                    }
                }

                member.HouseholdID = household.ID;
                if (ModelState.IsValid)
                {
                    await AddDocumentsAsync(member, theFiles);
                    await _context.AddAsync(member);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { id = household.ID });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    ModelState.AddModelError("", "Unable to save: Duplicate Household Number." );
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." );
                }
            }
            PopulateAssignedSituationData(member);
            PopulateAssignedDietaryData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists(member);
            PopulateAssignedMemberData(household);
            return View(new MemberHouseHoldModel() { Household = household, Member = member });
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
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE"))
                    {
                        ModelState.AddModelError("", "Unable to save: Duplicate Household Number.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    }
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
        private void PopulateAssignedDietaryData(Member member)
        {
            //For this to work, you must have Included the PatientConditions 
            //in the Patient
            var allOptions = _context.Dietaries;
            var currentOptionIDs = new HashSet<int>(member.MemberDietaries.Select(b => b.DietaryID));
            var checkBoxes = new List<CheckOptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new CheckOptionVM
                {
                    ID = option.ID,
                    DisplayText = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["DietaryOptions"] = checkBoxes;
        }
        private void UpdateMemberDietaries(string[] selectedDietaryOptions, Member memberToUpdate)
        {
            if (selectedDietaryOptions == null)
            {
                memberToUpdate.MemberDietaries = new List<MemberDietary>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedDietaryOptions);
            var memberOptionsHS = new HashSet<int>
                (memberToUpdate.MemberDietaries.Select(d => d.DietaryID));
            foreach (var option in _context.Dietaries)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!memberOptionsHS.Contains(option.ID))
                    {
                        memberToUpdate.MemberDietaries.Add(new MemberDietary { MemberID = memberToUpdate.ID, DietaryID = option.ID });
                    }
                }
                else
                {
                    if (memberOptionsHS.Contains(option.ID))
                    {
                        MemberDietary dietaryToRemove = memberToUpdate.MemberDietaries.SingleOrDefault(d => d.DietaryID == option.ID);
                        _context.Remove(dietaryToRemove);
                    }
                }
            }
        }

        private void PopulateAssignedSituationData(Member member)
        {
            //For this to work, you must have Included the PatientConditions 
            //in the Patient
            var allOptions = _context.Situations;
            var currentOptionIDs = new HashSet<int>(member.MemberSituations.Select(b => b.SituationID));
            var checkBoxes = new List<CheckOptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new CheckOptionVM
                {
                    ID = option.ID,
                    DisplayText = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["SituationOptions"] = checkBoxes;
        }
        private void UpdateMemberSituation(string[] selectedSituationOptions, Member memberToUpdate)
        {
            if (selectedSituationOptions == null)
            {
                memberToUpdate.MemberSituations = new List<MemberSituation>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedSituationOptions);
            var memberOptionsHS = new HashSet<int>
                (memberToUpdate.MemberSituations.Select(s => s.SituationID));
            foreach (var option in _context.Dietaries)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!memberOptionsHS.Contains(option.ID))
                    {
                        memberToUpdate.MemberSituations.Add(new MemberSituation { MemberID = memberToUpdate.ID, SituationID = option.ID });
                    }
                }
                else
                {
                    if (memberOptionsHS.Contains(option.ID))
                    {
                        MemberSituation situationToRemove = memberToUpdate.MemberSituations.SingleOrDefault(s => s.SituationID == option.ID);
                        _context.Remove(situationToRemove);
                    }
                }
            }
        }

        private void PopulateAssignedIllnessData(Member member)
        {
            var allOptions = _context.Illnesses;
            var currentOptionIDs = new HashSet<int>(member.MemberIllnesses.Select(b => b.IllnessID));
            var checkBoxes = new List<CheckOptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new CheckOptionVM
                {
                    ID = option.ID,
                    DisplayText = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["IllnessOptions"] = checkBoxes;
        }
        private void UpdateMemberIllnesses(string[] selectedIllnessOptions, Member memberToUpdate)
        {
            if (selectedIllnessOptions == null)
            {
                memberToUpdate.MemberIllnesses = new List<MemberIllness>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedIllnessOptions);
            var memberOptionsHS = new HashSet<int>
                (memberToUpdate.MemberIllnesses.Select(i => i.IllnessID));
            foreach (var option in _context.Illnesses)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!memberOptionsHS.Contains(option.ID))
                    {
                        memberToUpdate.MemberIllnesses.Add(new MemberIllness { MemberID = memberToUpdate.ID, IllnessID = option.ID });
                    }
                }
                else
                {
                    if (memberOptionsHS.Contains(option.ID))
                    {
                        MemberIllness illnessToRemove = memberToUpdate.MemberIllnesses.SingleOrDefault(i => i.IllnessID == option.ID);
                        _context.Remove(illnessToRemove);
                    }
                }
            }
        }

        private async Task AddDocumentsAsync(Member member, List<IFormFile> theFiles)
        {
            foreach (var f in theFiles)
            {
                if (f != null)
                {
                    string mimeType = f.ContentType;
                    string fileName = Path.GetFileName(f.FileName);
                    long fileLength = f.Length;
                    if (!(fileName == "" || fileLength == 0))
                    {
                        MemberDocument d = new MemberDocument();
                        using (var memoryStream = new MemoryStream())
                        {
                            await f.CopyToAsync(memoryStream);
                            d.FileContent.Content = memoryStream.ToArray();
                        }
                        d.FileContent.MimeType = mimeType;
                        d.FileName = fileName;
                        member.MemberDocuments.Add(d);
                    };
                }
            }
        }

        private void PopulateDropDownLists(Member member = null)
        {
            //ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City", member?.AddressID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", member?.GenderID);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "ID", member?.HouseholdID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name", member?.VolunteerID);
        }

        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.FileContent.MimeType, theFile.FileName);
        }

        private bool HouseholdExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }


        private async Task<int> GetAddressID(string Lat, string Lng, string AddressName, string postal, string city)
        {

            var tempAddress = new Address()
            {
                FullAddress = AddressName,
                PostalCode = postal,
                City = city,
                Latitude = string.IsNullOrEmpty(Lat) ? (double?)null : Convert.ToDouble(Lat),
                Longitude = string.IsNullOrEmpty(Lng) ? (double?)null : Convert.ToDouble(Lng),
            };

            if (!_context.Addresses.Any(a => a.FullAddress == tempAddress.FullAddress && a.PostalCode == tempAddress.PostalCode))
            {
                _context.Addresses.Add(tempAddress);
                await _context.SaveChangesAsync();
            }
            return (await _context.Addresses.FirstOrDefaultAsync(a => a.FullAddress == tempAddress.FullAddress && a.PostalCode == tempAddress.PostalCode)).ID;

        }
        private bool MemberExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }
    }
}
