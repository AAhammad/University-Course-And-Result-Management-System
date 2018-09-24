using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class RoomGateway:DatabaseGateway
    {
         public List<Room> GetAllRooms()
        {
                    var roomList = new List<Room>();
                    CommandObj.CommandText = "SELECT * FROM Room_tbl";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();

                    while (reader.Read())
                    {
                        var room = new Room
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            Name = reader["Name"].ToString()
                        };
                        roomList.Add(room);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                    return roomList;
               
                   
                
            }
    }
}