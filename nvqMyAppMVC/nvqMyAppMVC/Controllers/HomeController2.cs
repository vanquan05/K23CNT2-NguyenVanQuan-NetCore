using Microsoft.AspNetCore.Mvc;

namespace nvqMyAppMVC.Controllers
{
    public class HomeController2 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
