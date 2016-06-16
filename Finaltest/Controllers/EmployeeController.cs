using Finaltest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ActionResult update(String empId) {
            List<Employees> bingo = db.Employees.Where(x => x.EmployeeID.ToString() == empId).ToList();

            Employees data = new Employees();
            
            String birthDate = "";
            String birthMon = "";
            String birthDay = "";

            String hireDate = "";
            String hireMon = "";
            String hireDay = "";
            
            foreach (var item in bingo)
            {
                data.EmployeeID = item.EmployeeID;
                data.LastName = item.LastName;
                data.FirstName = item.FirstName;
                data.Title = item.Title;
                data.TitleOfCourtesy = item.TitleOfCourtesy;
                data.Address = item.Address;
                data.City = item.City;
                data.Region = item.Region;
                data.PostalCode = item.PostalCode;
                data.Country = item.Country;
                data.Phone = item.Phone;

                birthMon = alterDate(item.BirthDate.Month.ToString());
                birthDay = alterDate(item.BirthDate.Day.ToString());
                birthDate = String.Format("{0}-{1}-{2}", item.BirthDate.Year, birthMon, birthDay);

                hireMon = alterDate(item.HireDate.Month.ToString());
                hireDay = alterDate(item.HireDate.Day.ToString());
                hireDate = String.Format("{0}-{1}-{2}", item.HireDate.Year, hireMon, hireDay);
            }
            ViewBag.data = data;
            ViewBag.birthDate = birthDate;
            ViewBag.hireDate = hireDate;

            List<Employees> bingolastname = db.Employees.Where(x => x.LastName != data.LastName).ToList();
            ViewBag.bingolastnamelist = bingolastname;

            List<Employees> bingofirstname = db.Employees.Where(x => x.FirstName != data.FirstName).ToList();
            ViewBag.bingofirstnamelist = bingofirstname;

            List<Employees> bingotitle = db.Employees.Where(x => x.Title != data.Title).ToList();
            ViewBag.bingotitlelist = bingotitle;

            List<Employees> bingotitleofcourtesy = db.Employees.Where(x => x.TitleOfCourtesy != data.TitleOfCourtesy).ToList();
            ViewBag.bingotitleofcourtesylist = bingotitleofcourtesy;

            return View();
        }
        public String alterDate(String date)
        {
            StringBuilder change = new StringBuilder();
            if (date.Length < 2)
            {
                change.Append("0");//append字串相加
            }
            change.Append(date);
            return change.ToString();
        }
        [HttpPost]
        public ActionResult update(FormCollection inputs)
        {
            String empId = inputs["empId"];
            String lastname = inputs["lastname"];
            String firstname = inputs["firstname"];
            String title = inputs["title"];
            String titleofcourtesy = inputs["titleofcourtesy"];
            String birthdate = inputs["birthdate"];
            String hiredate = inputs["hiredate"];
            String add = inputs["add"];
            String city = inputs["city"];
            String region = inputs["region"];
            String postalcode = inputs["postalcode"];
            String country = inputs["country"];
            String phone = inputs["phone"];

            Employees data = db.Employees.Find(Convert.ToInt32(empId));
            data.EmployeeID = Convert.ToInt32(empId);
            data.LastName = lastname;
            data.FirstName = firstname;
            data.Title = title;
            data.TitleOfCourtesy = titleofcourtesy;
            data.BirthDate = Convert.ToDateTime(birthdate);
            data.HireDate = Convert.ToDateTime(hiredate);
            data.Address = add;
            data.City = city;
            data.Region = region;
            data.PostalCode = postalcode;
            data.Country = country;
            data.Phone = phone;

            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult add()
        {
            int empId = db.Employees.Select(x => x.EmployeeID).Max() + 1;
            ViewBag.empId = empId;

            List<Employees> employee = db.Employees.ToList();
            ViewBag.employee = employee;

            return View();
        }
        [HttpPost]
        public ActionResult add(FormCollection inputs)
        {
            String empId = inputs["empId"];
            String lastname = inputs["lastname"];
            String firstname = inputs["firstname"];
            String title = inputs["title"];
            String titleofcourtesy = inputs["titleofcourtesy"];
            String birthdate = inputs["birthdate"];
            String hiredate = inputs["hiredate"];
            String add = inputs["add"];
            String city = inputs["city"];
            String region = inputs["region"];
            String postalcode = inputs["postalcode"];
            String country = inputs["country"];
            String phone = inputs["phone"];

            Employees data = new Employees();

            data.EmployeeID = Convert.ToInt32(empId);
            data.LastName = lastname;
            data.FirstName = firstname;
            data.Title = title;
            data.TitleOfCourtesy = titleofcourtesy;
            data.BirthDate = Convert.ToDateTime(birthdate);
            data.HireDate = Convert.ToDateTime(hiredate);
            data.Address = add;
            data.City = city;
            data.Region = region;
            data.PostalCode = postalcode;
            data.Country = country;
            data.Phone = phone;

            db.Employees.Add(data);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public void delete(int emploId) {
            Employees emp = db.Employees.Find(emploId);
            db.Employees.Remove(emp);
            db.SaveChanges();
        }
    }
}