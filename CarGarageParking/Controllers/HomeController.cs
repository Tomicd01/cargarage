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

            EnterVehicleModel evm = new EnterVehicleModel();
            evm.GarageId = garage.GarageId;
            evm.GarageName = garage.Name;
            evm.GarageLocation = garage.Location;

            Console.WriteLine(evm.GarageName);

            return View(evm);
        }

        [HttpPost]
        public IActionResult ConfirmVehicleEntry(int garageId, string licensePlate)
        {
            var existingVehicle = _unitOfWork.VehicleInGarageService.GetAllVehiclesInGarage().FirstOrDefault(v => v.Vehicle.LicensePlate == licensePlate && v.IsVehicleStillInGarage);
            
            if (existingVehicle != null)
            {
                ViewBag.ErrorMessage = "Vehicle is already in the garage!";
                return View("GarageResult", _unitOfWork.GarageService.GetAllgarages());
            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(garageId);
            if (garage == null || garage.IsFull)
            {
                ViewBag.ErrorMessage = "Garage is either full or not found!";
                return View("GarageResult", _unitOfWork.GarageService.GetAllgarages());
            }
            VehicleInGarage vig = new VehicleInGarage
            {
                GarageId = garageId,
                Vehicle = new Vehicle
                {
                    LicensePlate = licensePlate
                },
                EntryTime = DateTime.Now,
                HourlyRate = 25,
            };

            garage.CurrentOccupancy++;
            _unitOfWork.VehicleInGarageService.Create(vig);

            ViewBag.SuccessMessage = $"Vehicle with license plate number: {licensePlate}, successfully entered the garage {garage.Name}!";

            return View("GarageResult", _unitOfWork.GarageService.GetAllgarages());
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
