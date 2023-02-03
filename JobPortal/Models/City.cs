namespace JobPortal.Models
{
    public class City
    {
        public City()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string NameCity { get; set; }
    }
}
