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
                return "Code must be at least five (5) characters long.";
            }
            if (courseGateway.GetAllCourse().Exists(x=>x.Code.Equals(aCourse.Code,StringComparison.OrdinalIgnoreCase)))
            {
                return "Course code Already Exists!";
            }
            if (courseGateway.GetAllCourse().Exists(x => x.Name.Equals(aCourse.Name,StringComparison.OrdinalIgnoreCase)))
            {
                return "Course Name Already Exists!";
            }
            if (courseGateway.SaveCourse(aCourse) > 0)
            {
                return "Saved";
            }
            return "failed";
        }

      

        private bool IsCorseCodeValid(Course aCourse)
        {
            if (aCourse.Code.Length >= 5)
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