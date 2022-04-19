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
using System.IO;
using System.Text;
using System.Data;
using System.Xml;
using SelectPdf;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using PinewoodGrow.ViewModels;

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
            string[] sortOptions = new[] { "Family Name", "VolunteerID", "Member", "Total" };

            ViewData["VolunteerID"] = new SelectList(_context.Volunteers, "ID", "FullName");

            var receipts = from p in _context.Receipts
                           .Include(r => r.Household)
                           .Include(r => r.Member)
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
            if (!String.IsNullOrEmpty(SearchString))
            {
                receipts = receipts.Where(p => p.Member.LastName.ToUpper().Contains(SearchString.ToUpper()));
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
            else if (sortField == "Member")
            {
                if (sortDirection == "asc")
                {
                    receipts = receipts
                        .OrderByDescending(p => p.Member.LastName)
                        .ThenByDescending(p => p.Member.FirstName);
                }
                else
                {
                    receipts = receipts
                        .OrderBy(p => p.Member.LastName)
                        .ThenByDescending(p => p.Member.FirstName);
                }
            }
            else if (sortField == "Total")
            {
                if (sortDirection == "asc")
                {
                    receipts = receipts
                        .OrderByDescending(p => p.Total)
                        .ThenByDescending(p => p.Household.FamilyName);
                }
                else
                {
                    receipts = receipts
                        .OrderBy(p => p.Total)
                        .ThenByDescending(p => p.Household.FamilyName);
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
            TempData["AlertMessage"] = "Email Sent Successfully....!";
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
                .Include(r => r.Member)
                .Include(r => r.Payment)
                .Include(r => r.Product)
                .Include(r => r.ProductUnitPrice)
                .Include(r => r.ProductType)
                .Include(r => r.Volunteer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Receipt == null)
            {
                TempData["AlertMessage"] = "Order Saved Successfully....!";
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
        public async Task<IActionResult> Create([Bind("ID,ProductTypeID,MemberID,ProductID,Quantity,Total,SubTotal,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt, double unitPrice, string saleData)
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
                TempData["AlertMessage"] = "Order Saved Successfully....!";

                return RedirectToAction("Details", "Receipts", new { id = Receipt.ID });
                //return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductTypeID,MemberID,ProductID,Quantity,Total,SubTotal,CompletedOn,HouseholdID,VolunteerID,PaymentID")] Receipt Receipt, double unitPrice)
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
                .Include(r => r.Member)
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
            var query = from c in _context.Products.Include(c => c.ProductType)
                        select c;
            if (ProductTypeID.HasValue)
            {
                query = query.Where(p => p.ProductTypeID == ProductTypeID);
            }
            var x = new SelectList(query, "ID", "Name", selectedId);
            return new SelectList(query, "ID", "Name", selectedId);
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
            ViewData["MemberID"] = new SelectList(_context.Members, "ID", "FullName");
            ViewData["ProductTypeID"] = ProductTypeSelectList(null);
        }

        //public ActionResult PrintSalarySlip(int id)
        //{
        //    var report = new ActionAsPdf("IndexById", new { id = id });
        //    return report;
        //}


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


        private SelectList getMemberSelectList(int? HouseholdID, int? selectedId)
        {
            //The ProvinceID has been added so we can filter by it.
            var query = from c in _context.Members.Include(c => c.Household)
                select c;
            if (HouseholdID.HasValue)
            {
                query = query.Where(p => p.HouseholdID == HouseholdID);
            }
            var x = new SelectList(query, "ID", "FullName", selectedId);
            return new SelectList(query, "ID", "FullName", selectedId);
        }
        [HttpGet]
        public JsonResult GetMembers(int? ID)
        {
            return Json(getMemberSelectList(ID, null));
        }


        public async Task<IActionResult> Notification1(/*int? id, */string Subject, string emailContent)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //Product t = await _context.Products.FindAsync(id);

            //ViewData["id"] = id;
            //ViewData["ProductName"] = t.Name;

            if (string.IsNullOrEmpty(Subject) || string.IsNullOrEmpty(emailContent))
            {
                ViewData["Message"] = "You must enter both a Subject and some message Content before sending the message.";
            }
            else
            {
                int folksCount = 0;
                try
                {
                    //Send a Notice.
                    List<EmailAddress> folks = (from p in _context.Members
                                                select new EmailAddress
                                                {
                                                    Name = p.FullName,
                                                    Address = p.Email
                                                }).ToList();
                    folksCount = folks.Count();
                    if (folksCount > 0)
                    {
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("PinewoodSolutions1@gmail.com");
                        //message.To.Add(new MailAddress("taewoo0109@gmail.com"));
                        //message.To.Add(new MailAddress("tryoo1@ncstudents.niagaracollege.ca"));
                        foreach (EmailAddress v in folks)
                        {
                            message.To.Add(new MailAddress(v.Address, v.Name));
                        }
                        message.Subject = Subject;
                        message.IsBodyHtml = true; //to make message body as html  
                        message.Body = "<p>" + emailContent + "</p><p>Please access the <strong>Niagara College</strong> web site to review.</p>";
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com"; //for gmail host  
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("PinewoodSolutions1@gmail.com", "Pinewood11");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Send(message);
                        //var msg = new EmailMessage()
                        //{
                        //    ToAddresses = folks,
                        //    Subject = Subject,
                        //    Content = "<p>" + emailContent + "</p><p>Please access the <strong>Niagara College</strong> web site to review.</p>"

                        //};
                        //await _emailSender.SendToManyAsync(msg);
                        //ViewData["Message"] = "Message sent to " + folksCount + " Member"
                        //    + ((folksCount == 1) ? "." : "s.");
                    }
                    else
                    {
                        ViewData["Message"] = "Message NOT sent!";
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = ex.GetBaseException().Message;
                    ViewData["Message"] = "Error: Could not send email message to the " + folksCount + " Member"
                        + ((folksCount == 1) ? "" : "s");
                }
            }
            return View();
        }


        private bool ReceiptExists(int id)
        {
            return _context.Receipts.Any(e => e.ID == id);
        }


   
    }
}
