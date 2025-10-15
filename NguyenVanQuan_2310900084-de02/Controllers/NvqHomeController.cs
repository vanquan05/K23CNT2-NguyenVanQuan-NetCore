using Microsoft.AspNetCore.Mvc;
using NguyenVanQuan_2310900084.Models;
using System.Diagnostics;

namespace NguyenVanQuan_2310900084.Controllers
{
    public class NvqHomeController : Controller
    {
        private readonly ILogger<NvqHomeController> _logger;

        public NvqHomeController(ILogger<NvqHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult NvqIndex()
        {
            return View();
        }

        public IActionResult NvqAbout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
