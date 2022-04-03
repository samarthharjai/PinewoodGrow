using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models.Temp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using PinewoodGrow.Models;
using PinewoodGrow.ViewModels;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class TempMembersController : Controller
    {
        private readonly GROWContext _context;
        public PartialViewResult MemberSituationList(int id)
        {
            ViewBag.MemberSituations = _context.TempMemberSituations
                .Include(s => s.Situation)
                .Where(s => s.MemberID == id)
                .OrderBy(s => s.Situation.Name)
                .ToList();
            return PartialView("_MemberSituationList");
        }
        public TempMembersController(GROWContext context)
        {
            _context = context;
        }


        public PartialViewResult CreateTempMember(int? ID)
        {

            var tempMember = new TempMember()
            {
                TempHouseholdID = ID,
                CompletedOn = DateTime.Today
            };
            _context.TempMembers.Add(tempMember);
            _context.SaveChanges();





            PopulateAssignedDietaryData(tempMember);
            PopulateAssignedSituationData(tempMember);
            PopulateAssignedIllnessData(tempMember);
            PopulateAssignedDietaryData(tempMember);
            PopulateDropDownLists();

            ViewData["MemberIncomeTypes"] = _context.Situations.Select(a => a);
            ViewData["TempMemberID"] = tempMember.ID;
            ViewData["HouseholdID"] = ID.GetValueOrDefault();

            return PartialView("_CreateTempMember", tempMember);
        }



        public PartialViewResult EditTempMember(int ID)
        {
            //Get the TempMemberSituation to edit
            var tempMember = _context.TempMembers.Include(a=> a.MemberSituations).Include(a=> a.MemberDietaries).Include(a=> a.MemberIllnesses).FirstOrDefault(a => a.ID == ID);
            PopulateAssignedDietaryData(tempMember);
            PopulateAssignedSituationData(tempMember);
            PopulateAssignedIllnessData(tempMember);
            PopulateAssignedDietaryData(tempMember);
            PopulateDropDownLists();

            ViewData["MemberIncomeTypes"] = _context.Situations.Select(a => a);
            ViewData["TempMemberID"] = tempMember.ID;
            ViewData["HouseholdID"] = ID;

            return PartialView("_EditTempMember", tempMember);
        }

        public PartialViewResult DeleteTempMember(int ID)
        {
            //Get the one to delete
            var TempMember = _context.TempMembers.FirstOrDefault(a => a.ID == ID);


            return PartialView("_DeleteTempMember", TempMember);
        }









        // GET: TempMembers
        public async Task<IActionResult> Index()
        {
            var gROWContext = _context.TempMembers.Include(t => t.Gender).Include(t => t.Household)
                .Include(t => t.TempHousehold).Include(t => t.Volunteer);
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
            /*var now = DateTime.Now;
         var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);*/

            var Tempmember = new TempMember()
            {
            };
            _context.TempMembers.Add(Tempmember);
            _context.SaveChanges();


 


            PopulateAssignedDietaryData(Tempmember);
            PopulateAssignedSituationData(Tempmember);
            PopulateAssignedIllnessData(Tempmember);
            PopulateDropDownLists();

            ViewData["MemberIncomeTypes"] = _context.Situations.Select(a => a);
            ViewData["TempMemberID"] = Tempmember.ID;
            return View(Tempmember);
        }

        // POST: TempMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Age,DOB,Telephone,Email,FamilySize,Income" +
            ",Notes,Consent,VolunteerID,CompletedOn,HouseholdID,GenderID")] TempMember member,
                 string[] selectedOptions, string[] selectedSituationOptions, string[] selectedIllnessOptions, List<IFormFile> theFiles, int TempID, int memID
                 )
        //string Lat, string Lng, string AddressName, string postal, string city
        {
            try
            {
                //member.AddressID =  await GetAddressID(Lat, Lng, AddressName, postal, city);
                UpdateMemberDietaries(selectedOptions, member);

                if (ModelState.IsValid)
                {
                    await AddDocumentsAsync(member, theFiles);
                    _context.Add(member);
                    await _context.SaveChangesAsync();

                    var Situations = _context.TempMemberSituations.Where(a => a.MemberID == TempID).Select(a => new MemberSituation
                    { MemberID = member.ID, SituationID = a.SituationID, SituationIncome = a.SituationIncome }).ToList();

                    await _context.AddRangeAsync(Situations);
                    await _context.SaveChangesAsync();

                    /*if (selectedDietaryOptions != null)
                    {
                        foreach (var dietary in selectedDietaryOptions)
                        {
                            var dietaryToAdd = new MemberDietary { MemberID = member.ID, DietaryID = int.Parse(dietary) };
                            member.MemberDietaries.Add(dietaryToAdd);
                        }
                    }*/
           
                    if (selectedIllnessOptions != null)
                    {
                        foreach (var illness in selectedIllnessOptions)
                        {
                            var illnessToAdd = new TempMemberIllness { MemberID = member.ID, IllnessID = int.Parse(illness) };
                            member.MemberIllnesses.Add(illnessToAdd);
                        }
                    }


                    return RedirectToAction(nameof(Index));
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
                    ModelState.AddModelError("", "Unable to save: Duplicate Email Addresses.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateAssignedSituationData(member);
            PopulateAssignedDietaryData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists(member);
            return View(member);
        }


        // POST: TempMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int TempID, int id, string[] selectedOptions, string[] selectedSituationOptions
            , string[] selectedIllnessOptions, List<IFormFile> theFiles)
        {
            id = TempID;
            var tempMemberToUpdate = await _context.TempMembers
                //.Include(m => m.Address)
                .Include(m => m.MemberDocuments)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .FirstOrDefaultAsync(m => m.ID == TempID);

            if (tempMemberToUpdate == null)
            {
                return NotFound();
            }

            UpdateMemberDietaries(selectedOptions, tempMemberToUpdate);
            UpdateMemberSituation(selectedSituationOptions, tempMemberToUpdate);
            UpdateMemberIllnesses(selectedIllnessOptions, tempMemberToUpdate);

            if (await TryUpdateModelAsync<TempMember>(tempMemberToUpdate, "", m => m.FirstName, m => m.LastName,
                    m => m.DOB, m => m.Telephone,
                    m => m.Email, m => m.Notes, m => m.Consent, m => m.VolunteerID, m => m.CompletedOn,
                    m => m.HouseholdID, m => m.GenderID))
                //m => m.FamilySize, m => m.AddressID, m => m.Address
            {
                try
                {
                    await AddDocumentsAsync(tempMemberToUpdate, theFiles);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("",
                        "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TempMemberExists(tempMemberToUpdate.ID))
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
                        ModelState.AddModelError("", "Unable to save: Duplicate Email Addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("",
                            "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }

            PopulateAssignedSituationData(tempMemberToUpdate);
            PopulateAssignedDietaryData(tempMemberToUpdate);
            PopulateAssignedIllnessData(tempMemberToUpdate);
            PopulateDropDownLists(tempMemberToUpdate);
            return View(tempMemberToUpdate);
        }

        // GET: TempMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tempMember = await _context.TempMembers
                //.Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Volunteer)
                .Include(m => m.Household)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .AsNoTracking()
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

        private bool TempMemberExists(int id)
        {
            return _context.TempMembers.Any(e => e.ID == id);
        }


        private void PopulateAssignedDietaryData(TempMember member)
        {
            //For this to work, you must have Included the child collection in the parent object
            var allOptions = _context.Dietaries;
            var currentOptionsHS = new HashSet<int>(member.MemberDietaries.Select(b => b.DietaryID));
            //Instead of one list with a boolean, we will make two lists
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var d in allOptions)
            {
                if (currentOptionsHS.Contains(d.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = d.ID,
                        DisplayText = d.Name
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = d.ID,
                        DisplayText = d.Name
                    });
                }
            }
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

            ViewData["DietOptions"] = checkBoxes;

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }
        private List<CheckOptionVM> DietaryCheckboxList(string skip)
        {
            //default query if no values to avoid
            var DietaryQuery = _context.Dietaries
                .OrderBy(d => d.Name);
            if (!String.IsNullOrEmpty(skip))
            {
                //Conver the string to an array of integers
                //so we can make sure we leave them out of the data we download
                string[] avoidStrings = skip.Split(',');
                int[] skipKeys = Array.ConvertAll(avoidStrings, s => int.Parse(s));
                DietaryQuery = _context.Dietaries.OrderBy(d => d.Name);
                return DietaryQuery.Select(a => new CheckOptionVM
                {
                    ID = a.ID,
                    DisplayText = a.Name,
                    Assigned = skipKeys.Contains(a.ID),
                    Name = "selectedDietaryOptions"
                }).ToList();

            }


            return DietaryQuery.Select(a => new CheckOptionVM
            {
                ID = a.ID,
                DisplayText = a.Name,
            }).ToList();
        }
        [HttpGet]
        public JsonResult GetDietariesCheckbox( string skip)
        {
            return Json(DietaryCheckboxList(skip));
        }

        private void UpdateMemberDietaries(string[] selectedOptions, TempMember tempMemberToUpdate)
        {
            if (selectedOptions == null)
            {
                tempMemberToUpdate.MemberDietaries = new List<TempMemberDietary>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(tempMemberToUpdate.MemberDietaries.Select(b => b.DietaryID));
            foreach (var d in _context.Dietaries)
            {
                if (selectedOptionsHS.Contains(d.ID.ToString()))//it is selected
                {
                    if (!currentOptionsHS.Contains(d.ID))//but not currently in the Member's collection - Add it!
                    {
                        tempMemberToUpdate.MemberDietaries.Add(new TempMemberDietary
                        {
                            DietaryID = d.ID,
                            MemberID = tempMemberToUpdate.ID
                        });
                    }
                }
                else //not selected
                {
                    if (currentOptionsHS.Contains(d.ID))//but is currently in the Member's collection - Remove it!
                    {
                        TempMemberDietary dietToRemove = tempMemberToUpdate.MemberDietaries.FirstOrDefault(m => m.DietaryID == d.ID);
                        _context.Remove(dietToRemove);
                    }
                }
            }
        }

        private void PopulateAssignedSituationData(TempMember member)
        {
            //For this to work, you must have Included the PatientConditions 
            //in the Patient
            var allOptions = _context.Situations;
            var currentOptionIDs = new HashSet<int>(member.MemberSituations.Select(b => b.SituationID));
            var checkBoxes = new List<IncomeOption>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new IncomeOption
                {
                    ID = option.ID,
                    Name = option.Name,
                    Summary = option.Name,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["SituationOptions"] = checkBoxes;
        }
        private void UpdateMemberSituation(string[] selectedSituationOptions, TempMember tempMemberToUpdate)
        {
            if (selectedSituationOptions == null)
            {
                tempMemberToUpdate.MemberSituations = new List<TempMemberSituation>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedSituationOptions);
            var memberOptionsHS = new HashSet<int>
                (tempMemberToUpdate.MemberSituations.Select(s => s.SituationID));
            foreach (var option in _context.Dietaries)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!memberOptionsHS.Contains(option.ID))
                    {
                        tempMemberToUpdate.MemberSituations.Add(new TempMemberSituation { MemberID = tempMemberToUpdate.ID, SituationID = option.ID });
                    }
                }
                else
                {
                    if (memberOptionsHS.Contains(option.ID))
                    {
                        TempMemberSituation situationToRemove = tempMemberToUpdate.MemberSituations.SingleOrDefault(s => s.SituationID == option.ID);
                        _context.Remove(situationToRemove);
                    }
                }
            }
        }

        private void PopulateAssignedIllnessData(TempMember member)
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
        private void UpdateMemberIllnesses(string[] selectedIllnessOptions, TempMember tempMemberToUpdate)
        {
            if (selectedIllnessOptions == null)
            {
                tempMemberToUpdate.MemberIllnesses = new List<TempMemberIllness>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedIllnessOptions);
            var memberOptionsHS = new HashSet<int>
                (tempMemberToUpdate.MemberIllnesses.Select(i => i.IllnessID));
            foreach (var option in _context.Illnesses)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!memberOptionsHS.Contains(option.ID))
                    {
                        tempMemberToUpdate.MemberIllnesses.Add(new TempMemberIllness { MemberID = tempMemberToUpdate.ID, IllnessID = option.ID });
                    }
                }
                else
                {
                    if (memberOptionsHS.Contains(option.ID))
                    {
                        TempMemberIllness illnessToRemove = tempMemberToUpdate.MemberIllnesses.SingleOrDefault(i => i.IllnessID == option.ID);
                        _context.Remove(illnessToRemove);
                    }
                }
            }
        }

        private async Task AddDocumentsAsync(TempMember member, List<IFormFile> theFiles)
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
                        var d = new TempMemberDocument();
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

        //For Adding Dietary
        private SelectList DietarySelectList(string skip)
        {
            //default query if no values to avoid
            var DietaryQuery = _context.Dietaries
                .OrderBy(d => d.Name);
            if (!String.IsNullOrEmpty(skip))
            {
                //Conver the string to an array of integers
                //so we can make sure we leave them out of the data we download
                string[] avoidStrings = skip.Split(',');
                int[] skipKeys = Array.ConvertAll(avoidStrings, s => int.Parse(s));
                DietaryQuery = _context.Dietaries
                    .Where(s => !skipKeys.Contains(s.ID))
                .OrderBy(d => d.Name);
            }
            return new SelectList(DietaryQuery, "ID", "Name");
        }
        [HttpGet]
        public JsonResult GetDietaries(string skip)
        {
            return Json(DietarySelectList(skip));
        }

        private void PopulateDropDownLists(TempMember member = null)
        {
            //ViewData["AddressID"] = new SelectList(_context.Addresses, "ID", "City", member?.AddressID);
            ViewData["GenderID"] = new SelectList(_context.Genders, "ID", "Name", member?.GenderID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "FullName", member?.VolunteerID);
            ViewData["HouseSummary"] = new SelectList(_context.Households, "ID", "HouseSummary", member?.HouseholdID);
            ViewData["MemberSituationID"] = new SelectList(_context.MemberSituations, "ID", "Summary", member?.MemberSituations);
        }


        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.FileContent.MimeType, theFile.FileName);
        }
    }
}
