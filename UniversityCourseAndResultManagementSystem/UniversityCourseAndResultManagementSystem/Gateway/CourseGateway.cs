using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class CourseGateway : DatabaseGateway
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

        TeacherGateway teacherGateway = new TeacherGateway();
        public List<Course> GetAllCourse()
        {
            string query = "SELECT * FROM Course_tbl";
            CommandObj.CommandText = query;
            List<Course> courses = new List<Course>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();

            while (reader.Read())
            {
                Course course = new Course();

                course.Id = Convert.ToInt32(reader["Id"].ToString());
                course.Code = reader["Code"].ToString();
                course.Name = reader["Name"].ToString();
                course.Credit = Convert.ToDecimal(reader["Credit"].ToString());
                course.Description = (string) reader["Description"];
                course.DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString());
                course.SemesterId = Convert.ToInt32(reader["SemesterId"].ToString());

                
                courses.Add(course);
            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();

            return courses;



        }




        public Course GetCourseByName(string name)
        {

            string query = "SELECT * FROM Course_tbl WHERE Name=@name";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@name", name);
            ConnectionObj.Open();
            Course course = null;
            SqlDataReader reader = CommandObj.ExecuteReader();
            if (reader.Read())
            {
                course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Credit = Convert.ToDecimal(reader["Credit"].ToString()),
                    Description = reader["Descirption"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                };

            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();
            return course;
        }






        public Course GetCourseByCode(string code)
        {

            string query = "SELECT * FROM Course_tbl WHERE Code=@code";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@code", code);
            ConnectionObj.Open();
            Course course = null;
            SqlDataReader reader = CommandObj.ExecuteReader();
            if (reader.Read())
            {
                course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDecimal(reader["Credit"].ToString()),
                    Description = reader["Descirption"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                };

            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();
            return course;
        }






        public List<CourseViewModel> GetCourseViewModels()
        {

            string query = "SELECT d.Id as DepartmentId,COALESCE(c.Code,'Not Assigned yet') AS Code,COALESCE(c.Name,'Not Assigned yet') AS Name,COALESCE(s.Title,'Not Assigned yet') as Semester,COALESCE(t.Name,'Not Assigned yet')  as Teacher FROM  Department_tbl d  LEFT OUTER JOIN Course_tbl  c  ON d.Id=c.DepartmentId LEFT OUTER JOIN  Semester_tbl s ON c.SemesterId=s.Id  LEFT OUTER JOIN CourseAssignToTeacher_tbl Ct  ON (c.Id=Ct.CourseId AND Ct.IsAssign=1) LEFT OUTER JOIN Teacher_tbl t ON t.Id=Ct.TeacherId ORDER BY c.Code";

            CommandObj.CommandText = query;
            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                CourseViewModel course = new CourseViewModel
                {
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Semester = reader["Semester"].ToString(),
                    Teacher = reader["Teacher"].ToString()



                };
                courseViewModels.Add(course);
            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();

            return courseViewModels;




        }

        public List<Course> GetCoursesTakeByaStudentByStudentId(int id)
        {


            string query = "SELECT c.Id,c.Code,c.Name,c.Credit,c.Descirption,c.DepartmentId,c.SemesterId FROM t_Course c INNER JOIN t_StudentEnrollInCourse r ON (c.Id=r.CourseId AND r.StudentId=@StudentId AND r.IsStudentActive=1)";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@StudentId", id);
            ConnectionObj.Open();
            List<Course> courses = new List<Course>();
            SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                Course aCourse = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDecimal(reader["Credit"].ToString()),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    Description = reader["Descirption"].ToString(),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())
                };
                courses.Add(aCourse);
            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();
            return courses;



        }

        public int UnAssignCourse()
        {
            ConnectionObj.Open();

            CommandObj.CommandText = "UPDATE CourseAssignToTeacher_tbl SET IsAssign=0";

            CommandObj.ExecuteNonQuery();
            teacherGateway.UpdateTeacherInformation();
            int a = ResetStudentResult();
            int i = UnAssignStudentCourse();
            CommandObj.Dispose();
            ConnectionObj.Close();
            return i;


        }

        private int ResetStudentResult()
        {
            CommandObj.CommandText = "UPDATE StudentResult_tbl SET IsStudentActive=0";
            return CommandObj.ExecuteNonQuery();
        }



        private int UnAssignStudentCourse()
        {
            CommandObj.CommandText = "UPDATE StudentEnrollInCourse_tbl SET IsStudentActive=0";
            return CommandObj.ExecuteNonQuery();
        }

        public List<Course> GetCourseByDepartmentId(int departmentId)
        {


            string query = "SELECT * FROM Course_tbl WHERE DepartmentId='" + departmentId + "'";
            CommandObj.CommandText = query;
            List<Course> courses = new List<Course>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Credit = Convert.ToDecimal(reader["Credit"].ToString()),
                    Description = reader["Description"].ToString(),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    SemesterId = Convert.ToInt32(reader["SemesterId"].ToString())

                };
                courses.Add(course);
            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();

            return courses;



        }
    }
}