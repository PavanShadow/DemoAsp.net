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
    public class EmployeeController : Controller
    {
        string connection = @"Data Source=pavan-pc;Initial Catalog=TestDetails;Integrated Security=True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable tblEmployee = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("SELECT * FROM Employee", sqlCon);
                sqlData.Fill(tblEmployee);

            }
            return View(tblEmployee);
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Employee());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee empModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query = "INSERT INTO Employee VALUES(@Name,@Position,@Office,@Age,@Salary)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Name", empModel.Name);
                sqlCmd.Parameters.AddWithValue("@Position", empModel.Position);
                sqlCmd.Parameters.AddWithValue("@Office", empModel.Office);
                sqlCmd.Parameters.AddWithValue("@Age", empModel.Age);
                sqlCmd.Parameters.AddWithValue("@Salary", empModel.Salary);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            DataTable tblEmployee = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query1 = "SELECT * FROM Employee WHERE EmployeeId = @EmployeeId";
                SqlDataAdapter sqlData1 = new SqlDataAdapter(query1, sqlCon);
                sqlData1.SelectCommand.Parameters.AddWithValue("@EmployeeId", id);
                sqlData1.Fill(tblEmployee);

            }
            if (tblEmployee.Rows.Count == 1)
            {
                employee.EmployeeId = Convert.ToInt32(tblEmployee.Rows[0][0].ToString());
                employee.Name = tblEmployee.Rows[0][1].ToString();
                employee.Position = tblEmployee.Rows[0][2].ToString();
                employee.Office = tblEmployee.Rows[0][3].ToString();
                employee.Age = Convert.ToInt32(tblEmployee.Rows[0][4].ToString());
                employee.Salary = Convert.ToInt32(tblEmployee.Rows[0][5].ToString());
                
                return View(employee);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Employee empModel)
        {

            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                string query2 = "UPDATE Employee SET Name=@Name, Position=@Position, Office=@Office, Age=@Age, Salary=@Salary WHERE EmployeeId=@EmployeeId";
                SqlCommand sqlCmd = new SqlCommand(query2, sqlCon);
                sqlCmd.Parameters.AddWithValue("@EmployeeId", empModel.EmployeeId);
                sqlCmd.Parameters.AddWithValue("@Name", empModel.Name);
                sqlCmd.Parameters.AddWithValue("@Position", empModel.Position);
                sqlCmd.Parameters.AddWithValue("@Office", empModel.Office);
                sqlCmd.Parameters.AddWithValue("@Age", empModel.Age);
                sqlCmd.Parameters.AddWithValue("@Salary", empModel.Salary);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
