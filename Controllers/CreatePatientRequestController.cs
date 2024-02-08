using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HalloDoc.Controllers
{
    public class CreatePatientRequestController : Controller
    {
        private readonly HalloDocContext _context;

        public CreatePatientRequestController(HalloDocContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ViewDataPatientRequest viewDataPatientRequest)
        {
            var A = new AspNetUser();
            A.Id = "1234";
            A.Email = viewDataPatientRequest.Email;
            A.UserName = "Komal";
            _context.AspNetUsers.Add(A);
            await _context.SaveChangesAsync();


            return View("../Request/SubmitRequestScreen");
        }
        public IActionResult Index()
        {
            return View("../Request/CreatePatientRequest");
        }
    }
}
