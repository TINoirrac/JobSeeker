using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using JobPortal.Areas.Identity.Data;

namespace JobPortal.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly JobPortalWebContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompaniesController(JobPortalWebContext context,IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Companies
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Companies.ToListAsync());
        //}
        public async Task<IActionResult> GetCompanyByName(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return View(await _context.Companies.Where(p => p.CompanyName.Contains(searchString)).ToListAsync());
            }

            return View(await _context.Companies.ToListAsync());
        }
        public async Task<IActionResult> GetAllCompany()
        {
            return View(await _context.Companies.Include(j=>j.JobPosts).ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }
        // GET: Companies/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.Include(j=>j.JobPosts).ThenInclude(j=>j.JobTypeActivities).ThenInclude(j=>j.JobType).Include(j=>j.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["CompanyPhone"] = company?.UserProfile?.PhoneNumber;
            ViewData["CompanyEmail"] = company?.UserProfile?.Email;
            return View(company);
        }
        // GET: Companies/Create
        [Authorize(Roles = "Employer,Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,ProfileDescription,EstablishmentDate,CompanyWebsiteUrl,CompanyLogoUrl")] Company company)
        {
            if (!ModelState.IsValid)
            {
                company.Id = Guid.NewGuid().ToString();
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }
        [Authorize(Roles = "Employer,Administrator")]
        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company.CompanyLogoUrl == null||company.CompanyLogoUrl=="")
                company.CompanyLogoUrl = "/media/logo_default.png";
            if (company == null)
            {
                return NotFound();
            }
            ViewData["City"] = new SelectList(_context.Citys.ToList(), "NameCity", "NameCity");
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,CompanyName,City,Location,ProfileDescription,EstablishmentDate,CompanyWebsiteUrl,UploadLogo")] Company newcompany)
        {
            if (id != newcompany.Id)
            {
                return NotFound();
            }
            var company = await _context.Companies.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    if(newcompany.CompanyName!=company.CompanyName)
                        company.CompanyName = newcompany.CompanyName;
                    if(newcompany.ProfileDescription!=company.ProfileDescription)
                        company.ProfileDescription = newcompany.ProfileDescription;
                    if(newcompany.EstablishmentDate!=company.EstablishmentDate)
                        company.EstablishmentDate = newcompany.EstablishmentDate;
                    if (newcompany.CompanyWebsiteUrl != company.CompanyWebsiteUrl)
                        company.CompanyWebsiteUrl = newcompany.CompanyWebsiteUrl;
                    if(newcompany.City!=company.City)
                        company.City = newcompany.City;
                    if(newcompany.Location!=company.Location)
                        company.Location = newcompany.Location;
                    if (newcompany.UploadLogo != null)
                    {
                        var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "media", newcompany.UploadLogo.FileName);
                        using var filestream = new FileStream(filepath, FileMode.Create);
                        await newcompany.UploadLogo.CopyToAsync(filestream);
                        company.CompanyLogoUrl = "/media/" + Path.GetFileNameWithoutExtension(newcompany.UploadLogo.FileName) + Path.GetExtension(newcompany.UploadLogo.FileName);
                    }
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(newcompany.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return RedirectToAction(nameof(Details), new {id=id});
        }
        [Authorize(Roles = "Administrator,Employer")]
        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'JobPortalWebContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(string id)
        {
          return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
