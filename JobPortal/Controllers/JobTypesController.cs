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
    public class JobTypesController : Controller
    {
        private readonly JobPortalWebContext _context;

        public JobTypesController(JobPortalWebContext context)
        {
            _context = context;
        }

        // GET: JobTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.JobTypes.ToListAsync());
        }

        // GET: JobTypes/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.JobTypes == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // GET: JobTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobType1")] JobType jobType)
        {
            if (!ModelState.IsValid)
            {
                jobType.Id = Guid.NewGuid().ToString();
                _context.Add(jobType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobType);
        }

        // GET: JobTypes/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.JobTypes == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        // POST: JobTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,JobType1")] JobType jobType)
        {
            if (id != jobType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTypeExists(jobType.Id))
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
            return View(jobType);
        }

        // GET: JobTypes/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.JobTypes == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // POST: JobTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.JobTypes == null)
            {
                return Problem("Entity set 'JobPortalWebContext.JobTypes'  is null.");
            }
            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType != null)
            {
                _context.JobTypes.Remove(jobType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTypeExists(string id)
        {
          return (_context.JobTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
