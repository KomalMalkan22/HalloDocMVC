using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class RequestForMeController : Controller
    {
        public IActionResult Index()
        {
            return View("../RequestByPatient/RequestForMe");
        }
    }
}
