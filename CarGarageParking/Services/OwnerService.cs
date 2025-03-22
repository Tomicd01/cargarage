using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly CarGarageParkingDbContext _dbContext;

        public OwnerService(CarGarageParkingDbContext context)
        {
            _dbContext = context;
        }  
        public void CreateOwner(Owner owner)
        {
            _dbContext.Owners.Add(owner);
        }

        public void DeleteOwner(int id)
        {
            Owner ownerDeleted = _dbContext.Owners.Find(id);
            if (ownerDeleted != null)
            {
                _dbContext.Owners.Remove(ownerDeleted);
            }
        }

        public IEnumerable<Owner> GetAllOwnersWithVehicles()
        {
            return _dbContext.Owners.Include(o => o.Vehicles).ToList();
        }

        public Owner GetOwnerById(int id)
        {
            Owner owner = _dbContext.Owners.Find(id);
            return owner;
        }

        public void UpdateOwner(Owner owner)
        {
            _dbContext.Owners.Update(owner);
        }
    }
}
