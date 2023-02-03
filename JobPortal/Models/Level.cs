namespace JobPortal.Models
{
    public class Level
    {
        public Level()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string NameLevel { get; set; }
    }
}
