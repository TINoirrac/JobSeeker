using System;
using System.Collections.Generic;

namespace JobPortal.Models
{
    public partial class JobPostActivity
    {
        public string UserProfileId { get; set; }
        public string JobPostId { get; set; }
        public DateTime ApplyDate { get; set; }

        public virtual JobPost? JobPost { get; set; } = null!;
        public virtual UserProfile? UserProfile { get; set; } = null!;
    }
}
