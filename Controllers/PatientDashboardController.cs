using HalloDoc.Models;
using HalloDoc.DataContext;
using HalloDoc.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    [CheckAccess]
    public class PatientDashboardController : Controller
    {
        private readonly HalloDocContext _context;

        public PatientDashboardController(HalloDocContext context)
        {
            _context = context;
        }

        public enum Status
        {
            Unassigned = 1,
            Accepted,
            Cancelled,
            Reserving,
            MDEnRoute,
            MDOnSite,
            FollowUp,
            Closed,
            Locked,
            Declined,
            Consult,
            Clear,
            CancelledByProvider,
            CCUploadedByClient,
            CCApprovedByAdmin
        }

        #region Index
        [Route("PatientDashboard/Index")]
        public async Task<IActionResult> Index()
        {
            if (CV.UserID() != null)
            {
                var UserIDForRequest = _context.Users.Where(r => r.Aspnetuserid == CV.UserID()).FirstOrDefault();

                if (UserIDForRequest != null)
                {
                    List<DataModels.Request> Request = _context.Requests.Where(r => r.Userid == UserIDForRequest.Userid).ToList();
                    List<int> ids = new();

                    foreach (var request in Request)
                    {

                        var doc = _context.Requestwisefiles.Where(r => r.Requestid == request.Requestid).FirstOrDefault();
                        if (doc != null)
                        {
                            ids.Add(request.Requestid);
                        }
                    }
                    ViewBag.docidlist = ids;
                    ViewBag.list = Request;

                    // Get the list of documents for each request
                    var docList = _context.Requestwisefiles.ToList();

                    // Group the documents by request id and count them
                    var docCount = docList.GroupBy(d => d.Requestid)
                                          .ToDictionary(g => g.Key, g => g.Count());

                    // Store the dictionary in the ViewBag
                    ViewBag.docCount = docCount;

                }
                return View();
            }
            else
            {
                return View("../Home/Index");
            }

        }
        #endregion Index

        #region ViewDocuments
        public IActionResult ViewDocuments(int? id)
        {

            List<Request> Request = _context.Requests.Where(r => r.Requestid == id).ToList();
            ViewBag.requestinfo = Request;
            List<Requestwisefile> DocList = _context.Requestwisefiles.Where(r => r.Requestid == id).ToList();
            ViewBag.DocList = DocList;
            return View("ViewDocuments");
        }
        #endregion ViewDocuments

        #region UploadDocuments
        public IActionResult UploadDoc(int Requestid, IFormFile? UploadFile)
        {
            string UploadImage;
            if (UploadFile != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                string fileNameWithPath = Path.Combine(path, UploadFile.FileName);
                UploadImage = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + UploadFile.FileName;
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    UploadFile.CopyTo(stream)
;
                }
                var requestwisefile = new Requestwisefile
                {
                    Requestid = Requestid,
                    Filename = UploadFile.FileName,
                    Createddate = DateTime.Now,
                };
                _context.Requestwisefiles.Add(requestwisefile);
                _context.SaveChanges();
            }

            return RedirectToAction("ViewDocuments", new { id = Requestid });
        }
        #endregion UploadDocuments

        #region ReviewAgreement

        public IActionResult ReviewAgreement()
        {
            return View("../PatientDashboard/ReviewAgreement");
        }
        #endregion ReviewAgreement
    }
}
