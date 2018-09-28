using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Controllers
{
    public class ClassRoomController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();
        RoomManager roomManager = new RoomManager();
        DayManager dayManager = new DayManager();
        ClassRoomManager classRoomManager = new ClassRoomManager();

        private List<AllocateClassSchedule> classRooms;
        //
        // GET: /ClassRoom/get information about room allocation
        public ActionResult Index()
        {
            ViewBag.Departments = departmentManager.GetAllDepts();
            classRooms = classRoomManager.GetAllClassSchedules();
            ViewBag.ClassSchedule = classRooms;
            return View();
        }


        // GET: /ClassRoom/Create
        public ActionResult Save()
        {
            List<Day> days = dayManager.GetAllDays();
            ViewBag.Days = days;
            List<Room> rooms = roomManager.GetAllRooms();
            ViewBag.Rooms = rooms;
            ViewBag.Departments = departmentManager.GetAllDepts();
            ViewBag.Courses = courseManager.GetAllCourses();
            return View();
        }

        //
        // POST: /ClassRoom/Create
        [HttpPost]
        public ActionResult Save(ClassRoom classRoom)
        {
            

                string message = classRoomManager.Save(classRoom);

                ViewBag.Message = message;
                List<Day> days = dayManager.GetAllDays();
                ViewBag.Days = days;
                List<Room> rooms = roomManager.GetAllRooms();
                ViewBag.Rooms = rooms;
                var depts = departmentManager.GetAllDepts();
                ViewBag.Departments = depts;
                ViewBag.Courses = courseManager.GetAllCourses();
                return View();
           
        }
        public JsonResult GetClassScheduleByDepartment(int departmentId)
        {
            var courses = courseManager.GetCourseByDepartmentId(departmentId);

            List<object> clsSches = new List<object>();

            foreach (var course in courses)
            {
                var scheduleInfo = classRoomManager.GetAllClassSchedulesByDeparmentId(departmentId, course.Id);
                if (scheduleInfo == "")
                {
                    scheduleInfo = "Not sheduled yet";
                }


                var clsSch = new
                {
                    CourseCode = course.Code,
                    CourseName = course.Name,
                    ScheduleInfo = scheduleInfo
                };
                clsSches.Add(clsSch);
            }
            return Json(clsSches, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCoursesByDepartmentId(int departmentId)
        {
            IEnumerable<Course> courses = courseManager.GetAllCourses();
            var courseList = courses.ToList().FindAll(c => c.DepartmentId == departmentId);
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UnAllocateClassRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAllocateClassRoom(int? id)
        {
            ViewBag.Message = classRoomManager.UnAllocateClassRoom();
            return View();
        }
	}
}