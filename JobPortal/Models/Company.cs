using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public partial class Company
    {
        public Company()
        {
            JobPosts = new HashSet<JobPost>();
        }

        public string Id { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? City { get; set; }
        public string? Location { get; set; }
        public string? ProfileDescription { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string? CompanyWebsiteUrl { get; set; }
        public string? CompanyLogoUrl { get; set; }
        [NotMapped]
        public IFormFile? UploadLogo { get; set; }
        public virtual UserProfile? UserProfile { get; set; }
        //public virtual City? City { get; set; }
        public virtual ICollection<JobPost> JobPosts { get; set; }
    }
}
