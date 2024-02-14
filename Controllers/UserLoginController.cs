using HalloDoc.DataContext;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace HalloDoc.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly HalloDocContext _context;

        public UserLoginController(HalloDocContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View("../Home/PatientLoginPage");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Passwordhash)
        {
            NpgsqlConnection connection = new("Server=localhost;Database=HalloDoc;User Id=postgres;Password=komal123;Include Error Detail=True");
            string query = "SELECT * FROM aspnetusers WHERE email = @Email AND passwordhash = @Passwordhash";
            connection.Open();
            NpgsqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Passwordhash", Passwordhash);
            NpgsqlDataReader reader = command.ExecuteReader();
            DataTable dt = new();
            dt.Load(reader);
            int rows = dt.Rows.Count;
            if (rows > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    HttpContext.Session.SetString("UserName", row["username"].ToString());
                    HttpContext.Session.SetString("UserID", row["Id"].ToString());
                }
                return RedirectToAction("Index", "PatientDashboard");
            }
            else
            {
                ViewData["error"] = "Invalid ID or Password";
                return View("../Home/PatientLoginPage");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
