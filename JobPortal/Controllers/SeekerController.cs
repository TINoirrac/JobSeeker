using JobPortal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;


namespace JobPortal.Controllers
{
    [Authorize]
    public class SeekerController:Controller
    {
        private readonly JobPortalWebContext _context;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly static List<string> listyofexp = new List<string> { "<1năm", "1 năm", "1+năm", "2 năm", "2+năm", "3 năm", "3+năm", "4 năm", "4+năm" };

        public SeekerController(JobPortalWebContext context,UserManager<UserProfile> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Seeker == null)
            {
                return NotFound();
            }

            var seeker = await _context.Seeker.Include(j=>j.UserProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seeker == null)
            {
                return RedirectToAction("Create");
            }
            return View(seeker);
        }
        [Authorize(Roles ="Seeker")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobTitle,Level,YOfExp,Introduction")] Seeker Seeker)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.GetUserAsync(User);
                Seeker.Id = user.Id;
                _context.Add(Seeker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Seeker);
        }
        [Authorize(Roles ="Seeker")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Seeker == null)
            {
                return NotFound();
            }
            var seeker = await _context.Seeker.Include(j=>j.UserProfile).FirstOrDefaultAsync(j=>j.Id==id);
            if (seeker == null)
            {
                return NotFound();
            }
            ViewData["City"] = new SelectList(_context.Citys, "NameCity", "NameCity");
            ViewData["Level"] = new SelectList(_context.Levels, "NameLevel", "NameLevel");
            ViewData["YOfExp"] = new SelectList(listyofexp);
            return View(seeker);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,JobTitle,Level,City,YOfExp,Introduction,UploadCv")] Seeker newseeker)
        {
            if (id != newseeker.Id)
            {
                return NotFound();
            }
            var seeker = await _context.Seeker.Include(j => j.UserProfile).FirstOrDefaultAsync(j => j.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (newseeker.JobTitle != seeker.JobTitle)
                        seeker.JobTitle = newseeker.JobTitle;
                    if (newseeker.Level != seeker.Level)
                        seeker.Level = newseeker.Level;
                    if (newseeker.City != seeker.City)
                        seeker.City = newseeker.City;
                    if (newseeker.YOfExp != seeker.YOfExp)
                        seeker.YOfExp = newseeker.YOfExp;
                    if(newseeker.Introduction!=seeker.Introduction)
                        seeker.Introduction = newseeker.Introduction;
                    if (newseeker.UploadCv != null)
                    {
                        var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "cvs", newseeker.UploadCv.FileName);
                        using var filestream = new FileStream(filepath, FileMode.Create);
                        await newseeker.UploadCv.CopyToAsync(filestream);
                        newseeker.CvUrl = "/cvs/" + Path.GetFileNameWithoutExtension(newseeker.UploadCv.FileName) + Path.GetExtension(newseeker.UploadCv.FileName);
                    }
                    _context.Update(seeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerExists(newseeker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            ViewData["City"] = new SelectList(_context.Citys, "NameCity", "NameCity",seeker.City);
            ViewData["Level"] = new SelectList(_context.Levels, "NameLevel", "NameLevel",seeker.Level);
            ViewData["YOfExp"] = new SelectList(listyofexp,seeker.YOfExp);
            return RedirectToAction(nameof(Details), new {id=id});
        }
        [Authorize(Roles ="Seeker")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadCv(string id,[Bind("Id,UploadCv")]Seeker newseeker)
        {
            if(id != newseeker.Id)
            {
                return NotFound();
            }
            var seeker = await _context.Seeker.Include(j => j.UserProfile).FirstOrDefaultAsync(j => j.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (newseeker.UploadCv != null)
                    {
                        var filepath = Path.Combine(_webHostEnvironment.WebRootPath, "cvs", newseeker.UploadCv.FileName);
                        using var filestream = new FileStream(filepath, FileMode.Create);
                        await newseeker.UploadCv.CopyToAsync(filestream);
                        seeker.CvUrl = "/cvs/" + Path.GetFileNameWithoutExtension(newseeker.UploadCv.FileName) + Path.GetExtension(newseeker.UploadCv.FileName);
                    }
                    _context.Update(seeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerExists(seeker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return RedirectToAction(nameof(Details), new { id = id });
        }
        private bool SeekerExists(string id)
        {
            return (_context.Companies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
