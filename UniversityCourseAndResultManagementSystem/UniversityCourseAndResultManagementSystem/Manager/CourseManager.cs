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
        CourseGateway aCourseGateway = new CourseGateway();
        public string SaveCourse(Course aCourse)
        {
            if (aCourseGateway.SaveCourse(aCourse) > 0)
            {
                return "Saved";
            }
            return "failed";
        }
    }
}