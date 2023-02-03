using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobType
    {
        public JobType()
        {
            JobTypeActivities = new HashSet<JobTypeActivity>();
        }

        public string Id { get; set; }
        public string JobType1 { get; set; } = null!;
        public virtual ICollection<JobTypeActivity> JobTypeActivities { get; set; }
    }
}
