using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class PatientLogin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
