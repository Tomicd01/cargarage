using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IVehicleInGarageService
    {
        IEnumerable<VehicleInGarage> GetAllVehiclesInGarage();
        VehicleInGarage GetVehicleInGarage(int id);
        void Create(VehicleInGarage vehicleInGarage);
        void Update(VehicleInGarage vehicleInGarage);
        void Delete(int id);
    }
}
