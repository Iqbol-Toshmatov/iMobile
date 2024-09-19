using iMobile.DAL.Interfaces;
using iMobile.Models;
using iMobile.Object.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace iMobile.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class HomeController : Controller
    {
        private readonly IPhoneRepository _phoneRepository;


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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

