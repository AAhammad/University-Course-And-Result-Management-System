using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class CourseAssignToTeacherController : Controller
    {
        //
        // GET: /CourseAssignToTeacher/
        public ActionResult Index()
        {
            return View();
        }

        CourseManager courseManger = new CourseManager();
        DepartmentManager departmentManager = new DepartmentManager();
        TeacherManager teacherManager = new TeacherManager();
        CourseAssignToTeacherManager courseAssignToTeacherManager = new CourseAssignToTeacherManager();
        public ActionResult AssignCourseToTeacher()
        {
            List<Course> courses = courseManger.GetAllCourses();
            List<Department> departments = departmentManager.GetAllDepts();
            List<Teacher> teachers = teacherManager.GetAllTeachers();
            ViewBag.Courses = courses;
            ViewBag.Departments = departments;
            ViewBag.Teachers = teachers;
            return View();
        }
        [HttpPost]
        public ActionResult AssignCourseToTeacher(CourseAssignToTeacher courseAssign)
        {
            
                ViewBag.Message = courseAssignToTeacherManager.Save(courseAssign);
            List<Course> courses = courseManger.GetAllCourses();
            List<Department> departments = departmentManager.GetAllDepts();
            List<Teacher> teachers = teacherManager.GetAllTeachers();
                ViewBag.Courses = courses;
                ViewBag.Departments = departments;
                ViewBag.Teachers = teachers;
                return View();
           
              


        }

        public JsonResult GetTeachersByDepartmentId(int departmentId)
        {

            List<Teacher> teachers = teacherManager.GetAllTeachers();
            var teacherList = teachers.ToList().FindAll(t => t.DepartmentId == departmentId);
            return Json(teacherList, JsonRequestBehavior.AllowGet);


        }

        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            List<Course> courses = courseManger.GetAllCourses();
            var courseList = courses.ToList().FindAll(c => c.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacherById(int teacherId)
        {
            IEnumerable<Teacher> teachers = teacherManager.GetAllTeachers();
            Teacher aTeacher = teachers.ToList().Find(t => t.Id == teacherId);
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourseById(int courseId)
        {
            IEnumerable<Course> courses = courseManger.GetAllCourses();
            Course aCourse = courses.ToList().Find(c => c.Id == courseId);
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }
	}
}