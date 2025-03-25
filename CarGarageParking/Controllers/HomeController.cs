using Microsoft.AspNetCore.Mvc;

namespace CarGarageParking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
