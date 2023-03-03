using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using qbu.Models;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace qbu.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IConfiguration _configuration;
        public int role = 0;
        public string name = "user_name";

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection Connect()
        {
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return conn;
        }
           
        public IActionResult Index()
        {
            if (chkLogin() == true)
            {
                if (HttpContext.Session.GetString("id_session") != null)
                {
                    int id = Convert.ToInt32(HttpContext.Session.GetString("id_session"));
                    if(getRole(id) == 2)
                    {
                        TempData["name"]= getNameS(id);
                        return RedirectToAction("Index","Students");
                    }
                    if (getRole(id) == 1)
                    {
                        TempData["name"]= getNameL(id);
                        return RedirectToAction("Index", "Lecturers");
                    }
                    if (getRole(id) == 3)
                    {
                        //return to admin control page
                    }
                }
             }
            if (chkLogin() == false)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(string email, string password)
        {
            var conn=Connect();
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
                        string id = tb.Rows[0]["ID"].ToString();
                        HttpContext.Session.SetString("id_session", id);
                        HttpContext.Session.SetString("login","1");
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

        private int getRole(int id)
        {
            var conn = Connect();
            conn.Open();
            string sql = $"SELECT Role from Account where ID='{id}'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            role = (int)cmd.ExecuteScalar();
            conn.Close();
            return role;
        }
        
        public string getNameS(int id)
        {
            var conn = Connect();
            conn.Open();
            string sql = $"select Name from Student,Account where Account.Role =2 and Account.ID=Student.ID and Student.ID='{id}'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            name = cmd.ExecuteScalar().ToString();
            conn.Close();
            return name;
        }

        public string getNameL(int id)
        {
            var conn = Connect();
            conn.Open();
            string sql = $"select Name from Lecturer,Account where Account.Role =1 and Account.ID=Lecturer.ID and Lecturer.ID='{id}'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            name = cmd.ExecuteScalar().ToString();
            conn.Close();
            return name;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();HttpContext.Session.Remove("login");
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}