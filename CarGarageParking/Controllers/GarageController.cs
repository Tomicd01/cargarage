using CarGarageParking.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class GarageController : Controller
    {
        public IActionResult Index(string name, string location, int? AvailableSpots)
        {
            IEnumerable<Garage> garages = ShowAllGarages();
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
           Garage garage = ShowAllGarages().FirstOrDefault(g => g.GarageId == id);
           return View(garage);
        }

        private IEnumerable<Garage> ShowAllGarages()
        {
            List<Garage> ListOfGarages = new List<Garage>();
            Garage g1 = new Garage();
            g1.GarageId = 1;
            g1.Name = "Resaska";
            g1.Location = "Resavska 23";
            g1.Capacity = 600;
            g1.CurrentOccupancy = 80;
            ListOfGarages.Add(g1);

            Garage g2 = new Garage();
            g2.GarageId = 2;
            g2.Name = "Vukov spomenik";
            g2.Location = "Bulevar kralja Aleksandra 78";
            g2.Capacity = 400;
            g2.CurrentOccupancy = 250;
            ListOfGarages.Add(g2);

            return ListOfGarages;
        } 
    }
}

























