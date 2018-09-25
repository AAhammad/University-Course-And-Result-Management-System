using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager aDepartmetManager = new DepartmentManager();
        SemesterManager aSemesterManager = new SemesterManager();
        CourseManager aCourseManager = new CourseManager();

        // GET: /Course/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Save()
        {
            List<Department> departments = aDepartmetManager.GetAllDepts();
            List<Semester> semesters = aSemesterManager.GetAllSemesters();
            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;
            return View();

        }

        [HttpPost]
        public ActionResult Save(Course aCourse)
        {
            List<Department> departments = aDepartmetManager.GetAllDepts();
            List<Semester> semesters = aSemesterManager.GetAllSemesters();
            if (ModelState.IsValid)
            {
                string message = aCourseManager.SaveCourse(aCourse);
                ViewBag.Mgs = message;
                
                
            }

            ViewBag.Departments = departments;
            ViewBag.Semesters = semesters;
            return View(aCourse);

        }


        [HttpGet]
        public ActionResult UnAssignCourses()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAssignCourses(int? id)
        {
            ViewBag.Message = aCourseManager.UnAssignCourses();
            return View();
        }
	}
}