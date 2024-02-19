using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class RequestController : Controller
    {
        #region SubmitRequestScreen
        public IActionResult SubmitRequestScreen()
        {
            return View();
        }
        #endregion SubmitRequestScreen
    }
}
