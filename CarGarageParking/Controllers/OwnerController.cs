using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class OwnerController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Owner> owners = GetAllOwners();

            return View(owners);

        }

        public IActionResult Details(int id)
        {
            IEnumerable<Owner> owners = GetAllOwners();
            Owner singleOwner = owners.FirstOrDefault(o => o.OwnerId == id);

            return View(singleOwner);
        }

        public IEnumerable<Owner> GetAllOwners()
        {
            List<Owner> owners = new List<Owner>();
            Owner owner1 = new Owner();

            owner1.OwnerId = 0;
            owner1.FirstName = "John";
            owner1.LastName = "Doe";
            owner1.Vehicles = new List<Vehicle>();

            Vehicle opel1 = new Vehicle();
            opel1.LicensePlate = "ABC123";
            owner1.Vehicles.Add(opel1);

            owners.Add(owner1);

            Owner owner2 = new Owner();
            owner1.OwnerId = 1;
            owner2.FirstName = "Jane";
            owner2.LastName = "Doe";
            owner2.Vehicles = new List<Vehicle>();

            Vehicle opel2 = new Vehicle();
            opel2.LicensePlate = "DEF456";
            owner2.Vehicles.Add(opel2);
            owners.Add(owner2);

            Vehicle opel3 = new Vehicle();
            opel3.LicensePlate = "Mak223";
            owner1.Vehicles.Add(opel3);

            return owners;
        }
    }
}
