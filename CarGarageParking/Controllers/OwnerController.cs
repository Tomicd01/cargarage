using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
        private readonly CarGarageParkingDbContext _context;
        public OwnerController(CarGarageParkingDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {
            var owners = _context.Owners.Include(o => o.Vehicles).AsQueryable();

            if (firstName != null)
            {
                owners = owners.Where(o => o.FirstName.ToLower().Trim() == firstName.ToLower().Trim());
            }
            if(lastName != null)
            {
                owners = owners.Where(o => o.LastName.ToLower().Trim() == lastName.ToLower().Trim());
            }
            if (numberOfCars.HasValue)
            {
                owners = owners.Where(o => o.Vehicles.Count() == numberOfCars);
            }

            return View(owners);

        }

        public IActionResult Details(int id)
        {
            Owner singleOwner = _context.Owners.Find(id);
            singleOwner.Vehicles = _context.Vehicles.Where(v => v.OwnerId == id).ToList();

            return View(singleOwner);
        }

    }
}
