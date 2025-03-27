using CarGarageParking.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Services
{
    public class VehicleInGarageService : IVehicleInGarageService
    {
        private readonly CarGarageParkingDbContext _context;

        public VehicleInGarageService(CarGarageParkingDbContext context)
        {
            _context = context;
        }
        public void Create(VehicleInGarage vehicleInGarage)
        {
            _context.VehicleInGarages.Add(vehicleInGarage); 
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            VehicleInGarage vehicleInGarage = _context.VehicleInGarages.Find(id);

            if (vehicleInGarage != null)
            {
                _context.VehicleInGarages.Remove(vehicleInGarage);
                _context.SaveChanges();
            }

        }

        public IEnumerable<VehicleInGarage> GetAllVehiclesInGarage()
        {
            return _context.VehicleInGarages.Include(v => v.Vehicle).ToList();
        }

        public VehicleInGarage GetVehicleInGarage(int id)
        {
            return _context.VehicleInGarages.Find(id);
        }

        public void Update(VehicleInGarage vehicleInGarage)
        {
            _context.VehicleInGarages.Update(vehicleInGarage);
        }
    }
}
