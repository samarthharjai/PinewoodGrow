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
using Microsoft.AspNetCore.Authorization;
using PinewoodGrow.Utilities;

namespace PinewoodGrow.Controllers
{
    [Authorize(Roles = "Admin,Security")]
    public class VolunteersController : Controller
    {
        private readonly GROWContext _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly IMyEmailSender _emailSender;

        public VolunteersController(GROWContext context, ApplicationDbContext identityContext, IMyEmailSender emailSender)
        {
            _context = context;
            _identityContext = identityContext;
            _emailSender = emailSender;
        }

        // GET: Volunteers
        public async Task<IActionResult> Index(string SearchString, bool IsActive, int? page, int? pageSizeID, string actionButton,
            string sortDirection = "asc", string sortField = "Name")
        {
            string[] sortOptions = new[] { "Name"};

            var volunteers = from v in _context.Volunteers
            select v;


            if (IsActive == true)
            {
                volunteers = volunteers.Where(v => v.Active == false);
            }
            else if (IsActive == false)
            {
                volunteers = volunteers.Where(v => v.Active == true);
            }


            if (!String.IsNullOrEmpty(SearchString))
            {
                volunteers = volunteers.Where(v => v.LastName.ToUpper().Contains(SearchString.ToUpper())
                                              || v.FirstName.ToUpper().Contains(SearchString.ToUpper()));
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
                    volunteers = volunteers
                        .OrderBy(v => v.LastName)
                        .ThenBy(v => v.FirstName);
                }
                else
                {
                    volunteers = volunteers
                        .OrderByDescending(v => v.LastName)
                        .ThenByDescending(v => v.FirstName);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID);
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Volunteer>.CreateAsync(volunteers.AsNoTracking(), page ?? 1, pageSize);

            //return View(await members.ToListAsync());
            return View(pagedData);
        }

        // GET: Volunteer/Create
        public IActionResult Create()
        {
            Volunteer volunteer = new Volunteer();
            return View(volunteer);
        }

        // POST: Volunteer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone")] Volunteer volunteer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(volunteer);
                    await _context.SaveChangesAsync();

                    //Send Email to new Volunteer - commented out till email configured
                    //await InviteUserToRegister(volunteer, null);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                {
                    ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email addresses.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }

            return View(volunteer);
        }

        private async Task InviteUserToRegister(Volunteer volunteer, string message)
        {
            message ??= "Hello " + volunteer.FirstName + "<br /><p>Please navigate to:<br />" +
                        "<a href='https://https://pinewoodgrow.azurewebsites.net//' title='https://https://pinewoodgrow.azurewebsites.net//' target='_blank' rel='noopener'>" +
                        "https://summercamp2022.azurewebsites.net</a><br />" +
                        " and Register using " + volunteer.Email + " for email address.</p>";
            //Sending the email commented out until the service is configured.
            await _emailSender.SendOneAsync(volunteer.FullName, volunteer.Email,
                "Account Registration", message);
            TempData["message"] = "Invitation email sent to " + volunteer.FullName + " at " + volunteer.Email;

        }

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await _context.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool Active)
        {
            var volunteerToUpdate = await _context.Volunteers
                .FirstOrDefaultAsync(v => v.ID == id);
            if (volunteerToUpdate == null)
            {
                return NotFound();
            }

            //Note the current Email and Active Status
            bool ActiveStatus = volunteerToUpdate.Active;
            string currentEmail = volunteerToUpdate.Email;

            if (await TryUpdateModelAsync<Volunteer>(volunteerToUpdate, "",
                v => v.FirstName, v => v.LastName, v => v.Email, v => v.Phone, v => v.Active))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    //Delete Login if you are making them inactive
                    if (volunteerToUpdate.Active == false && ActiveStatus == true)
                    {
                        //This deletes the user's login from the security system
                        await DeleteIdentityUser(volunteerToUpdate.Email);

                    }
                    //Delete old Login if you Changed the email
                    if (volunteerToUpdate.Email != currentEmail)
                    {
                        //This deletes the user's login from the security system
                        await DeleteIdentityUser(currentEmail);
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteerToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("Email", "Unable to save changes. Remember, you cannot have duplicate Email addresses.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    }
                }
            }
            return View(volunteerToUpdate);
        }

        /*public IActionResult Roles()
        {
            var RolesU = _context.RolesWithUsers
                .AsNoTracking()
                .ToList();
            return View(RolesU);
        }*/

        private async Task DeleteIdentityUser(string Email)
        {
            var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            if (userToDelete != null)
            {
                _identityContext.Users.Remove(userToDelete);
                await _identityContext.SaveChangesAsync();
            }
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(v => v.ID == id);
        }
    }
}
