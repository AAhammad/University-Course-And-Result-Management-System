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
        public IEnumerable<Teacher> GetAll()
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
                            CreditTobeTaken = Convert.ToDouble(reader["CreditToBeTaken"].ToString()),
                            CreditTaken = Convert.ToDouble(reader["CreditTaken"].ToString())


                        };
                        teachers.Add(teacher);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    
                    return teachers;
                
               
                
                    
                
            
        }

        public Teacher GetTeacherByEmailAddress(string email)
        {
           
                string query = "SELECT * FROM Teacher_tbl WHERE Email=@email";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@email", email);
                Teacher teacher = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    teacher = new Teacher
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Address = reader["Address"].ToString(),
                        Email = reader["Email"].ToString(),
                        Contact = reader["Contact"].ToString(),
                        DesignationId = Convert.ToInt32(reader["DesignationId"].ToString()),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CreditTobeTaken = Convert.ToDouble(reader["CreditToBeTaken"].ToString())

                    };

                }
                reader.Close();

                ConnectionObj.Close();
                CommandObj.Dispose();
                return teacher;
            
           
            
            
        }

        public int Insert(Teacher teacher)
        {
            
                CommandObj.CommandText = "spAddTeacher";
                CommandObj.CommandType = CommandType.StoredProcedure;
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

                ConnectionObj.Close();
                CommandObj.Dispose();
                return CommandObj.ExecuteNonQuery();

            
            

            
            
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