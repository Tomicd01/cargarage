using CarGarageParking.Models;
using CarGarageParking.Models.ViewModel;
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

        public IActionResult SearchAGarage(string? search, int page = 1)
        {
            IEnumerable<Garage> garages = _unitOfWork.GarageService.GetAllgarages();

            if(search != null)
            {
                garages = garages.Where(g => g.Name.ToLower().Contains(search.Trim().ToLower()) || g.Location.ToLower().Contains(search.Trim().ToLower()));
            }
            else
            {
                var pageSize1 = 2;
                PaginationViewModel<Garage> pgvm1 = new PaginationViewModel<Garage>();
                pgvm1.TotalCount = garages.Count();
                pgvm1.CurrentPage = page;
                pgvm1.PageSize = 2;
                garages = garages.Skip(pageSize1 * (page - 1)).Take(pageSize1);
                pgvm1.Colection = garages;


                return View("GarageResult", pgvm1);
            }
 
            var pageSize = 2;
            PaginationViewModel<Garage> pgvm = new PaginationViewModel<Garage>();
            pgvm.TotalCount = garages.Count();
            pgvm.CurrentPage = page;
            pgvm.PageSize = 2;
            garages = garages.Skip(pageSize * (page - 1)).Take(pageSize);
            pgvm.Colection = garages;


            return View("GarageResult", pgvm);
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
                var garages = _unitOfWork.GarageService.GetAllgarages();
                var pageSize = 2;
                PaginationViewModel<Garage> pgvm = new PaginationViewModel<Garage>()
                {
                    TotalCount = garages.Count(),
                    PageSize = pageSize,
                    CurrentPage = 1,
                    Colection = garages.Take(pageSize)
                };

                return View("GarageResult", pgvm);
            }

            Garage garage = _unitOfWork.GarageService.GetGarageById(garageId);
            if (garage == null || garage.IsFull)
            {
                ViewBag.ErrorMessage = "Garage is either full or not found!";
                var garages = _unitOfWork.GarageService.GetAllgarages();
                var pageSize = 2;
                PaginationViewModel<Garage> pgvm = new PaginationViewModel<Garage>()
                {
                    TotalCount = garages.Count(),
                    PageSize = pageSize,
                    CurrentPage = 1,
                    Colection = garages.Take(pageSize)
                };

                return View("GarageResult", pgvm);
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

            garage.LastEntryTime = DateTime.Now;
            garage.CurrentOccupancy++;
            _unitOfWork.VehicleInGarageService.Create(vig);

            ViewBag.SuccessMessage = $"Vehicle with license plate number: {licensePlate}, successfully entered the garage {garage.Name}!";
            var garagesAfterEntry = _unitOfWork.GarageService.GetAllgarages();

            var pgvmFinal = new PaginationViewModel<Garage>
            {
                TotalCount = garagesAfterEntry.Count(),
                CurrentPage = 1,
                PageSize = 2,
                Colection = garagesAfterEntry.Take(2).ToList()
            };
            

            return View("GarageResult", pgvmFinal);
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
