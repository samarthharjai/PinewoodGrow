using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnaanAndrews_Canada_Games.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PinewoodGrow.Data;
using PinewoodGrow.Data.Repositorys;
using PinewoodGrow.Models;
using PinewoodGrow.Utilities;
using PinewoodGrow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using PinewoodGrow.Models.Temp;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class HouseholdsController : Controller
    {
        private readonly GROWContext _context;
        private readonly TravelDataRepository travelDataRepository = new TravelDataRepository();

        public HouseholdsController(GROWContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> DeleteTempHousehold(int ID)
        {
            var house = _context.TempHouseholds.FirstOrDefault(a => a.ID == ID);

            _context.Remove(house);
            await _context.SaveChangesAsync();


            //return View(await households.ToListAsync());
            return RedirectToAction(nameof(Index));
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
                .ThenInclude(a=> a.TravelDetail)
                .Include(h => h.MemberHouseholds)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (household == null)
            {
                return NotFound();
            }

            ViewData["TravelStats"] = household.IsFixedAddress? new TravelStats(household.Address.TravelDetail): new TravelStats();
            ViewData["IncomeStats"] = household.Members.OrderByDescending(a => a.Income).Select(a=> new IncomeStats{Name = a.FullName, Income = a.Income}).ToList();
            return View(household);
        }

        // GET: Households/Create
        public IActionResult Create()
        {
            var tempHousehold = new TempHousehold();
            _context.Add(tempHousehold);
            _context.SaveChanges();
            return View(tempHousehold);
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("FamilyName,Dependants")] Household household, string isFixedAddress, string Lat, string Lng, string AddressName, string postal, string city, string placeID)
        {
            household.IsFixedAddress = isFixedAddress == "true";
            household.AddressID = household.IsFixedAddress? await GetAddressID(Lat, Lng, AddressName, placeID, postal, city): (int?)null; 


            try
            {
                await _context.AddAsync(household);
                await _context.SaveChangesAsync();

                if (ModelState.IsValid)
                {
                   return RedirectToAction("AddMember", "Households", new { HouseholdID = household.ID });
                }

            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                ModelState.AddModelError("",
                    dex.GetBaseException().Message.Contains("UNIQUE")
                        ? "Unable to save: Duplicate Household Number."
                        : "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
            }

        

            PopulateAssignedMemberData(household);
            return View(new MemberHouseHoldModel() { Household = household, Member = null });
        }


        /*public async Task<IActionResult> AddMember(int HouseHoldID)
        {
            return RedirectToAction("AddMember", "Households");
        }*/
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember()
        {
            return RedirectToAction("AddMember", "Households", new { HouseholdID = HouseHoldID });
        }*/
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


            ViewData["AddressInfo"] = household.IsFixedAddress
                ? household.Address
                : new Address
                {
                    Latitude = 0,
                    Longitude = 0,
                    FullAddress = "No Fixed Address",
                    PostalCode = "No Fixed Address",
                    PlaceID = "",
                };

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
        public async Task<IActionResult> Edit(int id, string[] selectedOptions,string Lat, string Lng, string AddressName, string postal, string city, string placeID, string isFixedAddress)
        {
            var householdToupdate = await _context.Households
                .Include(d => d.Address)
                .Include(d=> d.Members)
                .Include(d => d.MemberHouseholds)
                .ThenInclude(d => d.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (householdToupdate == null)
            {
                return NotFound();
            }

            householdToupdate.IsFixedAddress = isFixedAddress == "true";

            /*household.FamilySize = householdToupdate.Members.Count + household.Dependants;*/

            householdToupdate.AddressID = householdToupdate.IsFixedAddress ? await GetAddressID(Lat, Lng, AddressName, placeID, postal, city) : (int?)null;


            if (await TryUpdateModelAsync(householdToupdate, ""))


            //Try updating it with the values posted
            if (await TryUpdateModelAsync<Household>(householdToupdate, "",
                d => d.ID, d=> d.AddressID, d=> d.Dependants, d=> d.FamilyName, d=> d.FamilySize))
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

        public PartialViewResult TempMemberList(int id)
        {
            ViewBag.TempMembers = _context.TempMembers.Where(a => a.TempHouseholdID == id)
                .Include(a=> a.Gender).Include(a=> a.MemberSituations)
                .ToList();

            return PartialView("_TempMemberList");
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

        private async Task<int> CreateAddress(Address address)
        {
            _context.Add(address);
            await _context.SaveChangesAsync();

            var (travel, store) = await TravelDataRepository.GetTravelTimes(address);

            if (_context.GroceryStores.All(a => a.ID != store.ID))
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
            }
            _context.Add(travel);
            await _context.SaveChangesAsync();
            return address.ID;
        }

        

        private async Task<int> GetAddressID(string Lat, string Lng, string AddressName, string placeID, string postal, string city)
        {

            var tempAddress = new Address()
            {
                FullAddress = AddressName,
                PostalCode = postal,
                City = city,
                PlaceID = placeID,
                Latitude = string.IsNullOrEmpty(Lat) ? (double?)null : Convert.ToDouble(Lat),
                Longitude = string.IsNullOrEmpty(Lng) ? (double?)null : Convert.ToDouble(Lng),
            };
            tempAddress = _context.Addresses.FirstOrDefault(a => a.PlaceID == tempAddress.PlaceID) ?? tempAddress;
            tempAddress.ID = tempAddress.ID == 0 ? await CreateAddress(tempAddress) : tempAddress.ID;
            return tempAddress.ID;

        }
        private bool MemberExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }

        public IActionResult AddMember(int? HouseholdID)
        {
     
            ViewDataReturnURL();
            if (!HouseholdID.HasValue)
            {
                return Redirect(ViewData["returnURL"].ToString());
            }
            var member = new Member
            {
                HouseholdID = HouseholdID.GetValueOrDefault()
            };

            PopulateAssignedDietaryData(member);
            PopulateAssignedSituationData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists();
            return View(member);
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


        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMember([Bind("ID,FirstName,LastName,Age,DOB,Telephone,Email,FamilySize,Income" +
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
                    return RedirectToAction("Details", "Households", new { @id = member.HouseholdID });
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
            PopulateAssignedSituationData(member);
            PopulateAssignedDietaryData(member);
            PopulateAssignedIllnessData(member);
            PopulateDropDownLists(member);
            return View(member);
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }






        



    }
}
