using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public interface IGarageService
    {
        IEnumerable<Garage> GetAllgarages();
        Garage GetGarageById(int id);
        void CreateGarage(Garage garage);
        void UpdateGarage(Garage garage);
        void DeleteGarage(int id);
    }
}
