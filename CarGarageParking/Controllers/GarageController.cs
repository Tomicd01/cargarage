using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class GarageController : Controller
    {
        private readonly CarGarageParkingDbContext _context;

        public GarageController(CarGarageParkingDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string name, string location, int? AvailableSpots)
        {
            IEnumerable<Garage> garages = _context.Garages;
            if (name != null)
            {
                garages = garages.Where(g => g.Name.Trim().ToLower() == name.Trim().ToLower());
            }
            if (location != null)
            {
                garages = garages.Where(g => g.Location.Trim().ToLower() == location.Trim().ToLower());
            }
            if (AvailableSpots.HasValue)
            {
                Console.WriteLine(AvailableSpots);
                garages = garages.Where(g => g.AvailableSpots >= AvailableSpots);
            }
            return View(garages);
        }

        public  IActionResult Info(int id)
        {
           Garage garage = _context.Garages.FirstOrDefault(g => g.GarageId == id);
           return View(garage);
        }

    }
}

























