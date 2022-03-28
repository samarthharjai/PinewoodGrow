using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PinewoodGrow.Data;
using PinewoodGrow.Models;
using PinewoodGrow.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class ReceiptsController : Controller
    {
        private readonly GROWContext _context;

        public ReceiptsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index(string SearchString, int? VolunteerID, int? page, int? pageSizeID, string actionButton,
            string sortDirection = "asc", string sortField = "Name")
        {
            string[] sortOptions = new[] { "Family Name", "VolunteerID" };

            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "FullName");

            var receipts = from p in _context.Receipts
                           .Include(r => r.Household)
                           .Include(r => r.Payment)
                           .Include(r => r.Product)
                           .Include(r => r.ProductUnitPrice)
                           .Include(r => r.ProductType)
                           .Include(r => r.Volunteer)
                           select p;

            if (VolunteerID.HasValue)
            {
                receipts = receipts.Where(p => p.VolunteerID == VolunteerID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                receipts = receipts.Where(p => p.Household.FamilyName.ToUpper().Contains(SearchString.ToUpper()));
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

            if (sortField == "Family Name")
            {
                if (sortDirection == "asc")
                {
                    receipts = receipts
                        .OrderByDescending(p => p.Household.FamilyName);
                }
                else
                {
                    receipts = receipts
                        .OrderBy(p => p.Household.FamilyName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    receipts = receipts
                        .OrderByDescending(p => p.Volunteer.LastName)
                        .ThenByDescending(p => p.Volunteer.FirstName);
                }
                else
                {
                    receipts = receipts
                        .OrderBy(p => p.Volunteer.LastName)
                        .ThenByDescending(p => p.Volunteer.FirstName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Receipt>.CreateAsync(receipts.AsNoTracking(), page ?? 1, pageSize);

            //return View(await members.ToListAsync());
            return View(pagedData);

            /*var gROWContext = _context.Receipts.Include(r => r.Household).Include(r => r.Payment).Include(r => r.Product).Include(r => r.ProductUnitPrice).Include(r => r.Volunteer);
            return View(await gROWContext.ToListAsync());*/
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
                .Include(r => r.ProductType)
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
        public async Task<IActionResult> Create([Bind("ID,ProductTypeID,ProductID,Quantity,Total,SubTotal,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt, double unitPrice)
        {

            var Price = UnitPrice(Receipt.ProductID);
            if (Math.Abs(Price.ProductPrice - unitPrice) > 0.001)
            {
                var productUnitPriceToAdd = new ProductUnitPrice
                {
                    ProductID = Receipt.ProductID,
                    ProductPrice = unitPrice,
                    Date = DateTime.UtcNow
                };
                _context.ProductUnitPrices.Add(productUnitPriceToAdd);
                await _context.SaveChangesAsync();
                Receipt.ProductUnitPriceID = productUnitPriceToAdd.ID;
            }
            else
            {
                Receipt.ProductUnitPriceID = Price.ID;
            }
            

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
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductTypeID,ProductID,Quantity,Total,SubTotal,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt, double unitPrice)
        {
            if (id != Receipt.ID)
            {
                return NotFound();
            }

            var Price = UnitPrice(Receipt.ProductID);
            if (Math.Abs(Price.ProductPrice - unitPrice) > 0.001)
            {
                var productUnitPriceToAdd = new ProductUnitPrice
                {
                    ProductID = Receipt.ProductID,
                    ProductPrice = unitPrice,
                    Date = DateTime.UtcNow
                };
                _context.ProductUnitPrices.Add(productUnitPriceToAdd);
                await _context.SaveChangesAsync();
                Receipt.ProductUnitPriceID = productUnitPriceToAdd.ID;
            }
            else
            {
                Receipt.ProductUnitPriceID = Price.ID;
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
                .Include(r => r.ProductType)
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

        private SelectList ProductTypeSelectList(int? selectedId)
        {
            return new SelectList(_context.ProductTypes
                .OrderBy(d => d.Type), "ID", "Type", selectedId);
        }

        private SelectList ProductCatSelectList(int? ProductTypeID, int? selectedId)
        {
            //The ProvinceID has been added so we can filter by it.
            var query = from c in _context.ProductTypes.Include(c => c.Products)
                        select c;
            if (ProductTypeID.HasValue)
            {
                query = query.Where(p => p.ID == ProductTypeID);
            }
            return new SelectList(query.OrderBy(p => p.Type), "Type", "Type", selectedId);
        }


        private ProductUnitPrice UnitPrice(int id)
        {
            if (id == 0) return null;
            var prices = _context.ProductUnitPrices.Where(a => a.ProductID == id).ToList();
            if (prices.Count == 0) return null;
            var product = prices.First(a => a.Date == prices.Max(p => p.Date));
            return product;
        }

        private void PopulateDropDownLists(Receipt Receipt = null)
        {
            var productSelect = ProductsSelectList(null);
            ViewData["ProductID"] = productSelect;
            ViewData["UnitPrice"] = UnitPrice(Convert.ToInt32(productSelect.First().Value)).ProductPrice;
            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "FullName");
            ViewData["HouseSummary"] = new SelectList(_context.Households, "ID", "HouseSummary");
            ViewData["PaymentID"] = new SelectList(_context.Payments, "ID", "Type");
            ViewData["ProductTypeID"] = ProductTypeSelectList(null);
        }

        [HttpGet]
        public JsonResult GetUnitPrice(int? ID)
        {
            var price = UnitPrice(ID ?? 0);
            return Json((price?.ProductPrice ?? 0));
        }

        [HttpGet]
        public JsonResult GetProductType(int? ID)
        {
            return Json(ProductCatSelectList(ID, null));
        }

        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.ID == id);
        }


   
    }
}
