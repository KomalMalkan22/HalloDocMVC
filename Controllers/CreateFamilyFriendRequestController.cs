using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace HalloDoc.Controllers
{
    public class CreateFamilyFriendRequestController : Controller
    {
        private readonly HalloDocContext _context;

        public CreateFamilyFriendRequestController(HalloDocContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("../Request/CreateFamilyFriendRequest");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateFamilyFriendRequestModel createFamilyFriendRequest)
        {
            var request = new Request()
            {
                Requesttypeid = 3,
                Status = 1,
                Firstname = createFamilyFriendRequest.FF_FirstName,
                Lastname = createFamilyFriendRequest.FF_LastName,
                Email = createFamilyFriendRequest.FF_Email,
                Phonenumber = createFamilyFriendRequest.FF_PhoneNumber,
                Relationname = createFamilyFriendRequest.FF_RelationWithPatients,
                Createddate = DateTime.Now,
                Isurgentemailsent = new BitArray(1)
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestClient = new Requestclient()
            {
                Request = request,
                Requestid = request.Requestid,
                Notes = createFamilyFriendRequest.Symptoms,
                Firstname = createFamilyFriendRequest.FirstName,
                Lastname = createFamilyFriendRequest.LastName,
                Strmonth = createFamilyFriendRequest.DateOfBirth.ToString("MMMM"),
                Intdate = createFamilyFriendRequest.DateOfBirth.Day,
                Intyear = createFamilyFriendRequest.DateOfBirth.Year,
                Email = createFamilyFriendRequest.Email,
                Phonenumber = createFamilyFriendRequest.PhoneNumber,
                Location = createFamilyFriendRequest.RoomSuite
                //RoomSuite
            };
            _context.Requestclients.Add(requestClient);
            await _context.SaveChangesAsync();

            if (createFamilyFriendRequest.UploadFile != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, createFamilyFriendRequest.UploadFile.FileName);
                createFamilyFriendRequest.UploadImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + createFamilyFriendRequest.UploadFile.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    createFamilyFriendRequest.UploadFile.CopyTo(stream);
                }

                var requestwisefile = new Requestwisefile()
                {
                    Requestid = request.Requestid,
                    Filename = createFamilyFriendRequest.UploadImage,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            return View("../Request/SubmitRequestScreen");
        }
    }
}
