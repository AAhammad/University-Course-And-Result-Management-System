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
            
                message = studentManager.Save(aStudent);

                IEnumerable<Department> departments = departmentManager.GetAllDepts();

                ViewBag.Departments = departments;
                ViewBag.Message = message;
                ViewBag.StudentInfo = aStudent;

                return View();
                //return RedirectToAction("Index");
          
        }

	}
}