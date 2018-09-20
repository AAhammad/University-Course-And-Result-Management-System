using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class SemesterGateway:DatabaseGateway
    {
        public List<Semester> GetAllSemester()
        {

            string query = "SELECT * FROM Semester_tbl";
            CommandObj.CommandText = query;
            List<Semester> semesters = new List<Semester>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                Semester semester = new Semester
                {
                    SemesterId = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Title"].ToString(),

                };
                semesters.Add(semester);
            }
            reader.Close();
            ConnectionObj.Close();

            return semesters;






        }
    }
}