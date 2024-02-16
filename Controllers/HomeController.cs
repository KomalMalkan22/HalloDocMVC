using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HalloDoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion Index

        #region PatientLoginPage
        public IActionResult PatientLoginPage()
        {
            return View();
        }
        #endregion PatientLoginPage

        #region ResetPassword
        public IActionResult ResetPassword()
        {
            return View();
        }
        #endregion ResetPassword

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}