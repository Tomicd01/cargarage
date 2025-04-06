using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly CarGarageParkingDbContext _dbContext;

        public VehicleService(CarGarageParkingDbContext context)
        {
            _dbContext = context;
        }
        public void CreateVehicle(Vehicle vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);
        }

        public void DeleteVehicle(int id)
        {
            Vehicle vehicleDeleted = _dbContext.Vehicles.Find(id);
            if(vehicleDeleted != null)
            {
                _dbContext.Vehicles.Remove(vehicleDeleted);
            }   
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _dbContext.Vehicles.ToList();
        }

        public IEnumerable<Vehicle> GetVehicleByCondition(Func<Vehicle, bool> predicate)
        {
            return _dbContext.Vehicles.Where(predicate);
        }

        public Vehicle GetVehicleById(int id)
        {
            Vehicle vehicle = _dbContext.Vehicles.Find(id);
            return vehicle;
        }

        public Vehicle GetVehicleByLicensePlate(string licensePlate)
        {
            return _dbContext.Vehicles.FirstOrDefault(v => v.LicensePlate.ToLower().Trim() == licensePlate.ToLower().Trim());
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            _dbContext.Vehicles.Update(vehicle);
        }
    }
}
