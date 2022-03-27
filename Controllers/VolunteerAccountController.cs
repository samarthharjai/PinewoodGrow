using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinewoodGrow.Data;
using PinewoodGrow.Models;
using PinewoodGrow.Utilities;
using PinewoodGrow.ViewModels;

namespace PinewoodGrow.Controllers
{
    [Authorize]
    public class VolunteerAccountController : Controller
    {
        private readonly GROWContext _context;

        public VolunteerAccountController(GROWContext context)
        {
            _context = context;
        }

        // GET: VolunteerAccount
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: VolunteerAccount/Details/5
        public async Task<IActionResult> Details()
        {

            var volunteer = await _context.Volunteers
               .Where(c => c.Email == User.Identity.Name)
               .Select(c => new VolunteerVM
               {
                   ID = c.ID,
                   FirstName = c.FirstName,
                   LastName = c.LastName,
                   Phone = c.Phone,
                   Email = c.Email,
                   Active = c.Active
               })
               .FirstOrDefaultAsync();
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // GET: VolunteerAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            var volunteer = await _context.Volunteers
                .Where(c => c.Email == User.Identity.Name)
                .Select(c => new VolunteerVM
                {
                    ID = c.ID,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Phone = c.Phone,
                    Email = c.Email,
                    Active = c.Active
                })
                .FirstOrDefaultAsync();
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // POST: VolunteerAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var volunteerToUpdate = await _context.Volunteers
                .FirstOrDefaultAsync(m => m.ID == id);

            //Note: Using TryUpdateModel we do not need to invoke the ViewModel
            //Only allow some properties to be updated
            if (await TryUpdateModelAsync<Volunteer>(volunteerToUpdate, "",
                c => c.FirstName, c => c.LastName, c => c.Phone))
            {
                try
                {
                    _context.Update(volunteerToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(volunteerToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteerToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit "
                                + "was modified by another user after you received your values.  You need to go back and try your edit again.");
                    }
                }
                catch (DbUpdateException)
                {
                    //Since we do not allow changing the email, we cannot introduce a duplicate
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            return View(volunteerToUpdate);

        }

        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private bool VolunteerExists(int id)
        {
            return _context.Volunteers.Any(e => e.ID == id);
        }
    }

}

