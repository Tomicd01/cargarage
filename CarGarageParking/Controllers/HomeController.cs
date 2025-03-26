using CarGarageParking.Models;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EnterVehicle()
        {
            return View();
        }

        public IActionResult SearchAGarage(string? search)
        {
            IEnumerable<Garage> garages = _unitOfWork.GarageService.GetAllgarages();

            var result = search.Trim().ToLower();

            if(search != null)
            {
                garages = garages.Where(g => g.Name.ToLower().Contains(result) || g.Location.ToLower().Contains(result));
            }
            else
            {
                return NotFound(); 
            }

            return View("GarageResult", garages);
        }

        [HttpGet]
        public IActionResult EnterVehicleDetails(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);

            if(id == null)
            {
                return NotFound();
            }

            EnterVehicleModel evm = new EnterVehicleModel
            {
                GarageId = garage.GarageId,
                GarageName = garage.Name,
                GarageLocation = garage.Location,
            };

            return View();
        }

        [HttpGet]
        public IActionResult GarageResult(IEnumerable<Garage> garages)
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExitVehicle()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }
    }
}
