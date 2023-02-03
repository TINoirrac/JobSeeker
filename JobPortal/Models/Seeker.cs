using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models
{
    public partial class Seeker
    {

        public string Id { get; set; }
        public string? JobTitle { get; set; }
        public string? Level { get; set; }
        public string? City { get; set; }
        public string? YOfExp { get; set; }
        public string? Introduction { get; set; }
        public string? CvUrl { get; set; }
        [NotMapped]
        public IFormFile? UploadCv { get; set; }

        public virtual UserProfile? UserProfile { get; set; }
    }
}
