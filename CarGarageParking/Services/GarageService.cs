using CarGarageParking.Models;

namespace CarGarageParking.Services
{
    public class GarageService : IGarageService
    {
        private readonly CarGarageParkingDbContext _context;

        public GarageService(CarGarageParkingDbContext context)
        {
            _context = context;
        }

        public void CreateGarage(Garage garage)
        {
            _context.Garages.Add(garage);
        }

        public void DeleteGarage(int id)
        {
            Garage garage = _context.Garages.Find(id);
            if (garage != null)
            {
                _context.Garages.Remove(garage);
            }
        }

        public IEnumerable<Garage> GetAllgarages()
        {
            return _context.Garages.ToList();
        }

        public Garage GetGarageById(int id)
        {
            return _context.Garages.Find(id);
        }

        public void UpdateGarage(Garage garage)
        {
            _context.Garages.Update(garage);
        }
    }
}
