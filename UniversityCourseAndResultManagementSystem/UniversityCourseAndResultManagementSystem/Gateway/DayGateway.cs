using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class DayGateway:DatabaseGateway
    {
        public List<Day> GetAllDays()
        {

            var dayList = new List<Day>();
            CommandObj.CommandText = "SELECT * FROM Day_tbl";
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();

            while (reader.Read())
            {
                var day = new Day
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString()
                };
                dayList.Add(day);
            }
            reader.Close();
            ConnectionObj.Close();
            CommandObj.Dispose();
            return dayList;




        }
    }
}