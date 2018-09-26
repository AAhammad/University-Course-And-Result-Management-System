using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class ClassRoomGateway:DatabaseGateway
    {
        public int InsertClassRoom(ClassRoom room)
        {
            
                string query =
                    "INSERT INTO AllocateClassRoom_tbl (DepartmentId,CourseId,RoomId,DayId,StartTime,EndTime,AllocationStatus) VALUES(@deptId,@courseId,@roomId,@dayId,@StartTime,@endTime,@allocationStatus)";
                CommandObj.CommandText = query;
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@deptId", room.DepartmentId);
                CommandObj.Parameters.AddWithValue("@courseId", room.CourseId);
                CommandObj.Parameters.AddWithValue("@roomId", room.RoomId);
                CommandObj.Parameters.AddWithValue("@dayId", room.DayId);
                CommandObj.Parameters.AddWithValue("@startTime", room.StartTime.ToShortTimeString());
                CommandObj.Parameters.AddWithValue("@endTime", room.Endtime.ToShortTimeString());
                CommandObj.Parameters.AddWithValue("@allocationStatus", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                CommandObj.Dispose();
                ConnectionObj.Close();
                return rowAffected;
            
           
               
            

        }

       


        public List<AllocateClassSchedule> GetAllAllocateClassSchedules()
        {
          
                    List<AllocateClassSchedule> scheduleList = new List<AllocateClassSchedule>();
                    CommandObj.CommandText = "SELECT * FROM ScheduleOfClassView";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        AllocateClassSchedule schedule = new AllocateClassSchedule
                        {

                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            CourseCode = reader["Code"].ToString(),
                            CourseName = reader["Name"].ToString(),
                            RoomName = reader["Room_Name"].ToString(),
                            DayName = reader["Day_Name"].ToString(),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            EndTime = Convert.ToDateTime(reader["EndTime"].ToString())
                        };
                        scheduleList.Add(schedule);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                    return scheduleList;
              
                   
                

            
        }



      

        public List<ClassRoom> GetClassSchedulByStartAndEndingTime(int roomId, int dayId, DateTime startTime, DateTime endTime)
        {
            
             
                CommandObj.CommandText = "Select * from AllocateClassRoom_tbl Where DayId=@DayId AND RoomId=@RoomId  AND AllocationStatus=1";

            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("RoomId", roomId);
            CommandObj.Parameters.AddWithValue("DayId", dayId);
                List<ClassRoom> tempClassSchedules = new List<ClassRoom>();
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    ClassRoom temp = new ClassRoom
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                        RoomId = Convert.ToInt32(reader["RoomId"].ToString()),
                        DayId = Convert.ToInt32(reader["DayId"].ToString()),
                        StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                        Endtime = Convert.ToDateTime(reader["EndTime"].ToString())

                    };
                    tempClassSchedules.Add(temp);
                }
                reader.Close();
                CommandObj.Dispose();
                ConnectionObj.Close();
                return tempClassSchedules;
            
           
                
            

        }

        public List<AllocateClassSchedule> GetAllClassSchedulesByDeparmentId(int departmentId, int courseId)
        {
           
                List<AllocateClassSchedule> scheduleList = new List<AllocateClassSchedule>();
                CommandObj.CommandText = "SELECT * FROM ScheduleOfClassView WHERE DepartmentId= @DepartmentId  AND CourseId=@CourseId  AND AllocationStatus= 1 ";
               CommandObj.Parameters.Clear();
               CommandObj.Parameters.AddWithValue("DepartmentId", departmentId);
               CommandObj.Parameters.AddWithValue("CourseId", courseId);
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    AllocateClassSchedule schedule = new AllocateClassSchedule
                    {

                        DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                        CourseCode = reader["Code"].ToString(),
                        CourseName = reader["Name"].ToString(),
                        RoomName = reader["Room_Name"].ToString(),
                        DayName = reader["Day_Name"].ToString(),
                        StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                        EndTime = Convert.ToDateTime(reader["EndTime"].ToString()),
                        Status = Convert.ToBoolean(reader["AllocationStatus"])
                    };

                    scheduleList.Add(schedule);
                }

                reader.Close();
                
                ConnectionObj.Close();
                CommandObj.Dispose();
                return scheduleList;
           
               
            

        }

        public List<ClassRoom> GetAllClassroomInformation()
        {
           
                    List<ClassRoom> scheduleList = new List<ClassRoom>();
                    CommandObj.CommandText = "SELECT * FROM AllocateClassRoom_tbl";
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        ClassRoom classRoom = new ClassRoom
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString()),
                            DayId = Convert.ToInt32(reader["DayId"].ToString()),
                            RoomId = Convert.ToInt32(reader["RoomId"].ToString()),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            Endtime = Convert.ToDateTime(reader["EndTime"].ToString()),

                            AlloctionStaus = Convert.ToBoolean(reader["AllocationStatus"].ToString())
                        };
                        scheduleList.Add(classRoom);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                    return scheduleList;
               

            
        }

        public int UnAllocateClassRoom()
        {
            ConnectionObj.Open();
           
                CommandObj.CommandText = "UPDATE AllocateClassRoom_tbl SET AllocationStatus=0";
               
                int i = CommandObj.ExecuteNonQuery();

              
          
                CommandObj.Dispose();
                ConnectionObj.Close();
            return i;

        }
    }
}