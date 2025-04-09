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
        public VehicleInGarage? FindActiveVehicleInGarage(string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                Console.WriteLine("License plate is null");
                return null;
            }
            var result = _context.VehicleInGarages
                .Include(vig => vig.Vehicle)
                .ThenInclude(v => v.Application)
                .ThenInclude(a => a.Owner)
                .Include(vig => vig.Garage)
                .FirstOrDefault(vig => vig.Vehicle.LicensePlate.ToLower().Trim() == licensePlate.ToLower().Trim() && vig.IsVehicleStillInGarage == true);

            if(result == null)
            {
                Console.WriteLine($"No vehicle with license plate: {licensePlate}");
            }

            return result;
        }


        public void Update(VehicleInGarage vehicleInGarage)
        {
            _context.VehicleInGarages.Update(vehicleInGarage);
            _context.SaveChanges();
        }
    }
}
