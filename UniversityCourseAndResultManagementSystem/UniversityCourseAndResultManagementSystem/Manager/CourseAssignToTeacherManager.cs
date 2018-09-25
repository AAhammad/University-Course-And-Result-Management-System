using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class CourseAssignToTeacherManager
    {
        CourseAssignToTeacherGateway courseAssignToTeacherGateway = new CourseAssignToTeacherGateway();
        public string Save(CourseAssignToTeacher courseAssign)
        {

            CourseAssignToTeacher courseAssignTo = GetAllaCourseAssignToTeachers().ToList().Find(ca => ca.CourseId == courseAssign.CourseId && ca.Status);

            if (courseAssignTo == null)
            {
                if (courseAssignToTeacherGateway.SaveCourseAssignToTeacherInfo(courseAssign) > 0)
                {
                    return "Assigned successfully";
                }
                return "Failed to save";
            }

            return "Overlaping not allowed!";
        }


        public List<CourseAssignToTeacher> GetAllaCourseAssignToTeachers()
        {
            return courseAssignToTeacherGateway.GetAllCourseAssignToTeacher();
        } 

    }
}