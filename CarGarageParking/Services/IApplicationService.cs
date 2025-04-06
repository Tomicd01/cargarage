using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IApplicationService
    {
        public IEnumerable<Application> GetApplications();
        public Application GetApplicationById(int id);
        Application? GetApplicationByOwnerId(int ownerId);
        void CreateApplication(Application app);
        void UpdateApplication(Application app);
        void DeleteApplication(int id);
    }
}
