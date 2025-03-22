using CarGarageParking.Models;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class GarageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GarageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string name, string location, int? AvailableSpots)
        {
            IEnumerable<Garage> garages = _unitOfWork.GarageService.GetAllgarages();
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
           Garage garage = _unitOfWork.GarageService.GetGarageById(id);
           return View(garage);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Garage garage)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GarageService.CreateGarage(garage);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(garage);
        }

    }
}

























