using HalloDoc.DataContext;
using HalloDoc.DataModels;
using HalloDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.Controllers
{
    [CheckAccess]
    public class Profile : Controller
    {
        private readonly HalloDocContext _context;

        public Profile(HalloDocContext context)
        {
            _context = context;
        }
        #region Index
        public IActionResult Index()
        {
            var userProfile = _context.Users
                                .Where(r => Convert.ToString(r.Aspnetuserid) == (CV.UserID()))
                                .Select(r => new UserProfile
                                {
                                    Userid = r.Userid,
                                    FirstName = r.Firstname,
                                    LastName = r.Lastname,
                                    PhoneNumber = r.Mobile,
                                    Email = r.Email,
                                    Street = r.Street,
                                    State = r.State,
                                    City = r.City,
                                    ZipCode = r.Zipcode,
                                    DateOfBirth = new DateTime((int)r.Intyear, Convert.ToInt32(r.Strmonth.Trim()), (int)r.Intdate),
                                })
                                .FirstOrDefault();

            return View(userProfile);
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(UserProfile userprofile)
        {
            User userToUpdate = await _context.Users.FindAsync(userprofile.Userid);

            userToUpdate.Firstname = userprofile.FirstName;
            userToUpdate.Lastname = userprofile.LastName;
            userToUpdate.Mobile = userprofile.PhoneNumber;
            userToUpdate.Email = userprofile.Email;
            userToUpdate.State = userprofile.State;
            userToUpdate.Street = userprofile.Street;
            userToUpdate.City = userprofile.City;
            userToUpdate.Zipcode = userprofile.ZipCode;
            userToUpdate.Intdate = userprofile.DateOfBirth.Day;
            userToUpdate.Intyear = userprofile.DateOfBirth.Year;
            userToUpdate.Strmonth = userprofile.DateOfBirth.Month.ToString();
            userToUpdate.Modifiedby = userprofile.Createdby;
            userToUpdate.Modifieddate = DateTime.Now;
            _context.Update(userToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion 
        private bool UserExists(object id)
        {
            throw new NotImplementedException();
        }
    }
}
