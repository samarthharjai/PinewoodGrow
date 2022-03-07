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
    public class ReceiptsController : Controller
    {
        private readonly GROWContext _context;

        public ReceiptsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {

           



            var gROWContext = _context.Receipts.Include(r => r.Household).Include(r => r.Payment).Include(r => r.Product).Include(r => r.ProductUnitPrice).Include(r => r.Volunteer);
            return View(await gROWContext.ToListAsync());

            
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Receipt = await _context.Receipts
                .Include(r => r.Household)
                .Include(r => r.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Receipt == null)
            {
                return NotFound();
            }

            return View(Receipt);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,Quantity,Total,SubTotal,ProductUnitPriceID,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropDownLists(Receipt);
            return View(Receipt);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Receipt = await _context.Receipts.FindAsync(id);
            if (Receipt == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(Receipt);
            return View(Receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,Total,SubTotal,ProductUnitPriceID,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt)
        {
            if (id != Receipt.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(Receipt.ID))
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
            PopulateDropDownLists(Receipt);
            return View(Receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Receipt = await _context.Receipts
                .Include(r => r.Household)
                .Include(r => r.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Receipt == null)
            {
                return NotFound();
            }

            return View(Receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Receipt = await _context.Receipts.FindAsync(id);
            _context.Receipts.Remove(Receipt);
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
            var x = new SelectList(query.OrderBy(p => p.ProductPrice), "ID", "ProductPrice", selectedId);
            return new SelectList(query.OrderBy(p => p.ProductPrice), "ID", "ProductPrice", selectedId);
        }

        private double GetUnitPrice(int id)
        {

            var prices = _context.ProductUnitPrices.Where(a => a.ProductID == id).ToList();
            var price = prices.First(a => a.Date == prices.Max(p => p.Date)).ProductPrice;
            return price;
        }

        private void PopulateDropDownLists(Receipt Receipt = null)
        {
            var productSelect = ProductsSelectList(null);

            ViewData["ProductID"] = productSelect;
            ViewData["ProductUnitPriceID"] = UnitPriceSelectList(null, null);
            /*ViewData["UnitPrice"] = GetUnitPrice((int)productSelect.SelectedValue);*/
            ViewData["HouseholdID"] = new SelectList(_context.Households, "ID", "FamilyName");
            ViewData["PaymentID"] = new SelectList(_context.Payments, "ID", "Type");
            //ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Name", Receipt.ProductID);
            //ViewData["ProductUnitPriceID"] = new SelectList(_context.ProductUnitPrices, "ID", "ProductPrice", Receipt.ProductUnitPriceID);
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "Name");
        }

        [HttpGet]
        public JsonResult GetUnitPrice(int? ID)
        {
            return Json(UnitPriceSelectList(ID, null));
        }


        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.ID == id);
        }
    }
}
