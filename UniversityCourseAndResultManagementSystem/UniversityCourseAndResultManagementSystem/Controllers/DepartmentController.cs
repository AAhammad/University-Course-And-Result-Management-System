using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager aDepartmetManager = new DepartmentManager();
        // GET: /Department/
        public ActionResult Index()
        {
            List<Department> departments = aDepartmetManager.GetAllDepts();
            return View(departments);
        }

        public ActionResult Save()
        {
            return View();
        }

        //
        // POST: /Department/Create
        [HttpPost]
        public ActionResult Save(Department aDepartment)
        {

            string message = aDepartmetManager.SaveDept(aDepartment);
            ViewBag.Mgs = message;
            return View();




        }
	}
}