using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class TeacherGateway:DatabaseGateway
    {
        public List<Teacher> GetAllTeachers()
        {
           
                
                    string query = "SELECT * FROM Teacher_tbl";
                    CommandObj.CommandText = query;
                    List<Teacher> teachers = new List<Teacher>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        Teacher teacher = new Teacher
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            Contact = reader["Contact"].ToString(),
                            DesignationId = Convert.ToInt32(reader["DesignationId"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            CreditTobeTaken =  Convert.ToDecimal((reader["CreditToBeTaken"].ToString())),
                            CreditTaken = Convert.ToDecimal((reader["CreditTaken"].ToString()))


                        };
                        teachers.Add(teacher);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    
                    return teachers;
                
         
            
        }

      

        public int SaveTeacher(Teacher teacher)
        {


            string query = "INSERT INTO Teacher_tbl(Name,Address,Email,Contact,DesignationId,DepartmentId,CreditToBeTaken,CreditTaken) VALUES(@Name,@Address,@Email,@Contact, @DesignationId,@DepartmentId,@CreditTobeTaken,@RemainingCredit)";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@Name", teacher.Name);
                CommandObj.Parameters.AddWithValue("@Address", teacher.Address);
                CommandObj.Parameters.AddWithValue("@Email", teacher.Email.ToLower());
                CommandObj.Parameters.AddWithValue("@Contact", teacher.Contact);
                CommandObj.Parameters.AddWithValue("@DesignationId", teacher.DesignationId);
                CommandObj.Parameters.AddWithValue("@DepartmentId", teacher.DepartmentId);
                CommandObj.Parameters.AddWithValue("@CreditTobeTaken", teacher.CreditTobeTaken);
                CommandObj.Parameters.AddWithValue("@RemainingCredit", 0);
                ConnectionObj.Open();
                int rowAffact=CommandObj.ExecuteNonQuery();
                ConnectionObj.Close();
                CommandObj.Dispose();
                 return rowAffact;



        }


        public int UpdateTeacherInformation()
        {
            ConnectionObj.Open();
            CommandObj.CommandText = "UPDATE Teacher_tbl SET CreditTaken=0";
            int i = CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return i;
        }
       
                
    }
}