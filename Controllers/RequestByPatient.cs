using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HalloDoc.Controllers
{
    [CheckAccess]
    public class RequestByPatient : Controller
    {
        private readonly HalloDocContext _context;

        public RequestByPatient(HalloDocContext context)
        {
            _context = context;
        }
        public IActionResult RequestForMe()
        {
            var patientRequest = _context.Users
                               .Where(r => Convert.ToString(r.Aspnetuserid) == (CV.UserID()))
                               .Select(r => new CreatePatientRequestModel
                               {
                                   FirstName = r.Firstname,
                                   LastName = r.Lastname,
                                   Email = r.Email,
                                   PhoneNumber = r.Mobile,
                                   DateOfBirth = new DateTime((int)r.Intyear, Convert.ToInt32(r.Strmonth.Trim()), (int)r.Intdate)
                               })
                               .FirstOrDefault();
            return View(patientRequest);
        }
        public IActionResult RequestForSomeoneElse()
        {
            return View();
        }

        #region PostSomeoneElseAsync
        public async Task<IActionResult> PostMe(CreatePatientRequestModel patientRequest)
        {
            var isExist = _context.Users.FirstOrDefault(x => x.Email == patientRequest.Email);
            var request = new Request()
            {
                Requesttypeid = 2,
                Userid = isExist.Userid,
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Email = patientRequest.Email,
                Phonenumber = patientRequest.PhoneNumber,
                Isurgentemailsent = new BitArray(1),
                Createddate = DateTime.Now
            };
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var requestClient = new Requestclient()
            {
                Requestid = request.Requestid,
                Firstname = patientRequest.FirstName,
                Lastname= patientRequest.LastName,
                Address = patientRequest.Street,
                Email = patientRequest.Email,
                Phonenumber = patientRequest.PhoneNumber
            };
            _context.Requestclients.Add(requestClient);
            await _context.SaveChangesAsync();


            if (patientRequest.UploadFile != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, patientRequest.UploadFile.FileName);
                patientRequest.UploadImage = patientRequest.UploadFile.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    patientRequest.UploadFile.CopyTo(stream);
                }

                var requestwisefile = new Requestwisefile
                {
                    Requestid = request.Requestid,
                    Filename = patientRequest.UploadImage,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "PatientDashboard");
        }
        #endregion


        #region PostSomeoneElseAsync
        public async Task<IActionResult> PostSomeoneElse(CreatePatientRequestModel patientRequest)
        {
            var request = new Request()
            {
                Requesttypeid = 2,
                //Userid = Convert.ToInt32(CV.UserID()),
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Email = patientRequest.Email,
                Phonenumber = patientRequest.PhoneNumber,
                Relationname = patientRequest.FF_RelationWithPatient,
                Isurgentemailsent = new BitArray(1),
                Createddate = DateTime.Now
            };            
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            var Requestclient = new Requestclient()
            {
                Requestid = request.Requestid,
                Firstname = patientRequest.FirstName,
                Lastname = patientRequest.LastName,
                Address = patientRequest.Street,
                Email = patientRequest.Email,
                Phonenumber = patientRequest.PhoneNumber
            };
            _context.Requestclients.Add(Requestclient);
            await _context.SaveChangesAsync();


            if (patientRequest.UploadFile != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileNameWithPath = Path.Combine(path, patientRequest.UploadFile.FileName);
                patientRequest.UploadImage = patientRequest.UploadFile.FileName;

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    patientRequest.UploadFile.CopyTo(stream);
                }

                var requestwisefile = new Requestwisefile
                {
                    Requestid = request.Requestid,
                    Filename = patientRequest.UploadImage,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "PatientDashboard");
        }
        #endregion 
    }
}
