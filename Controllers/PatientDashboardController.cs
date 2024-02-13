using HalloDoc.Models;
using HalloDoc.DataContext;
using HalloDoc.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    public class PatientDashboardController : Controller
    {
        private readonly HalloDocContext _context;

        public PatientDashboardController(HalloDocContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (CV.UserID() != null)
            {
                var UserIDForRequest = _context.Users.Where(r => r.Aspnetuserid == CV.UserID()).FirstOrDefault();

                if (UserIDForRequest != null)
                {
                    List<DataModels.Request> Request = _context.Requests.Where(r => r.Userid == UserIDForRequest.Userid).ToList();
                    List<int> ids = new List<int>();

                    foreach (var request in Request)
                    {

                        var doc = _context.Requestwisefiles.Where(r => r.Requestid == request.Requestid).FirstOrDefault();
                        if (doc != null)
                        {
                            ids.Add(request.Requestid);
                        }
                    }
                    ViewBag.docidlist = ids;
                    ViewBag.listofrequest = Request;

                }
                return View();
            }
            else
            {
                return View("../Home/Index");
            }

        }
    }
}
