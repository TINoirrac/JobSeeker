using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobPost
    {
        public JobPost()
        {
            JobPostActivities = new HashSet<JobPostActivity>();
            JobTypeActivities = new HashSet<JobTypeActivity>();
        }

        public string Id { get; set; }
        public string NameJob { get; set; }
        public string CompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string JobDescription { get; set; } = null!;
        public string? EmploymentType { get; set; }
        public string? Level { get; set; }
        public bool Status { get; set; }

        public virtual Company? Company { get; set; }
        public virtual ICollection<JobPostActivity> JobPostActivities { get; set; }
        public virtual ICollection<JobTypeActivity> JobTypeActivities { get; set; }
    }
}
