using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class CourseGateway:DatabaseGateway
    {
        public int SaveCourse(Course aCourse)
        {

            string query = "INSERT Course_tbl (Code,Name,Credit,Description,DepartmentId,SemesterId) VALUES(@code,@name,@credit,@description,@department,@semester)";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("code", aCourse.Code.ToUpper());
            CommandObj.Parameters.AddWithValue("name", aCourse.Name);
            CommandObj.Parameters.AddWithValue("credit", aCourse.Credit);
            CommandObj.Parameters.AddWithValue("description", aCourse.Description);
            CommandObj.Parameters.AddWithValue("department", aCourse.DepartmentId);
            CommandObj.Parameters.AddWithValue("semester", aCourse.SemesterId);

            ConnectionObj.Open();

            int rowAffact = CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return rowAffact;


        }
    }
}