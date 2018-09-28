using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Manager;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class CourseAssignToTeacherGateway : DatabaseGateway
    {
        TeacherManager teacherManager = new TeacherManager();
        public int SaveCourseAssignToTeacherInfo(CourseAssignToTeacher courseAssign)
        {
            ConnectionObj.Open();


            CommandObj.CommandText = "INSERT INTO CourseAssignToTeacher_tbl (DepartmentId,TeacherId,CourseId,IsAssign) VALUES(@deptId,@teacherId,@courseId,@status)";
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@deptId", courseAssign.DepartmentId);
            CommandObj.Parameters.AddWithValue("@teacherId", courseAssign.TeacherId);
            CommandObj.Parameters.AddWithValue("@courseId", courseAssign.CourseId);
            CommandObj.Parameters.AddWithValue("@status", 1);
            int rowAffected = CommandObj.ExecuteNonQuery();

            int updateResult = UpdateTeacher(courseAssign);


            CommandObj.Dispose();
            ConnectionObj.Close();
            return rowAffected;




        }



        private int UpdateTeacher(CourseAssignToTeacher courseAssign)
        {
            Teacher teacher = teacherManager.GetAllTeachers().ToList().Find(t => t.Id == courseAssign.TeacherId);

            decimal creditTakenbyTeacher = Convert.ToDecimal(teacher.CreditTaken) + Convert.ToDecimal(courseAssign.Credit);
         

            CommandObj.CommandText = "Update Teacher_tbl Set CreditTaken=@creditTaken WHERE Id=@id";
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("creditTaken", creditTakenbyTeacher);
            CommandObj.Parameters.AddWithValue("id", courseAssign.TeacherId);
            return CommandObj.ExecuteNonQuery();
        }

        public List<CourseAssignToTeacher> GetAllCourseAssignToTeacher()
        {

            CommandObj.CommandText = "SELECT * FROM CourseAssignToTeacher_tbl";
            List<CourseAssignToTeacher> courseAssignToTeachers = new List<CourseAssignToTeacher>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                CourseAssignToTeacher assignToTeacher = new CourseAssignToTeacher
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                    TeacherId = Convert.ToInt32(reader["TeacherId"].ToString()),
                    CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                    Status = Convert.ToBoolean(reader["IsAssign"].ToString())

                };
                courseAssignToTeachers.Add(assignToTeacher);
            }
            reader.Close();

            CommandObj.Dispose();
            ConnectionObj.Close();

            return courseAssignToTeachers;




        }


        public int Update(CourseAssignToTeacher courseAssign)
        {
            ConnectionObj.Open();

            CommandObj.CommandText = "UPDATE CourseAssignToTeacher_tbl SET IsAssign=1 WHERE TeacherId=@teacherId AND CourseId=@courseId";
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("teacherId",courseAssign.TeacherId );
            CommandObj.Parameters.AddWithValue("courseId", courseAssign.CourseId);
            CommandObj.ExecuteNonQuery();

            int updateResult = UpdateTeacher(courseAssign);


            CommandObj.Dispose();
            ConnectionObj.Close();
            return updateResult;

        }
    }
}