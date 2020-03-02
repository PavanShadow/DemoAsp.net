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

        SqlConnection sqlCon = new SqlConnection();
        SqlCommand sqlCom = new SqlCommand();
        SqlDataReader dr;


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        void connectString()
        {
            sqlCon.ConnectionString = "Data Source=pavan-pc;Initial Catalog=TestDetails;Integrated Security=True";
        }

        [HttpPost]
        public ActionResult Login(Admin adminModel)
        {
            connectString();
            sqlCon.Open();
            sqlCom.Connection = sqlCon;
            sqlCom.CommandText = "SELECT * FROM Admins WHERE Username='" + adminModel.Username + "' AND Password='" + adminModel.Password + "'";
            //sqlCom.CommandText = "UPDATE Admins SET Status ='1' WHERE Username='" + adminModel.Username + "' AND Password='" + adminModel.Password + "'";
            dr = sqlCom.ExecuteReader();
            if (dr.Read())
            {
                sqlCon.Close();
                return RedirectToAction("Create", "Employee");
            }
            else
            {
                sqlCon.Close();
                return View();
            }

            
        }
    }
}