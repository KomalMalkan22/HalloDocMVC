using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class RequestForSomeoneElseController : Controller
    {
        public IActionResult Index()
        {
            return View("../RequestByPatient/RequestForSomeoneElse");
        }
    }
}
