using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class DesignationGateway:DatabaseGateway
    {
        public List<Designation> GetAllDesignations()
        {
 
                   
                    string query = "SELECT * FROM Designation_tbl";
                    CommandObj.CommandText = query;
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    List<Designation> desingantionlist = new List<Designation>();
                    while (reader.Read())
                    {
                        Designation designation = new Designation
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Title = reader["Title"].ToString()
                        };
                        desingantionlist.Add(designation);
                    }

                    reader.Close();
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                    return desingantionlist;
          
            
        }
    }
}