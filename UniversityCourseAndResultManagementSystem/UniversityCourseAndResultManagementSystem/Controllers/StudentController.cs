using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private string message;
        StudentManager studentManager = new StudentManager();
        DepartmentManager departmentManager = new DepartmentManager();
       
        //
        // GET: /Student/
        public ActionResult Index()
        {
            List<Student> students = studentManager.GetAllStudents();

            return View(students);
        }


        // GET: /Student/Create
        [HttpGet]
        public ActionResult Save()
        {

            List<Department> departments = departmentManager.GetAllDepts();

            ViewBag.Departments = departments;
            return View();
        }

        //
        // POST: /Student/Create
        [HttpPost]
        public ActionResult Save(Student aStudent)
        {


            IEnumerable<Department> departments = departmentManager.GetAllDepts();
            if (ModelState.IsValid)
            {
                message = studentManager.Save(aStudent);
                ViewBag.Message = message;
            }
              


                ViewBag.Departments = departments;
                
                ViewBag.StudentInfo = aStudent;

                return View(aStudent);
                //return RedirectToAction("Index");
          
        }

        [HttpPost]
        public JsonResult CheckingExistingEmail(string Email)
        {
            return Json(!studentManager.GetAllStudents().Any(x => x.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)), JsonRequestBehavior.AllowGet);
        }

	}
}