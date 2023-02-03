using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class UserProfile:IdentityUser
    {
        public UserProfile()
        {
            //ExperienceDetails = new HashSet<ExperienceDetail>();
            JobPostActivities = new HashSet<JobPostActivity>();
        }
        public override string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }
        public bool? Gender { get; set; }
        public string? UserImageUrl { get; set; }
        public string? CompanyId { get; set; }
        public string? SeekerId { get; set; }
        public virtual Company Company { get; set; }
        public virtual Seeker Seeker { get; set; }
        //public virtual ICollection<ExperienceDetail> ExperienceDetails { get; set; }
        public virtual ICollection<JobPostActivity> JobPostActivities { get; set; }
    }
}
