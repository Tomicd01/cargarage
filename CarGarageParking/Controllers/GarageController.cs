using CarGarageParking.Models;
using CarGarageParking.Models.Utility;
using CarGarageParking.Models.ViewModel;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Index(string name, string location, int? AvailableSpots, int page = 1)
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

            int pageSize = 2;
            PaginationViewModel<Garage> pgvm = new PaginationViewModel<Garage>();
            pgvm.PageSize = pageSize;
            pgvm.TotalCount = garages.Count();
            pgvm.CurrentPage = page;
            
            garages = garages.Skip(pageSize * (page - 1)).Take(pageSize);
            pgvm.Colection = garages;

            return View(pgvm);
        }

        public  IActionResult Info(int id)
        {
           Garage garage = _unitOfWork.GarageService.GetGarageById(id);
           return View(garage);
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
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

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Edit(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);
            if(garage == null)
            {
                return NotFound();
            }
            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Edit(Garage garage)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.GarageService.UpdateGarage(garage);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(garage);    
        }

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Delete(int id)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(id);
            
            if(garage == null)
            {
                return NotFound();
            }
            return View(garage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult DeleteConfirmed(int GarageId)
        {
            Garage garage = _unitOfWork.GarageService.GetGarageById(GarageId);

            if(garage == null)
            {
                return NotFound();
            }

            _unitOfWork.GarageService.DeleteGarage(GarageId);
            _unitOfWork.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}

























