using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class CourseManager
    {
      

        CourseGateway courseGateway = new CourseGateway();
        public List<Course> GetAllCourses()
        {
            return courseGateway.GetAllCourse();
        }
        public string SaveCourse(Course aCourse)
        {
            if (!(IsCorseCodeValid(aCourse)))
            {
                return "Course code must be at least 5 character of length";
            }
            if (IsCourseCodeExits(aCourse.Code))
            {
                return "Course code Already Exists ! Code must be unique";
            }
            if (IsCourseNameExits(aCourse.Name))
            {
                return "Course Name Already Exists ! Name must be unique";
            }
            if (courseGateway.SaveCourse(aCourse) > 0)
            {
                return "Saved";
            }
            return "failed";
        }

        private bool IsCourseNameExits(string name)
        {

            Course course = courseGateway.GetCourseByName(name);
            if (course != null)
            {
                return true;
            }

            return false;
        }

        private bool IsCourseCodeExits(string code)
        {
            Course course = courseGateway.GetCourseByCode(code.ToUpper());

            if (course != null)
            {
                return true;
            }

            return false;
        }

        private bool IsCorseCodeValid(Course aCourse)
        {
            if (aCourse.Code.Length > 5)
            {
                return true;
            }
            return false;

        }

        public List<CourseViewModel> GetCourseViewModels()
        {
            return courseGateway.GetCourseViewModels();
        }

        public List<Course> GetCoursesTakenByaStudentById(int id)
        {
            return courseGateway.GetCoursesTakeByaStudentByStudentId(id);
        }
        public string UnAssignCourses()
        {
            if (courseGateway.UnAssignCourse() > 0)
            {
                return "Unassign Courses Successfully!";
            }
            return "Failed to unassign";
        }

        public List<Course> GetCourseByDepartmentId(int departmentId)
        {
            return courseGateway.GetCourseByDepartmentId(departmentId);
        }
    }
}