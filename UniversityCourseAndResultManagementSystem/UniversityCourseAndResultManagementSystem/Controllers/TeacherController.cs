using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        private string message;
        TeacherManager teacherManager = new TeacherManager();
        DesignationManager designationManager = new DesignationManager();
        DepartmentManager departmentManager = new DepartmentManager();
        //
        // GET: /Teacher/
        public ActionResult Index()
        {
            List<Teacher> teachers = teacherManager.GetAllTeachers();
            return View(teachers);
        }

       

        //
        // GET: /Teacher/Create
        public ActionResult Save()
        {
            List<Designation> desinationList = designationManager.GetAllDesignations();
            List<Department> departments = departmentManager.GetAllDepts();
            ViewBag.Designations = desinationList;
            ViewBag.Departments = departments;
            return View();
        }

        //
        // POST: /Teacher/Create
        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
           
                message = teacherManager.Save(teacher);
                IEnumerable<Designation> desinationList = designationManager.GetAllDesignations();
                IEnumerable<Department> departments = departmentManager.GetAllDepts();
                ViewBag.Designations = desinationList;
                ViewBag.Departments = departments;
                ViewBag.Message = message;
                return View();
               
            }
            
        

	}
}