using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly CarGarageParkingDbContext _context;

        public ApplicationService(CarGarageParkingDbContext context)
        {
            _context = context;
        }
        public void CreateApplication(Application app)
        {
            _context.Applications.Add(app);
            _context.SaveChanges();
        }

        public void DeleteApplication(int id)
        {
            Application app = _context.Applications.Find(id);
            if (app != null)
            {
                _context.Applications.Remove(app);
                _context.SaveChanges();
            }
        }

        public Application GetApplicationById(int id)
        {
            return _context.Applications.Find(id);
        }

        public Application? GetApplicationByOwnerId(int ownerId)
        {
            return _context.Applications.Include(a => a.Owner).FirstOrDefault(a => a.OwnerId == ownerId);

        }

        public IEnumerable<Application> GetApplications()
        {
            return _context.Applications.ToList();
        }

        public void UpdateApplication(Application app)
        {
            _context.Applications.Update(app);
            _context.SaveChanges();
        }
    }
}
