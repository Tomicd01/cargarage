using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.Garages
                .Include(g => g.VehicleInGarages)
                .ThenInclude(vg => vg.Vehicle)
                .ThenInclude(v => v.Owner)
                .ToList();
        }

        public Garage GetGarageById(int id)
        {
            return _context.Garages
                .Include(g => g.VehicleInGarages)
                .ThenInclude(vg => vg.Vehicle)
                .ThenInclude(v => v.Owner)
                .FirstOrDefault(g => g.GarageId == id);


        }

        public void UpdateGarage(Garage garage)
        {
            _context.Garages.Update(garage);
        }
    }
}
