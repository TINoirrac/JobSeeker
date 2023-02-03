namespace JobPortal.Models
{
    public class JobTypeActivity
    {
        public string JobTypeId { get; set; }
        public string JobPostId { get; set; }

        public virtual JobType JobType { get; set; } = null!;
        public virtual JobPost JobPost { get; set; } = null!;
    }
}
