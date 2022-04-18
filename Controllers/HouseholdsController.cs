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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PinewoodGrow.Models.Temp;
using SQLitePCL;

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
        public async Task<IActionResult> Index(int? page, int? pageSizeID, string SearchFamily, string SearchSize, string SearchID, string SearchAddress)
        {
            string[] sortOptions = new[] { "SearchFamily", "SearchSize", "SearchID", "SearchAddress" };

            ViewData["Filtering"] = ""; //Asume not filtering
            var households = from h in _context.Households
                         
                             .Include(h => h.Address)
                             .Include(h => h.Members).ThenInclude(m=> m.MemberSituations)
                             .Include(h=> h.LICOHistory)
                             select h;

            if (!String.IsNullOrEmpty(SearchFamily))
            {
                households = households.Where(m => m.FamilyName.ToUpper().Contains(SearchFamily.ToUpper()));
                ViewData["Filtering"] = "show";
            }
            if (!String.IsNullOrEmpty(SearchAddress))
            {
                households = households.Where(m => m.Address.FullAddress.ToUpper().Contains(SearchAddress.ToUpper()));
                ViewData["Filtering"] = "show";
            }
            if (!String.IsNullOrEmpty(SearchID))
            {
                try
                {
                    var searchID = Convert.ToInt32(SearchID);
                    households = households.Where(m => m.ID == searchID );
                    ViewData["Filtering"] = "show";
                }
                catch
                {
                }
            }
            if (!String.IsNullOrEmpty(SearchSize))
            {
                try
                {
                    var sizeID = Convert.ToInt32(SearchSize);
                    households = households.Where(m => m.FamilySize == sizeID);
                    ViewData["Filtering"] = "show";
                }
                catch
                {
                }
            }
            

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
                .Include(h => h.Address)
                .ThenInclude(a=> a.TravelDetail).ThenInclude(a=> a.GroceryStore)
                .Include(h => h.Members).ThenInclude(m=> m.MemberSituations)
                .Include(h=> h.Members).ThenInclude(h=> h.Gender)
                .Include(a=> a.LICOHistory).Include(a=> a.Dependant)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (household == null)
            {
                return NotFound();
            }
            var stats = household.Members.OrderByDescending(a => a.Income).Select(a => new IncomeStats { Name = a.FullName, Income = a.Income }).ToList();

            var situations = _context.Situations.Select(a => a).ToList();
            var memberIncomes = household.Members.Select(a=> a.MemberSituations).ToList();


            var IncomesByType = situations.Select(s => new IncomeStats
            {
                Name = s.Name,
                Income = memberIncomes.Select(a => a.Where(t => t.SituationID == s.ID).Sum(t => t.SituationIncome)).Sum()
            }).Where(a=> a.Income != 0).OrderByDescending(a=> a.Income).ToList();


            var orders = _context.Receipts
                .Include(a=> a.Volunteer)
                .Include(a=> a.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.ProductType)
                .Where(a => a.HouseholdID == household.ID)
                .OrderBy(a=> a.CreatedOn)
                .ToList();
            ViewData["Orders"] = orders;
            ViewData["TravelStats"] = (household.IsFixedAddress && household.Address.TravelDetail != null) ? new TravelStats(household.Address.TravelDetail): new TravelStats();
            ViewData["StoreName"] = (household.IsFixedAddress && household.Address.TravelDetail != null)
                ? household.Address.TravelDetail.GroceryStore.Name
                : "No Data";
            ViewData["IncomeStats"] = stats;
            ViewData["IncomeByType"] = IncomesByType;
            ViewData["Colors1"] = new PieChartData(stats).Colors;
            ViewData["Colors2"] = new PieChartData(IncomesByType).Colors;
            return View(household);
        }



        public async Task<IActionResult> OverrideLICO(int? ID)
        {
            if (ID == null) return NotFound();

            var licoInfo = await _context.LICOInfos
                .Where(a => a.HouseholdID == ID)
                .OrderBy(a => a.CreatedOn).FirstAsync();

            licoInfo.Override();

            _context.Update(licoInfo);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Households", new { id = ID });



        }
        public async Task<IActionResult> RemoveOverrideLICO(int? ID)
        {
            if (ID == null) return NotFound();

            var licoInfo = await _context.LICOInfos
                .Where(a => a.HouseholdID == ID)
                .OrderBy(a => a.CreatedOn).FirstAsync();

            licoInfo.RemoveOverride();

            _context.Update(licoInfo);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Households", new { id = ID });



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




        public PartialViewResult MemberSituationList(int id)
        {
            ViewBag.TempMemberSituations = _context.TempMemberSituations
                .Include(s => s.Situation)
                .Where(s => s.MemberID == id)
                .OrderBy(s => s.Situation.Name)
                .ToList();
            return PartialView("_MemberSituationList");
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
