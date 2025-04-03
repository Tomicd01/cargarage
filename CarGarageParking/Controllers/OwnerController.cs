using CarGarageParking.Models;
using CarGarageParking.Models.ViewModel;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
        //private readonly CarGarageParkingDbContext _context;
        //public OwnerController(CarGarageParkingDbContext context) 
        //{
        //    _context = context;
        //}
        //private readonly IOwnerService _ownerService;
        //private readonly IVehicleService _vehicleService;
        //public OwnerController(IOwnerService ownerService, IVehicleService vehicleService)
        //{
        //    _ownerService = ownerService;
        //    _vehicleService = vehicleService;
        //}
        private readonly IUnitOfWork _unitOfWork;
        public OwnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string firstName, string lastName, int? numberOfCars)
        {
            var owners = _unitOfWork.OwnerService.GetAllOwnersWithVehicles();

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

            var app = owners.Select(o => new ApplicationRegistrationViewModel()
            {
                Owner = o,
                NumberOfVehicles = o.Vehicles.Count(),
                Vehicles = o.Vehicles.ToList()
            }).ToList();

            PaginationViewModel<Owner> pgvm = new PaginationViewModel<Owner>();

            return View(app);

        }

        public IActionResult Details(int id)
        {
            Owner singleOwner = _unitOfWork.OwnerService.GetOwnerById(id);
            singleOwner.Vehicles = _unitOfWork.VehicleService.GetVehicleByCondition(v => v.OwnerId == id).ToList();

            return View(singleOwner);
        }

    }
}
