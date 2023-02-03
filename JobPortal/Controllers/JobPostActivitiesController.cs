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
    [Authorize(Roles = "Administrator,Seeker")]
    public class JobPostActivitiesController : Controller
    {
        private readonly JobPortalWebContext _context;
        private readonly UserManager<UserProfile> _userManager;

        public JobPostActivitiesController(JobPortalWebContext context, UserManager<UserProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: JobPostActivities
        public async Task<IActionResult> Index()
        {
            var userId=_userManager.GetUserId(User);
            var jobPortalWebContext = _context.JobPostActivities.Include(j => j.JobPost).Include(j => j.UserProfile).Where(p=>p.UserProfileId==userId );
            return View(await jobPortalWebContext.ToListAsync());
        }

        // GET: JobPostActivities/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.JobPostActivities == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities
                .Include(j => j.JobPost)
                .Include(j => j.UserProfile)
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }

            return View(jobPostActivity);
        }

        // GET: JobPostActivities/Create
        public IActionResult Create()
        {
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Id");
            return View();
        }

        // POST: JobPostActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserProfileId,JobPostId,ApplyDate")] JobPostActivity jobPostActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPostActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Id", jobPostActivity.UserProfileId);
            return View(jobPostActivity);
        }
        [Authorize(Roles ="Seeker")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyJob(string id)
        {
            JobPostActivity jobPostActivity = new JobPostActivity();
            var userId = _userManager.GetUserId(User);
            jobPostActivity.JobPostId = id;
            jobPostActivity.UserProfileId = userId;
            jobPostActivity.ApplyDate = DateTime.Now;
            _context.Add(jobPostActivity);
            await _context.SaveChangesAsync();
            return RedirectToAction("GetAllJobPostApplied", "UserProfiles");
        }
        // GET: JobPostActivities/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.JobPostActivities == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities.FindAsync(id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Id", jobPostActivity.UserProfileId);
            return View(jobPostActivity);
        }

        // POST: JobPostActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserProfileId,JobPostId,ApplyDate")] JobPostActivity jobPostActivity)
        {
            if (id != jobPostActivity.UserProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPostActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostActivityExists(jobPostActivity.UserProfileId))
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
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobPostActivity.JobPostId);
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "Id", "Id", jobPostActivity.UserProfileId);
            return View(jobPostActivity);
        }

        // GET: JobPostActivities/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.JobPostActivities == null)
            {
                return NotFound();
            }

            var jobPostActivity = await _context.JobPostActivities
                .Include(j => j.JobPost)
                .Include(j => j.UserProfile)
                .FirstOrDefaultAsync(m => m.UserProfileId == id);
            if (jobPostActivity == null)
            {
                return NotFound();
            }

            return View(jobPostActivity);
        }

        // POST: JobPostActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.JobPostActivities == null)
            {
                return Problem("Entity set 'JobPortalWebContext.JobPostActivities'  is null.");
            }
            var jobPostActivity = await _context.JobPostActivities.FindAsync(id);
            if (jobPostActivity != null)
            {
                _context.JobPostActivities.Remove(jobPostActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostActivityExists(string id)
        {
          return (_context.JobPostActivities?.Any(e => e.UserProfileId == id)).GetValueOrDefault();
        }
        [Authorize(Roles ="Seeker")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelApplyJob(string id)
        {
            var userid=_userManager.GetUserId(User);
            var jobPostActivity = await _context.JobPostActivities.
                Where(s => s.UserProfileId == userid && s.JobPostId == id).FirstOrDefaultAsync();
            if(jobPostActivity != null)
                _context.Remove(jobPostActivity);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetAllJobPosts", "JobPosts");
        }
    }
}
