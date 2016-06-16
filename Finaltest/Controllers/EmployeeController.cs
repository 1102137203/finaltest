using Finaltest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finaltest.Controllers
{
    public class EmployeeController : Controller
    {
        DB db = new DB();
        // GET: Employee
        public ActionResult Index()
        {
            List<String> ename = db.Employees.Select(x => x.FirstName).ToList();
            List<String> etitle = db.Employees.Select(x => x.Title).ToList();
            ViewBag.ename = ename;
            ViewBag.etitle = etitle;
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection emp) {
            String empId = emp["empId"];
            String emplo = emp["emplo"];
            String title = emp["title"];
            String birthDate = emp["birthDate"];
            String hireDate = emp["hireDate"];
            String address = emp["address"];
            String city = emp["city"];
            String region = emp["region"];
            String postalCode = emp["postalCode"];
            return RedirectToAction("search",
                new { empId, emplo, title, birthDate, hireDate, address, city, region, postalCode });
        }

        public ActionResult search(String empId,String emplo,String title,String birthDate,String hireDate,String address,String city,String region,String postalCode) {
            DateTime bthdt = Convert.ToDateTime(birthDate);
            DateTime hrdt = Convert.ToDateTime(hireDate);

            List<Employees> bingo = db.Employees.Where(x =>
             (String.IsNullOrEmpty(empId) ? true : x.EmployeeID.ToString() == empId) &&
             (String.IsNullOrEmpty(emplo) ? true : x.FirstName == emplo) &&
             (String.IsNullOrEmpty(title) ? true : x.Title == title) &&
             (String.IsNullOrEmpty(birthDate) ? true : x.BirthDate == bthdt) &&
             (String.IsNullOrEmpty(hireDate) ? true : x.HireDate == hrdt) &&
             (String.IsNullOrEmpty(address) ? true : x.Address == address) &&
             (String.IsNullOrEmpty(city) ? true : x.City == city) &&
             (String.IsNullOrEmpty(region) ? true : x.Region == region) &&
             (String.IsNullOrEmpty(postalCode) ? true : x.PostalCode == postalCode)
             ).ToList();
            ViewBag.bingo = bingo;
            return View();
        }
        public ActionResult update() {
            return View();
        }
        public ActionResult add()
        {
            int empId = db.Employees.Select(x => x.EmployeeID).Max() + 1;
            ViewBag.empId = empId;

            List<Employees> employees = db.Employees.ToList();
            ViewBag.employees = employees;
            return View();
        }
    }
}