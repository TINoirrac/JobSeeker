namespace JobPortal.Models
{
    public class SearchJobPost
    {
        public List<JobPost> Posts { get; set; }
        public string SearchString { get; set; }
        public string JobTypeId { get; set; }
        public string EmployType { get; set; }
        public string Level { get; set; }
        public string City { get; set; }

    }
}
