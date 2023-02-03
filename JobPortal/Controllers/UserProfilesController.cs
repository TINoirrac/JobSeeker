using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private readonly JobPortalWebContext _context;
        private readonly UserManager<UserProfile> _userManager;

        public UserProfilesController(JobPortalWebContext context, UserManager<UserProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: UserProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserProfiles.ToListAsync());
        }
        [Authorize(Roles ="Seeker")]
        public async Task<IActionResult> GetAllJobPostApplied()
        {

            var userId = _userManager.GetUserId(User);
            var jobPortalWebContext = _context.JobPostActivities.Include(j => j.JobPost.Company).Include(j => j.JobPost.JobTypeActivities).ThenInclude(j=>j.JobType).Include(j => j.UserProfile.Seeker).Where(p => p.UserProfileId == userId);
            return View(await jobPortalWebContext.ToListAsync());
        }
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> GetAllSeekerApplied()
        {
            var user=await _userManager.GetUserAsync(User);
            var jobPortalWebContext = _context.JobPostActivities.Include(j => j.JobPost.Company).Include(j => j.JobPost).Include(j => j.UserProfile).ThenInclude(j=>j.Seeker).Where(j => j.JobPost.CompanyId == user.CompanyId);
            return View(await jobPortalWebContext.ToListAsync());
        }
        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.UserProfiles == null)
            {
                return NotFound();
            }

            var UserProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (UserProfile == null)
            {
                return NotFound();
            }

            return View(UserProfile);
        }

        // GET: UserProfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,Gender,UserImageUrl,NetUserId")] UserProfile UserProfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(UserProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(UserProfile);
        }

        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.UserProfiles == null)
            {
                return NotFound();
            }

            var UserProfile = await _context.UserProfiles.FindAsync(id);
            if (UserProfile == null)
            {
                return NotFound();
            }
            return View(UserProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FullName,DateOfBirth,Gender,UserImageUrl")] UserProfile UserProfile)
        {
            if (id != UserProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(UserProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            _context.Update(UserProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(UserProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.UserProfiles == null)
            {
                return NotFound();
            }

            var UserProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (UserProfile == null)
            {
                return NotFound();
            }

            return View(UserProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var company = await _context.Companies.Include(j=>j.UserProfile).Where(p => p.UserProfile.Id == id).ToListAsync();
            if (company.Any())
            {
                for (int i = 0; i < company.Count; i++)
                {
                    var activity = company[i];
                    _context.Companies.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            var seeker = await _context.Seeker.Where(p => p.Id == id).ToListAsync();
            if (seeker.Any())
            {
                for (int i = 0; i < seeker.Count; i++)
                {
                    var activity = seeker[i];
                    _context.Seeker.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            var roleuser=await _context.UserRoles.Where(p=>p.UserId == id).ToListAsync();
            if (roleuser.Any())
            {
                for (int i = 0; i < roleuser.Count; i++)
                {
                    var activity=roleuser[i];
                    _context.UserRoles.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            if (_context.UserProfiles == null)
            {
                return Problem("Entity set 'JobPortalWebContext.UserProfiles'  is null.");
            }
            var UserProfile = await _context.UserProfiles.FindAsync(id);
            if (UserProfile != null)
            {
                _context.UserProfiles.Remove(UserProfile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(string id)
        {
          return (_context.UserProfiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
