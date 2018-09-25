using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class TeacherManager
    {
        TeacherGateway teacherGateway = new TeacherGateway();

        public List<Teacher> GetAllTeachers()
        {

            return teacherGateway.GetAllTeachers();

        }


        public string Save(Teacher teacher)
        {
           
            if (teacherGateway.GetAllTeachers().Exists(x=>x.Email.Equals(teacher.Email,StringComparison.OrdinalIgnoreCase)))
            {
                return "This Email address already exits try another one.";
            }
            if (teacherGateway.SaveTeacher(teacher) > 0)
            {
                return "Saved";
            }
            return "Failed to save";
        }

      

     


    }
}