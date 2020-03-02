using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using FinalDemo.Models;

namespace FinalDemo.Controllers
{
    public class LoginController : Controller
    {
        string connection = @"Data Source=pavan-pc;Initial Catalog=TestDetails;Integrated Security=True";

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            
            DataTable tblAdmin = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Admins WHERE Username = '"+admin.Username+"' AND Password = '"+admin.Password+"' ";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlCon);
                sqlData.Fill(tblAdmin);
            }
            if (tblAdmin.Rows.Count > 0)
            {
                Session["loggedId"] = tblAdmin.Rows[0][0].ToString();
                Session["loggedUser"] = tblAdmin.Rows[0][1].ToString();

                using (SqlConnection sqlCon = new SqlConnection(connection))
                {
                    sqlCon.Open();
                    string queryUp = "UPDATE Admins SET Status=1 WHERE AdminId='"+Session["loggedId"]+"' ";
                    SqlDataAdapter sqlDataUp = new SqlDataAdapter(queryUp, sqlCon);
                    sqlDataUp.Fill(tblAdmin);
                }

                
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                return View();
            }
            
        }



        public ActionResult Logout()
        {
            DataTable tblAdmin = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string queryLog = "UPDATE Admins SET Status=0 WHERE AdminId='"+Session["loggedId"]+"' ";
                SqlDataAdapter sqlDataLog = new SqlDataAdapter(queryLog, sqlCon);
                sqlDataLog.Fill(tblAdmin);
            }

            Session.Abandon();
            return RedirectToAction("Login");
        }

            
    }
}