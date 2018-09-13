using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class DepartmentGateway:DatabaseGateway
    {
        public List<Department> GetAllDepts()
        {

            string query = "SELECT * FROM Department_tbl";
            CommandObj.CommandText = query;
            List<Department> departments = new List<Department>();
            ConnectionObj.Open();
            SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                Department department = new Department
                {
                    DepartmentId = Convert.ToInt32(reader["ID"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString()
                };

                departments.Add(department);
            }
            reader.Close();
            ConnectionObj.Close();

            return departments;






        }


        public int SaveDept(Department aDepartment)
        {

            string query = "INSERT Department_tbl (Code,Name) VALUES(@code,@name)";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("code", aDepartment.Code.ToUpper());
            CommandObj.Parameters.AddWithValue("name", aDepartment.Name);
            ConnectionObj.Open();

            int rowAffact = CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return rowAffact;


        }

        public int GetDepartmentByCode(string code)
        {

            string query = "SELECT count(*) FROM t_Departments WHERE Code=@code";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@code", code);

            ConnectionObj.Open();

            int count = (int)CommandObj.ExecuteScalar();




            ConnectionObj.Close();

            return count;





        }

        public int GetDepartmentByName(string name)
        {

            string query = "SELECT count(*) FROM t_Departments WHERE Name=@name";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("@name", name);
            Department department = null;
            ConnectionObj.Open();

            int count = (int)CommandObj.ExecuteScalar();


            ConnectionObj.Close();

            return count;




        }
    }
}