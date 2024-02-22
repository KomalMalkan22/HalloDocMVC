using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HalloDoc.Controllers
{
    public class AdminController : Controller
    {
        private readonly HalloDocContext _context;

        public AdminController(HalloDocContext context)
        {
            _context = context;
        }

        public enum RequestType
        {
            Business = 1,
            Patient,
            Family,
            Concierge
        }

        public IActionResult Index()
        {
            var countRequest = new CountStatusWiseRequestModel
            {
                NewRequest = _context.Requests.Where(r => r.Status == 1).Count(),
                PendingRequest = _context.Requests.Where(r => r.Status == 2).Count(),
                ActiveRequest = _context.Requests.Where(r => ( r.Status == 4 || r.Status == 5 )).Count(),
                ConcludeRequest = _context.Requests.Where(r => r.Status == 6).Count(),
                ToCloseRequest = _context.Requests.Where(r => (r.Status == 3 || r.Status == 7 || r.Status == 8)).Count(),
                UnpaidRequest = _context.Requests.Where(r => r.Status == 9).Count(),
                adminDashboardList = NewRequestData()
            };
            return View(countRequest);
        }
        public List<AdminDashboardList> NewRequestData()
        {
            var list =  _context.Requests.Join
                        (_context.Requestclients,
                        requestclients => requestclients.Requestid,requests => requests.Requestid,
                        (requests, requestclients) => new { Request = requests, Requestclient = requestclients }
                        )
                        .Where(req => req.Request.Status == 1)
                        .Select(req => new AdminDashboardList()
                        {
                            RequestId = req.Request.Requestid,
                            PatientName = req.Requestclient.Firstname + " " + req.Requestclient.Lastname,
                            Email = req.Requestclient.Email,
                            //DateOfBirth = new DateTime((int)req.Requestclient.Intyear, Convert.ToInt32(req.Requestclient.Strmonth.Trim()), (int)req.Requestclient.Intdate),
                            RequestTypeId = req.Request.Requesttypeid, 
                            Requestor = req.Request.Firstname + " " + req.Request.Lastname,
                            RequestedDate = req.Request.Createddate,
                            PatientPhoneNumber = req.Requestclient.Phonenumber,
                            RequestorPhoneNumber = req.Request.Phonenumber,
                            Notes = req.Requestclient.Notes,
                            Address = req.Requestclient.Address + " " + req.Requestclient.Street + " " + req.Requestclient.City + " " + req.Requestclient.State + " " + req.Requestclient.Zipcode
                        })
                        .OrderByDescending(req => req.RequestedDate)
                        .ToList();
            return list;
        }
    }
}
