using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class PatientDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
