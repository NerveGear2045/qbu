using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using qbu.Models;
using System.Data;
using System.Diagnostics;

namespace qbu.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;
        public int id=0;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (chkLogin() == true)
            {
                return View();
            }
            else return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                try
                {
                    conn.Open();
                    string sql=$"SELECT ID FROM Account where Email='{email}' and Password='{password}'";
                    SqlDataAdapter ad = new SqlDataAdapter(sql,conn);
                    DataTable tb = new DataTable();
                    ad.Fill(tb);  
                    if (tb.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("login", "1");
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex) { TempData["err"] = ex.Message; }
                finally { conn.Close(); }
            }
            return View();
        }

        private bool chkLogin()
        {
            bool result = false;
            if (HttpContext.Session.GetString("login") != null)
            {
                if (HttpContext.Session.GetString("login") == "1")
                {
                    result = true;
                }
            }
            return result;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}