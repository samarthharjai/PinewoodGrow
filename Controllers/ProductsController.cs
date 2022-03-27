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
using Microsoft.AspNetCore.Authorization;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly GROWContext _context;

        public ProductsController(GROWContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string SearchString, int? ProductTypeID, int? page, int? pageSizeID, string actionButton,
            string sortDirection = "asc", string sortField = "Name")
        {
            string[] sortOptions = new[] { "Name", "Unit Price", "Product Type" };

            ViewData["ProductTypeID"] = new SelectList(_context
                .ProductTypes
                .OrderBy(p => p.Type), "ID", "Type");

            var products = from p in _context.Products
                .Include(p => p.ProductType)
            select p;

            if (ProductTypeID.HasValue)
            {
                products = products.Where(p => p.ProductTypeID == ProductTypeID);
                ViewData["Filtering"] = " show";
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.ToUpper().Contains(SearchString.ToUpper()));
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

            if (sortField == "Name")
            {
                if (sortDirection == "asc")
                {
                    products = products
                        .OrderByDescending(p => p.Name);
                }
                else
                {
                    products = products
                        .OrderBy(p => p.Name);
                }
            }
            else if (sortField == "Unit Price")
            {
                if (sortDirection == "asc")
                {
                    products = products
                        .OrderByDescending(p => p.UnitPrice)
                        .ThenByDescending(p => p.Name);
                }
                else
                {
                    products = products
                        .OrderBy(p => p.UnitPrice)
                        .ThenBy(p => p.Name);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    products = products
                        .OrderByDescending(p => p.ProductType)
                        .ThenByDescending(p => p.Name);
                }
                else
                {
                    products = products
                        .OrderBy(p => p.ProductType)
                        .ThenBy(p => p.Name);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), page ?? 1, pageSize);

            //return View(await members.ToListAsync());
            return View(pagedData);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,UnitPrice,ProductTypeID")] Product product)
        {
			try
			{
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
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
                    ModelState.AddModelError("", "Unable to save: Duplicate Product ID.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator.");
                }
            }
            PopulateDropDownLists(product);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(product);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var productToUpdate = await _context.Products
                .FirstOrDefaultAsync(p => p.ID == id);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Product>(productToUpdate, "", p => p.Name, p => p.UnitPrice, p => p.ProductTypeID ))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { productToUpdate.ID });
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productToUpdate.ID))
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
                        ModelState.AddModelError("", "Unable to save: Duplicate Product ID.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." );
                    }
                }
            }
            PopulateDropDownLists(productToUpdate);
            return View(productToUpdate);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ID == id);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateDropDownLists(Product product = null)
        {
            ViewData["ProductTypeID"] = new SelectList(_context.ProductTypes, "ID", "Type", product?.ProductTypeID);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
