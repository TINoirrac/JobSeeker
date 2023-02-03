using JobPortal.Models;

namespace JobPortal.Services
{
    public class DataService : IDataService
    {
        private readonly JobPortalWebContext _dbcontext;
        public DataService(JobPortalWebContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
    }
}
