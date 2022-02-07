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
        public async Task<IActionResult> Index()
        {
            var households = from h in _context.Households
                .Include(h => h.Members)
                .Include(h => h.Address)
            select h;

            return View(await households.ToListAsync());
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
                .AsNoTracking()
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
            return View();
        }

        // POST: Households/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HouseIncome,LICO,FamilySize,Dependants")] Household household,
            string Lat, string Lng, string AddressName, string postal, string city)
        {
            try
			{
                household.AddressID = await GetAddressID(Lat, Lng, AddressName, postal, city);

                if (ModelState.IsValid)
                {
                    _context.Add(household);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException )
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

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
                .Include(h => h.Address)
                .FirstOrDefaultAsync(h => h.ID == id);

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
		public async Task<IActionResult> Edit(int id)
		{
			var householdToUpdate = await _context.Households
				.Include(h => h.Address)
				.FirstOrDefaultAsync(h => h.ID == id);

			if (householdToUpdate == null)
			{
				return NotFound();
			}

			if (await TryUpdateModelAsync<Household>(householdToUpdate, "", h => h.HouseIncome, h => h.LICO, h => h.FamilySize,
				h => h.Dependants, h => h.Address.ID, h => h.Address))
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToAction("Details", new { householdToUpdate.ID });
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!HouseholdExists(householdToUpdate.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}
			return View(householdToUpdate);
		}

		// GET: Households/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var household = await _context.Households
                .Include(h => h.Address)
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
            var household = await _context.Households
                .Include(h => h.Address)
                .FirstOrDefaultAsync(m => m.ID == id);

            _context.Households.Remove(household);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        private bool HouseholdExists(int id)
        {
            return _context.Households.Any(e => e.ID == id);
        }
    }
}
