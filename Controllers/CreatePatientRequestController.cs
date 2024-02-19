using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HalloDoc.Controllers
{
    public class CreatePatientRequestController : Controller
    {
        private readonly HalloDocContext _context;

        public CreatePatientRequestController(HalloDocContext context)
        {
            _context = context;
        }

        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientRequestModel createPatientRequest)
        {
            var isUserExist = _context.Users.FirstOrDefault(x => x.Email == createPatientRequest.Email);

            if (isUserExist == null)
            {
                Guid g = Guid.NewGuid();
                var aspnetuser = new Aspnetuser()
                {

                    Id = g.ToString(),
                    Username = createPatientRequest.FirstName,
                    Passwordhash = createPatientRequest.PassWord,
                    Email = createPatientRequest.Email,
                    CreatedDate = DateTime.Now
                };
                _context.Aspnetusers.Add(aspnetuser);
                await _context.SaveChangesAsync();

                var user = new User()
                {
                    Aspnetuserid = aspnetuser.Id,
                    Firstname = createPatientRequest.FirstName,
                    Lastname = createPatientRequest.LastName,
                    Email = createPatientRequest.Email,
                    Createdby = aspnetuser.Id,
                    Createddate = DateTime.Now
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            var User = new User();

            var request = new Request()
            {
                Requesttypeid = 2,
                Status = 1,
                Userid = isUserExist == null ? User.Userid : isUserExist.Userid,
                Firstname = createPatientRequest.FirstName,
                Lastname = createPatientRequest.LastName,
                Email = createPatientRequest.Email,
                Phonenumber = createPatientRequest.PhoneNumber,
                Isurgentemailsent = new BitArray(1),
                Createddate = DateTime.Now
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestclient = new Requestclient()
            {
                Requestid = request.Requestid,
                Firstname = createPatientRequest.FirstName,
                Lastname = createPatientRequest.LastName,
                Street = createPatientRequest.Street,
                Email = createPatientRequest.Email,
                Phonenumber = createPatientRequest.PhoneNumber,
                Notes = createPatientRequest.Symptoms,
                Strmonth = createPatientRequest.DateOfBirth.ToString("MMMM"),
                Intdate = createPatientRequest.DateOfBirth.Day,
                Intyear = createPatientRequest.DateOfBirth.Year
            };
            _context.Requestclients.Add(requestclient);
            await _context.SaveChangesAsync();

            if (createPatientRequest.UploadFile != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileNameWithPath = Path.Combine(path, createPatientRequest.UploadFile.FileName);
                createPatientRequest.UploadImage = createPatientRequest.UploadFile.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    createPatientRequest.UploadFile.CopyTo(stream);
                }

                var requestwisefile = new Requestwisefile()
                {
                    Request = request,
                    Requestid = request.Requestid,
                    Filename = createPatientRequest.UploadImage,
                    Createddate = DateTime.Now
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }

            return View("../Request/SubmitRequestScreen");

        }
        #endregion Create

        #region CheckEmail
        [HttpPost]
        public async Task<IActionResult> CheckEmailAsync(string email)
        {
            string message;
            var aspnetuser = await _context.Aspnetusers.FirstOrDefaultAsync(m => m.Email == email);
            if (aspnetuser == null)
            {
                message = "False";
            }
            else
            {
                message = "Success";
            }
            return Json(new
            {
                isAspnetuser = aspnetuser == null
            });
        }
        #endregion CheckEmail

        #region Index
        public IActionResult Index()
        {
            return View("../Request/CreatePatientRequest");
        }
        #endregion Index
    }
}
