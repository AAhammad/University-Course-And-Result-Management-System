using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class ViewCourseStaticsController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();

        //
        // GET: /ViewCourseStatics/

        public ActionResult ShowCourseStatics()
        {
            IEnumerable<Department> departments = departmentManager.GetAllDepts();
            ViewBag.Departments = departments;
            List<CourseViewModel> courseViewModels = courseManager.GetCourseViewModels();
            return View(courseViewModels);
        }

        public JsonResult GetCourseInformationByDepartmentId(int departmentId)
        {
            IEnumerable<CourseViewModel> courseViewModels = courseManager.GetCourseViewModels().FindAll(deptId => deptId.DepartmentId == departmentId);
           
            return Json(courseViewModels, JsonRequestBehavior.AllowGet);


        }
	}
}