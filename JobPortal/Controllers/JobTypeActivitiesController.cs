using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace JobPortal.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class JobTypeActivitiesController : Controller
    {
        private readonly JobPortalWebContext _context;

        public JobTypeActivitiesController(JobPortalWebContext context)
        {
            _context = context;
        }

        // GET: JobTypeActivities
        public async Task<IActionResult> Index()
        {
            var jobPortalWebContext = _context.JobTypeActivity.Include(j => j.JobPost).Include(j => j.JobType);
            return View(await jobPortalWebContext.ToListAsync());
        }

        // GET: JobTypeActivities/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.JobTypeActivity == null)
            {
                return NotFound();
            }

            var jobTypeActivity = await _context.JobTypeActivity
                .Include(j => j.JobPost)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobTypeId == id);
            if (jobTypeActivity == null)
            {
                return NotFound();
            }

            return View(jobTypeActivity);
        }

        // GET: JobTypeActivities/Create
        public IActionResult Create()
        {
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "NameJob");
            ViewData["JobTypeId"] = new MultiSelectList(_context.JobTypes, "Id", "JobType1");
            return View();
        }

        // POST: JobTypeActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobTypeId,JobPostId")] JobTypeActivity jobTypeActivity)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(jobTypeActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "NameJob", jobTypeActivity.JobPostId);
            ViewData["JobTypeId"] = new MultiSelectList(_context.JobTypes, "Id", "JobType1", jobTypeActivity.JobTypeId);
            return View(jobTypeActivity);
        }

        // GET: JobTypeActivities/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.JobTypeActivity == null)
            {
                return NotFound();
            }

            var jobTypeActivity = await _context.JobTypeActivity.FindAsync(id);
            if (jobTypeActivity == null)
            {
                return NotFound();
            }
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "NameJob", jobTypeActivity.JobPostId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "JobType1", jobTypeActivity.JobTypeId);
            return View(jobTypeActivity);
        }

        // POST: JobTypeActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("JobTypeId,JobPostId")] JobTypeActivity jobTypeActivity)
        {
            if (id != jobTypeActivity.JobTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobTypeActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTypeActivityExists(jobTypeActivity.JobTypeId))
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
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Id", jobTypeActivity.JobPostId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "Id", jobTypeActivity.JobTypeId);
            return View(jobTypeActivity);
        }

        // GET: JobTypeActivities/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.JobTypeActivity == null)
            {
                return NotFound();
            }

            var jobTypeActivity = await _context.JobTypeActivity
                .Include(j => j.JobPost)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobTypeId == id);
            if (jobTypeActivity == null)
            {
                return NotFound();
            }

            return View(jobTypeActivity);
        }

        // POST: JobTypeActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.JobTypeActivity == null)
            {
                return Problem("Entity set 'JobPortalWebContext.JobTypeActivity'  is null.");
            }
            var jobTypeActivity = await _context.JobTypeActivity.FindAsync(id);
            if (jobTypeActivity != null)
            {
                _context.JobTypeActivity.Remove(jobTypeActivity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTypeActivityExists(string id)
        {
          return (_context.JobTypeActivity?.Any(e => e.JobTypeId == id)).GetValueOrDefault();
        }
    }
}
