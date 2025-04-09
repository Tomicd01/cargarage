using CarGarageParking.Models;
using CarGarageParking.Models.ViewModel;
using CarGarageParking.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

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
            evm.Garage = garage;

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
                Vehicle = new Vehicle()
                {
                    LicensePlate = licensePlate,
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
        public IActionResult RegisterUser()
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner = new Owner();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(ApplicationRegistrationViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("VehicleCount", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName
            });
        }

        [HttpGet]
        public IActionResult VehicleCount(string firstName, string lastName)
        {
            var model = new ApplicationRegistrationViewModel();
            model.Owner = new Owner
            {
                FirstName = firstName,
                LastName = lastName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VehicleCount(ApplicationRegistrationViewModel model)
        {
            if (model.NumberOfVehicles < 1 || model.NumberOfVehicles > 10)
            {
                ModelState.AddModelError("", "You can register between 1 to 10.");
                return View(model);
            }

            for(int i = 0; i < model.NumberOfVehicles; i++)
            {
                model.Vehicles.Add(new Vehicle());
            }


            return RedirectToAction("LicenseInput", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfVehicles = model.NumberOfVehicles
            });

        }

        [HttpGet]
        public IActionResult LicenseInput(string firstName, string lastName, int numberOfVehicles)
        {
            try
            {
                var model = new ApplicationRegistrationViewModel()
                {
                    Owner = new Owner
                    {
                        FirstName = firstName,
                        LastName = lastName
                    },
                    NumberOfVehicles = numberOfVehicles,
                    Vehicles = new List<Vehicle>()
                };

                for(int i = 0; i < numberOfVehicles; i++)
                {
                    model.Vehicles.Add(new Vehicle());
                }
                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LicenseInput(ApplicationRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("LicenseInput", model);
            }

            return RedirectToAction("ConfirmApplication", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfvehicles = model.NumberOfVehicles,
                licensePlates = string.Join(",", model.Vehicles.Select(v => v.LicensePlate))
            });
        }

        [HttpGet]
        public IActionResult ConfirmApplication(string firstName, string lastName, int numberOfVehicles, string licensePlates)
        {
            var model = new ApplicationRegistrationViewModel()
            {
                Owner = new Owner
                {
                    FirstName = firstName,
                    LastName = lastName
                },
                NumberOfVehicles = numberOfVehicles,
                Vehicles = licensePlates.Split(",").Select(lp => new Vehicle { LicensePlate = lp }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmApplication(ApplicationRegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ConfirmApplication", model);
            }

            Application application = new Application()
            {
                Owner = model.Owner,
                Vehicles = model.Vehicles,
                Credit = 100,
                HasActiveMembership = true
            };

     
            //for(int i = 0; i < application.Vehicles.Count; i++)
            //{
            //    var item = application.Vehicles.ToList()[i];
            //    item.VehicleInGarages.Add(new VehicleInGarage
            //    {
            //        Owner = application.Owner,
            //    });
            //}

            _unitOfWork.ApplicationService.CreateApplication(application);

            ViewBag.Message = "Application successfully submitted!";
            return RedirectToAction("Success", new
            {
                firstName = model.Owner.FirstName,
                lastName = model.Owner.LastName,
                numberOfVehicles = model.NumberOfVehicles,
                licensePlates = string.Join(",", model.Vehicles.Select(v => v.LicensePlate))
            });
        }

        [HttpGet]
        public IActionResult Success(string firstName, string lastName, int numberOfVehicles, string licensePlates)
        {
            var model = new ApplicationRegistrationViewModel()
            {
                Owner = new Owner
                {
                    FirstName = firstName,
                    LastName = lastName
                },
                NumberOfVehicles = numberOfVehicles,
                Vehicles = licensePlates.Split(",").Select(lp => new Vehicle { LicensePlate = lp }).ToList()
            };

            ViewBag.SuccessMessage = "Application successfully submitted!";
            return View(model);
        }

        [HttpGet]
        public IActionResult FindVehicleForExit()
        {
            LeaveGarageViewModel model = new LeaveGarageViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult FindVehicleForExit(LeaveGarageViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.VehicleInGarage.Vehicle.LicensePlate))
            {
                ModelState.AddModelError("", "License plate is required!");
                return View(model);
            }
            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(model.VehicleInGarage.Vehicle.LicensePlate);

            if(vig == null)
            {
                ViewBag.ErrorMessage = $"There is no Vehicle with license plate: {model.VehicleInGarage.Vehicle.LicensePlate} in garages!";
                return View(model);
            }

            return RedirectToAction("LeaveGarage", new { licensePlate = vig.Vehicle.LicensePlate, garageId = vig.Garage.GarageId });
        }

        [HttpGet]
        public IActionResult LeaveGarage(string licensePlate, int garageId)
        {
            licensePlate = licensePlate.Trim().ToLower();

            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(licensePlate);
            if(vig == null || vig.Garage.GarageId != garageId)
            {
                return NotFound("Vehicle is not found in specific garage");
            }
            
            Application application = vig.Vehicle.Application;

            var hourlyrate = vig.HourlyRate;
            var timeSpend = DateTime.Now - vig.EntryTime;
            var hoursSpend = Math.Ceiling(timeSpend.TotalHours);
            decimal amountToPay = (decimal)hoursSpend * hourlyrate;
            var discountAmount = 0m;

            if(application != null)
            {
                var discount = application.HasActiveMembership ? 0.1m : 0.00m;
                discountAmount = amountToPay * (amountToPay * discount);
            }

            LeaveGarageViewModel lgvm = new LeaveGarageViewModel(); 
            lgvm.Vehicle = vig.Vehicle;
            lgvm.Owner = application?.Owner;
            lgvm.Application = application;
            lgvm.Payment = new Payment();

            if(lgvm.Application != null)
            {
                lgvm.Payment.TotalCharge = discountAmount;
            }
            else
            {
                lgvm.Payment.TotalCharge = amountToPay;
            }

            lgvm.VehicleInGarage = vig;

            return View(lgvm);
        }

        [HttpGet]
        public IActionResult PaymentInput(string licensePlate, string? errorMessage)
        {
            licensePlate = licensePlate.Trim().ToLower();

            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(licensePlate);

            Application application = vig.Vehicle.Application;

            var hourlyrate = vig.HourlyRate;
            var timeSpend = DateTime.Now - vig.EntryTime;
            var hoursSpend = Math.Ceiling(timeSpend.TotalHours);
            decimal amountToPay = (decimal)hoursSpend * hourlyrate;
            var discountAmount = 0m;

            if (application != null)
            {
                var discount = application.HasActiveMembership ? 0.1m : 0.00m;
                discountAmount = amountToPay * (amountToPay * discount);
            }

            if (vig == null)
            {
                return NotFound("Not found");
            }

            LeaveGarageViewModel lgvm = new LeaveGarageViewModel();
            lgvm.Vehicle = vig.Vehicle;
            lgvm.Owner = application?.Owner;
            lgvm.Application = application;
            lgvm.Payment = new Payment();
            lgvm.Payment.PaymentTime = DateTime.Now;

            if (lgvm.Application != null)
            {
                lgvm.Payment.TotalCharge = discountAmount;
            }
            else
            {
                lgvm.Payment.TotalCharge = amountToPay;
            }
            lgvm.VehicleInGarage = vig;

            ViewBag.ErrorMessage = errorMessage;

            return View(lgvm);
        }

        [HttpPost]
        public IActionResult PaymentInput(string licensePlate, bool amountIsPaid, decimal paidAmount)
        {
            if (!amountIsPaid)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "Payment was cancelled." });
            }
            licensePlate = licensePlate?.Trim().ToLower();

            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(licensePlate);
            if (vig == null || vig.Vehicle == null)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "The vehicle has not ben found in Garage." });
            }

            Application application = vig.Vehicle.Application;

            var hourlyrate = vig.HourlyRate;
            var timeSpend = DateTime.Now - vig.EntryTime;
            var hoursSpend = Math.Ceiling(timeSpend.TotalHours);
            decimal baseAmountToPay = (decimal)hoursSpend * hourlyrate;


            var discountAmount = application?.HasActiveMembership == true ? 0.1m : 0.00m;
            var amountToPay = baseAmountToPay - (baseAmountToPay * discountAmount);

            if(paidAmount < amountToPay)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "The amount entered is less than required." });
            }

            LeaveGarageViewModel lgvm = new LeaveGarageViewModel();
            lgvm.Application = application;
            if(lgvm.Application != null)
            {
                lgvm.Application.Owner = application?.Owner;
            }
            lgvm.Payment = new Payment()
            {
                TotalCharge = paidAmount,
                VehicleInGarage = vig,
                IsPaid = true,
                PaymentTime = DateTime.Now,
                ExpirationTime = DateTime.Now.AddMinutes(Payment.TimeToTakeCar),
                
            };

            _unitOfWork.PaymentService.Create(lgvm.Payment);

            return RedirectToAction("PaymentConfirmation", new { licensePlate, successMessage = "Payment was successful!" });
        }

        [HttpGet]
        public IActionResult PaymentConfirmation(string licensePlate, string? successMessage)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                return BadRequest("License plate is required.");
            }

            licensePlate = licensePlate?.Trim().ToLower();

            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(licensePlate);
            if (vig == null || vig.Vehicle == null)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "The vehicle has not ben found in Garage." });
            }

            var payment = _unitOfWork.PaymentService.GetLastPaymentByVehicleInGarageId(vig.VehicleInGarageId);
            if(payment == null)
            {
                return NotFound("Payment not found.");
            }

            LeaveGarageViewModel lgvm = new LeaveGarageViewModel();
            lgvm.Vehicle = vig.Vehicle;
            lgvm.VehicleInGarage = vig;
            lgvm.Payment = payment;
            lgvm.Application = vig.Vehicle.Application;
            lgvm.Owner = lgvm.Application?.Owner;   

            if(string.IsNullOrEmpty(successMessage))
            {
                successMessage = "Payment is confirmed!";
            }
            ViewBag.SuccessMessage = successMessage;

            return View(lgvm);
        }

        [HttpPost]
        public IActionResult PaymentConfirmation(string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                return BadRequest("License plate is required.");
            }
            licensePlate = licensePlate?.Trim().ToLower();

            VehicleInGarage vig = _unitOfWork.VehicleInGarageService.FindActiveVehicleInGarage(licensePlate);
            if (vig == null || vig.Vehicle == null)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "The vehicle has not been found in Garage." });
            }
            var payment = _unitOfWork.PaymentService.GetLastPaymentByVehicleInGarageId(vig.VehicleInGarageId);
            if (payment == null || !payment.IsPaid)
            {
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "Payment not found." });
            }

            if(DateTime.Now > payment.ExpirationTime)
            {
                vig.EntryTime = payment.ExpirationTime;
                vig.IsVehicleStillInGarage = true;

                _unitOfWork.VehicleInGarageService.Update(vig);
                return RedirectToAction("PaymentInput", new { licensePlate, errorMessage = "You have exceeded time to leave a garage." });
            }

            vig.IsVehicleStillInGarage = false;
            vig.ExitTime = DateTime.Now;
            vig.Garage.CurrentOccupancy--;
            _unitOfWork.VehicleInGarageService.Update(vig);

            return RedirectToAction("ConfirmationExitVehicle", "Home", new { licensePlate = vig.Vehicle.LicensePlate, entryTime = vig.EntryTime, exitTime = vig.ExitTime, garageName = vig.Garage.Name, currentOccupancy = vig.Garage.CurrentOccupancy });
        }

        [HttpGet]
        public IActionResult ConfirmationExitVehicle(string licensePlate, DateTime entryTime, DateTime exitTime, int currentOccupancy, string garageName)
        {
            ViewBag.LicensePlate = licensePlate;    
            ViewBag.EntryTime = entryTime;
            ViewBag.ExitTime = exitTime;
            ViewBag.CurrentOccupancy = currentOccupancy;
            ViewBag.GarageName = garageName;
            ViewBag.Message = "Vehicle successfully exited the garage!";

            return View();
        }
    }
}
