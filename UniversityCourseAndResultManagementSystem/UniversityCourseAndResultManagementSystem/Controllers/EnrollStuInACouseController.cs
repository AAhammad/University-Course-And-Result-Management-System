using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class EnrollStuInACouseController : Controller
    {
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();

        [HttpGet]
        public ActionResult Enroll()
        {
            IEnumerable<Student> students = studentManager.GetAllStudents();
            IEnumerable<Course> courses = courseManager.GetAllCourses();
            ViewBag.Students = students;
            ViewBag.Courses = courses;
            return View();
        }
        [HttpPost]
        public ActionResult Enroll(EnrollStudentInCourse enrollStudent)
        {
            string message;
           
                message = studentManager.Save(enrollStudent);
                ViewBag.Message = message;
                IEnumerable<Student> students = studentManager.GetAllStudents();
                IEnumerable<Course> courses = courseManager.GetAllCourses();
                ViewBag.Students = students;
                ViewBag.Courses = courses;

                return View();
          
        }

        public JsonResult GetStudentById(int studentId)
        {
            StudentViewModel student = studentManager.GetStudentInformationById(studentId);
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            Student aStudent = studentManager.GetAllStudents().Find(st => st.Id == studentId);
            IEnumerable<Course> courses = courseManager.GetAllCourses().FindAll(d => d.DepartmentId == aStudent.DepartmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);

        }
	}
}