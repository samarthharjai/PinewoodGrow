using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models;

namespace PinewoodGrow.Controllers
{
    public class RecieptsController : Controller
    {
        private readonly GROWContext _context;

        public RecieptsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Reciepts
        public async Task<IActionResult> Index()
        {

           



            var gROWContext = _context.Reciepts.Include(r => r.Household).Include(r => r.Payment).Include(r => r.Product).Include(r => r.ProductUnitPrice).Include(r => r.Volunteer);
            return View(await gROWContext.ToListAsync());

            
        }

        // GET: Reciepts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciept = await _context.Reciepts
                .Include(r => r.Household)
                .Include(r => r.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reciept == null)
            {
                return NotFound();
            }

            return View(reciept);
        }

        // GET: Reciepts/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Reciepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,Quantity,Total,SubTotal,ProductUnitPriceID,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Reciept reciept)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reciept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(reciept);
            return View(reciept);
        }

        // GET: Reciepts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciept = await _context.Reciepts.FindAsync(id);
            if (reciept == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(reciept);
            return View(reciept);
        }

        // POST: Reciepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,Total,SubTotal,ProductUnitPriceID,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Reciept reciept)
        {
            if (id != reciept.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reciept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecieptExists(reciept.ID))
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
            PopulateDropDownLists(reciept);
            return View(reciept);
        }

        // GET: Reciepts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reciept = await _context.Reciepts
                .Include(r => r.Household)
                .Include(r => r.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reciept == null)
            {
                return NotFound();
            }

            return View(reciept);
        }

        // POST: Reciepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reciept = await _context.Reciepts.FindAsync(id);
            _context.Reciepts.Remove(reciept);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private SelectList ProductsSelectList(int? selectedId)
        {
            return new SelectList(_context.Products
                .OrderBy(d => d.Name), "ID", "Name", selectedId);
        }


        private SelectList UnitPriceSelectList(int? ProductID, int? selectedId)
        {
            //The ProvinceID has been added so we can filter by it.
            var query = from c in _context.ProductUnitPrices.Include(c => c.Product)
                        select c;
            if (ProductID.HasValue)
            {
                query = query.Where(p => p.ProductID == ProductID);
            }
            return new SelectList(query.OrderBy(p => p.ProductPrice), "ID", "ProductPrice", selectedId);
        }

        private void PopulateDropDownLists(Reciept reciept = null)
        {
            ViewData["ProductID"] = ProductsSelectList(null);
            ViewData["ProductUnitPriceID"] = UnitPriceSelectList(null, null);
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName");
            ViewData["PaymentID"] = new SelectList(_context.Payments, "ID", "Type");
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name", reciept.ProductID);
            //ViewData["ProductUnitPriceID"] = new SelectList(_context.ProductUnitPrices, "ID", "ProductPrice", reciept.ProductUnitPriceID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name");
        }

        [HttpGet]
        public JsonResult GetUnitPrice(int? ID)
        {
            return Json(UnitPriceSelectList(ID, null));
        }


        private bool RecieptExists(int id)
        {
            return _context.Reciepts.Any(e => e.ID == id);
        }
    }
}
