using System;
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
using PinewoodGrow.ViewModels;
using PinewoodGrow.Utilities;

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
        public async Task<IActionResult> Index(string SearchString, int? page, int? pageSizeID, int? DietaryID, int? SituationID, int? IllnessID,
            string actionButton, string sortDirection = "asc", string sortField = "Member")
        {
            string[] sortOptions = new[] { "Member", "Age", "Family Size", "Income" };

            ViewData["Filtering"] = ""; //Asume not filtering

            ViewData["DietaryID"] = new SelectList(_context
                .Dietaries
                .OrderBy(d => d.Name), "ID", "Name");

            ViewData["SituationID"] = new SelectList(_context
                .Situations
                .OrderBy(s => s.Name), "ID", "Name");

            var members = from m in _context.Members
                //.Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Volunteer)
                .Include(m => m.Household)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
            select m;

            if (DietaryID.HasValue)
			{
                members = members.Where(m => m.MemberDietaries.Any(d => d.DietaryID == DietaryID));
                ViewData["Filtering"] = " show";
            }
            if (SituationID.HasValue)
            {
                members = members.Where(m => m.MemberSituations.Any(s => s.SituationID == SituationID));
                ViewData["Filtering"] = " show";
            }
            if (IllnessID.HasValue)
            {
                members = members.Where(m => m.MemberIllnesses.Any(i => i.IllnessID == IllnessID));
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                members = members.Where(m => m.LastName.ToUpper().Contains(SearchString.ToUpper())
                                        || m.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(actionButton))
            {
                page = 1;
                if (sortOptions.Contains(actionButton))
                {
                    if (actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }
            
            if (sortField == "Age")
            {
                if (sortDirection == "asc")
                {
                    members = members
                        .OrderByDescending(m => m.DOB);
                }
                else
                {
                    members = members
                        .OrderBy(m => m.DOB);
                }
            }
            else if (sortField == "Family Size")
            {
                if (sortDirection == "asc")
                {
                    members = members
                        .OrderBy(m => m.Household.FamilySize);
                }
                else
                {
                    members = members
                        .OrderByDescending(m => m.Household.FamilySize);
                }
            }
            else if (sortField == "Income")
            {
                if (sortDirection == "asc")
                {
                    members = members
                        .OrderBy(m => m.Income);
                }
                else
                {
                    members = members
                        .OrderByDescending(m => m.Income);
                }
            }
            else 
            {
                if (sortDirection == "asc")
                {
                    members = members
                        .OrderBy(m => m.LastName)
                        .ThenBy(m => m.FirstName);
                }
                else
                {
                    members = members
                        .OrderByDescending(m => m.LastName)
                        .ThenByDescending(m => m.FirstName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Member>.CreateAsync(members.AsNoTracking(), page ?? 1, pageSize);

            //return View(await members.ToListAsync());
            return View(pagedData);
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                //.Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Volunteer)
                .Include(m => m.Household)
                .Include(m => m.MemberDocuments)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .AsNoTracking()
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
            var member = new Member();
            PopulateAssignedDietaryData(member);
            PopulateAssignedSituationData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists();
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Age,DOB,Telephone,Email,FamilySize,Income" +
            ",Notes,Consent,VolunteerID,CompletedOn,HouseholdID,GenderID,ODSPIncome,OWIncome,CPPIncome,EIIncome,GAINSIncome,PSIncome,OIncome,EIncome")] Member member,
            string[] selectedDietaryOptions, string[] selectedSituationOptions, string[] selectedIllnessOptions, List<IFormFile> theFiles
            )
            //string Lat, string Lng, string AddressName, string postal, string city
        {
            try
            {
                //member.AddressID =  await GetAddressID(Lat, Lng, AddressName, postal, city);
               
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
                if (ModelState.IsValid)
                {
                    await AddDocumentsAsync(member, theFiles);
                    _context.Add(member);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException )
            {
                
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                
            }
            PopulateAssignedSituationData(member);
            PopulateAssignedDietaryData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists(member);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                //.Include(m => m.Address)
                .Include(m => m.MemberDocuments)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (member == null)
            {
                return NotFound();
            }

            PopulateAssignedSituationData(member);
            PopulateAssignedDietaryData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists(member);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string[] selectedDietaryOptions, string[] selectedSituationOptions
            , string[] selectedIllnessOptions, List<IFormFile> theFiles)
        {
            var memberToUpdate = await _context.Members
                //.Include(m => m.Address)
                .Include(m => m.MemberDocuments)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (memberToUpdate == null)
            {
                return NotFound();
            }

            UpdateMemberDietaries(selectedDietaryOptions, memberToUpdate);
            UpdateMemberSituation(selectedSituationOptions, memberToUpdate);
            UpdateMemberIllnesses(selectedIllnessOptions, memberToUpdate);

            if (await TryUpdateModelAsync<Member>(memberToUpdate, "", m => m.FirstName, m => m.LastName, m => m.Age, m => m.DOB, m => m.Telephone, 
                m => m.Email, m => m.Income, m => m.Notes, m => m.Consent, m => m.VolunteerID, m => m.CompletedOn, m => m.HouseholdID, m => m.GenderID,
                m => m.ODSPIncome, m => m.OWIncome, m => m.CPPIncome, m => m.EIIncome, m => m.GAINSIncome, m => m.PSIncome, m => m.OIncome, m => m.EIncome))
            //m => m.FamilySize, m => m.AddressID, m => m.Address
            {
                try
                {
                    await AddDocumentsAsync(memberToUpdate, theFiles);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { memberToUpdate.ID });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(memberToUpdate.ID))
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
            PopulateAssignedSituationData(memberToUpdate);
            PopulateAssignedDietaryData(memberToUpdate);
            PopulateAssignedIllnessData(memberToUpdate);
            PopulateDropDownLists(memberToUpdate);
            return View(memberToUpdate);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                //.Include(m => m.Address)
                .Include(m => m.Gender)
                .Include(m => m.Volunteer)
                .Include(m => m.Household)
                .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
                .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
                .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
                .AsNoTracking()
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
            var member = await _context.Members
               //.Include(m => m.Address)
               .Include(m => m.Gender)
               .Include(m => m.Volunteer)
               .Include(m => m.Household)
               .Include(m => m.MemberDietaries).ThenInclude(m => m.Dietary)
               .Include(m => m.MemberSituations).ThenInclude(m => m.Situation)
               .Include(m => m.MemberIllnesses).ThenInclude(m => m.Illness)
               .FirstOrDefaultAsync(m => m.ID == id);

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name", member?.VolunteerID);
            ViewData["HouseSummary"] = new SelectList(_context.Households, "ID", "HouseSummary", member?.HouseholdID);
        }

        public async Task<FileContentResult> Download(int id)
        {
            var theFile = await _context.UploadedFiles
                .Include(d => d.FileContent)
                .Where(f => f.ID == id)
                .FirstOrDefaultAsync();
            return File(theFile.FileContent.Content, theFile.FileContent.MimeType, theFile.FileName);
        }

        /*private async Task<int> GetAddressID(string Lat, string Lng, string AddressName, string postal, string city)
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
            return  (await _context.Addresses.FirstOrDefaultAsync(a => a.FullAddress == tempAddress.FullAddress && a.PostalCode == tempAddress.PostalCode)).ID;

        }*/


        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }
    }
}
