using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult SubmitRequestScreen()
        {
            return View();
        }

        public IActionResult CreateFamilyFriendRequest()
        {
            return View();
        }

        public IActionResult CreateConciergeRequest()
        {
            return View();
        }

        public IActionResult CreateBusinessRequest()
        {
            return View();
        }
    }
}
