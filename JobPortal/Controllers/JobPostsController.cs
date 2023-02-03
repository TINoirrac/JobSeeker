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
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Controllers
{
    public class JobPostsController : Controller
    {
        private readonly JobPortalWebContext _context;
        private readonly UserManager<UserProfile> _userManager;
        private readonly List<string> employments = new List<string>()
        {
            "Bán thời gian",
            "Toàn thời gian",
            "Thực tập",
            "Làm việc từ xa"
        };
        public JobPostsController(JobPortalWebContext context,
            UserManager<UserProfile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: JobPosts
        public async Task<IActionResult> GetAllJobPosts(SearchJobPost search)
        {
            var jobtypes = _context.JobTypes.ToList();
            var citys = _context.Citys.ToList();
            var levels = _context.Levels.ToList();
            var jobpost = _context.JobPosts.Include(j => j.Company).Include(j => j.JobTypeActivities).ToList();
            if (!string.IsNullOrEmpty(search.SearchString))
                jobpost = jobpost.Where(j => j.NameJob.ToUpper().Contains(search.SearchString.ToUpper())).ToList();
            if (!string.IsNullOrEmpty(search.JobTypeId))
                jobpost = jobpost.Where(j => j.JobTypeActivities.Any(j => j.JobTypeId == search.JobTypeId)).ToList();
            if (!string.IsNullOrEmpty(search.City))
                jobpost = jobpost.Where(j => j.Company.City == search.City).ToList();
            if (!string.IsNullOrEmpty(search.Level))
                jobpost = jobpost.Where(j => j.Level == search.Level).ToList();
            if (!string.IsNullOrEmpty(search.EmployType))
                jobpost = jobpost.Where(j => j.EmploymentType == search.EmployType).ToList();
            var jobpostview = new SearchJobPost()
            {
                Posts = jobpost.Where(j=>j.Status).OrderByDescending(j => j.CreatedDate).ToList(),
            };
            ViewData["JobTypeId"] = new SelectList(jobtypes, "Id", "JobType1");
            ViewData["City"] = new SelectList(citys, "NameCity", "NameCity");
            ViewData["Level"] = new SelectList(levels, "NameLevel", "NameLevel");
            ViewData["EmployType"] = new SelectList(employments);
            ViewData["searchString"] = search.SearchString;
            return View(jobpostview);
        }
        [Authorize(Roles = "Employer,Administrator")]
        public async Task<IActionResult> GetJobPostByCompany(SearchJobPost search)
        {
            var user = await _userManager.GetUserAsync(User);
            var jobtypes = _context.JobTypes.ToList();
            var citys = _context.Citys.ToList();
            var levels = _context.Levels.ToList();
            var jobpost = _context.JobPosts.Include(j => j.Company).Include(j => j.JobTypeActivities)
                .Where(j => j.CompanyId.Equals(user.CompanyId)).ToList();
            if (!string.IsNullOrEmpty(search.SearchString))
                jobpost = jobpost.Where(j => j.NameJob.ToUpper().Contains(search.SearchString.ToUpper())).ToList();
            if (!string.IsNullOrEmpty(search.JobTypeId))
                jobpost = jobpost.Where(j => j.JobTypeActivities.Any(j => j.JobTypeId == search.JobTypeId)).ToList();
            if (!string.IsNullOrEmpty(search.Level))
                jobpost = jobpost.Where(j => j.Level == search.Level).ToList();
            if (!string.IsNullOrEmpty(search.EmployType))
                jobpost = jobpost.Where(j => j.EmploymentType == search.EmployType).ToList();
            var jobpostview = new SearchJobPost()
            {
                Posts = jobpost.OrderByDescending(j => j.CreatedDate).ToList(),
            };
            ViewData["JobTypeId"] = new SelectList(jobtypes, "Id", "JobType1");
            ViewData["Level"] = new SelectList(levels, "NameLevel", "NameLevel");
            ViewData["EmployType"] = new SelectList(employments);
            ViewData["searchString"] = search.SearchString;
            return View(jobpostview);
        }
        public async Task<IActionResult> Index()
        {
            var jobPortalWebContext = _context.JobPosts.Include(j => j.Company).OrderByDescending(j=>j.CreatedDate);
            return View(await jobPortalWebContext.ToListAsync());
        }
        // GET: JobPosts/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.JobPosts == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Company).ThenInclude(j => j.UserProfile)
                .Include(j => j.JobPostActivities).ThenInclude(j=>j.UserProfile)
                .Include(j => j.JobTypeActivities)
                .ThenInclude(p => p.JobType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }
        [Authorize(Roles = "Employer,Administrator")]
        // GET: JobPosts/Create
        public async Task<IActionResult> Create()
        {
            var jobtypes=await _context.JobTypes.ToListAsync();
            var levels = await _context.Levels.ToListAsync();
            ViewData["JobTypeId"] = new MultiSelectList(jobtypes, "Id", "JobType1");
            ViewData["Level"] = new SelectList(levels, "NameLevel", "NameLevel");
            ViewData["Employment"] = new SelectList(employments);
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NameJob,JobTypeId,CreatedDate,JobDescription,JobLocation,EmploymentType,Level,JobTypeIds")] CreatePostModel jobPost)
        {
            var user =await _userManager.GetUserAsync(User);
            jobPost.CompanyId = user.CompanyId;
            jobPost.Id = Guid.NewGuid().ToString();
            jobPost.CreatedDate = DateTime.Now;
            jobPost.Status = true;
            if (!ModelState.IsValid)
            {
                _context.Add(jobPost);
                if (jobPost.JobTypeIds != null)
                {
                    foreach (var jobTypeId in jobPost.JobTypeIds)
                    {
                        _context.Add(new JobTypeActivity()
                        {
                            JobTypeId = jobTypeId,
                            JobPostId = jobPost.Id,
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GetJobPostByCompany));
            }
            //ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "JobType1");
            return View(jobPost);
        }
        [Authorize(Roles = "Employer,Administrator")]
        // GET: JobPosts/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.JobPosts == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts.Include(p=>p.JobTypeActivities).FirstOrDefaultAsync(j=>j.Id==id);
            if (jobPost == null)
            {
                return NotFound();
            }
            var postEdit = new CreatePostModel()
            {
                Id = jobPost.Id,
                NameJob=jobPost.NameJob,
                CompanyId=jobPost.CompanyId,
                JobDescription = jobPost.JobDescription,
                JobTypeIds=jobPost.JobTypeActivities.Select(p=>p.JobTypeId).ToArray(),
            };
            var jobtypes = await _context.JobTypes.ToListAsync();
            var levels = await _context.Levels.ToListAsync();
            ViewData["JobTypeId"] = new MultiSelectList(jobtypes, "Id", "JobType1");
            ViewData["Level"] = new SelectList(levels, "NameLevel", "NameLevel");
            ViewData["Employment"] = new SelectList(employments);
            //ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "JobType1", jobPost.JobTypeId);
            return View(postEdit);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,NameJob,CreatedDate,JobDescription,JobLocation,EmploymentType,Level,JobTypeIds")] CreatePostModel jobPost)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id != jobPost.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                try
                {
                    var postUpdate = await _context.JobPosts.Include(p => p.JobTypeActivities).FirstOrDefaultAsync(j => j.Id == id);
                    if(postUpdate == null)
                        return NotFound();
                    postUpdate.Id = id;
                    postUpdate.NameJob = jobPost.NameJob;
                    //postUpdate.CompanyId = user.CompanyId;
                    postUpdate.JobDescription = jobPost.JobDescription;
                    postUpdate.CreatedDate = DateTime.Now;
                    postUpdate.EmploymentType=jobPost.EmploymentType;
                    postUpdate.Level = jobPost.Level;
                    //update JobType
                    if (jobPost.JobTypeIds == null)
                        jobPost.JobTypeIds = new string[] { };
                    var oldTypeIds=postUpdate.JobTypeActivities.Select(p=>p.JobTypeId).ToArray();
                    var newTypeIds = jobPost.JobTypeIds;

                    var removeTypes=from postType in postUpdate.JobTypeActivities
                                    where(!newTypeIds.Contains(postType.JobTypeId))
                                    select postType;
                    _context.JobTypeActivity.RemoveRange(removeTypes);
                    var addTypeIds=from postType in newTypeIds
                                   where !oldTypeIds.Contains(postType)
                                   select postType;
                    foreach(var TypeId in addTypeIds)
                    {
                        _context.JobTypeActivity.Add(new JobTypeActivity()
                        {
                            JobPostId = id,
                            JobTypeId = TypeId,
                        });
                    }
                    _context.Update(postUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GetJobPostByCompany));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            //ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "Id", "JobType1", jobPost.);
            return View(jobPost);
        }
        [Authorize(Roles="Employer")]
        [HttpPost]
        public async Task<IActionResult> Close(string id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);
            if (jobPost != null)
                jobPost.Status = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetJobPostByCompany));
        }
        [Authorize(Roles ="Employer")]
        [HttpPost]
        public async Task<IActionResult> Open(string id)
        {
            var jobPost=await _context.JobPosts.FindAsync(id);
            if(jobPost != null)
                jobPost.Status=true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetJobPostByCompany));
        }
        [Authorize(Roles = "Employer,Administrator")]
        // GET: JobPosts/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.JobPosts == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var jobPostActivities = await _context.JobPostActivities.Where(p => p.JobPostId == id).ToListAsync();
            if (jobPostActivities.Any())
            {
                for (int i = 0; i < jobPostActivities.Count; i++)
                {
                    var activity = jobPostActivities[i];
                    _context.JobPostActivities.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            var jobTypeActivities = await _context.JobTypeActivity.Where(p => p.JobPostId == id).ToListAsync();
            if (jobTypeActivities.Any())
            {
                for (int i = 0; i < jobTypeActivities.Count; i++)
                {
                    var activity = jobTypeActivities[i];
                    _context.JobTypeActivity.Remove(activity);
                }
                await _context.SaveChangesAsync();
            }
            if (_context.JobPosts == null)
            {
                return Problem("Entity set 'JobPortalWebContext.JobPosts'  is null.");
            }
            var jobPost = await _context.JobPosts.FindAsync(id);
            if (jobPost != null)
            {
                _context.JobPosts.Remove(jobPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GetJobPostByCompany));
        }

        private bool JobPostExists(string id)
        {
          return (_context.JobPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
