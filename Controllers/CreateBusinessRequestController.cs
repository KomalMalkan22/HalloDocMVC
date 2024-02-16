using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace HalloDoc.Controllers
{
    [CheckAccess]
    public class CreateBusinessRequestController : Controller
    {
        private readonly HalloDocContext _context;
        
        public CreateBusinessRequestController(HalloDocContext context)
        {
            _context = context;
        }        

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateBusinessRequestModel createBusinessRequest)
        {
            var business = new Business
            {
                Name = createBusinessRequest.BUS_FirstName + " " + createBusinessRequest.BUS_LastName,
                Phonenumber = createBusinessRequest.BUS_PhoneNumber,
                Createddate = DateTime.Now
            };
            _context.Businesses.Add(business);
            await _context.SaveChangesAsync();

            var request = new Request
            {
                Requesttypeid = 1,
                Status = 1,
                Firstname = createBusinessRequest.BUS_FirstName,
                Lastname = createBusinessRequest.BUS_LastName,
                Phonenumber = createBusinessRequest.BUS_PhoneNumber,
                Email = createBusinessRequest.BUS_Email,
                Createddate = DateTime.Now,
                Isurgentemailsent = new BitArray(1)
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestClient = new Requestclient
            {
                Request = request,
                Requestid = request.Requestid,
                Notes = createBusinessRequest.Symptoms,
                Firstname = createBusinessRequest.FirstName,
                Lastname = createBusinessRequest.LastName,
                Strmonth = createBusinessRequest.DateOfBirth.ToString("MMMM"),
                Intdate = createBusinessRequest.DateOfBirth.Day,
                Intyear = createBusinessRequest.DateOfBirth.Year,
                Email = createBusinessRequest.Email,
                Phonenumber = createBusinessRequest.PhoneNumber,
                Street = createBusinessRequest.Street,
                City = createBusinessRequest.City,
                State = createBusinessRequest.State,
                Zipcode = createBusinessRequest.ZipCode,
                Location = createBusinessRequest.RoomSuite
            };
            _context.Requestclients.Add(requestClient);
            await _context.SaveChangesAsync();

            var requestBusiness = new Requestbusiness
            {
                Requestid = request.Requestid,
                Businessid = business.Businessid
            };
            _context.Requestbusinesses.Add(requestBusiness);
            await _context.SaveChangesAsync();

            return View("../Request/SubmitRequestScreen");
        }
        #endregion Create

        #region Index
        public IActionResult Index()
        {
            return View("../Request/CreateBusinessRequest");
        }
        #endregion Index
    }
}
