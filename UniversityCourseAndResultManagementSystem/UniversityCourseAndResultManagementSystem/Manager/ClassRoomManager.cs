using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class ClassRoomManager
    {
        ClassRoomGateway classRoomGateway = new ClassRoomGateway();

        public String Save(ClassRoom room)
        {
            if (room.StartTime > room.Endtime)
            {
                return "To time must be greater than From time )";
            }
            bool isTimeScheduleValid = IsTimeScheduleValid(room.RoomId, room.DayId, room.StartTime, room.Endtime);

            if (isTimeScheduleValid)
            {

                if (classRoomGateway.InsertClassRoom(room) > 0)
                {
                    return "Saved Successfully";
                }
                return "Failed to save";

            }
            return "Both full and partial overlapping must be avoided";
        }

        private bool IsTimeScheduleValid(int roomId, int dayId, DateTime startTime, DateTime endTime)
        {
           
            List<ClassRoom> schedule = classRoomGateway.GetClassSchedulByStartAndEndingTime(roomId, dayId, startTime, endTime);
            foreach (var aSchedule in schedule)
            {

                if ((aSchedule.DayId == dayId && roomId == aSchedule.RoomId) && (startTime < aSchedule.StartTime && endTime > aSchedule.StartTime)
                                 || (startTime < aSchedule.StartTime && endTime > aSchedule.StartTime) ||
                                 (startTime == aSchedule.StartTime) || (aSchedule.StartTime < startTime && aSchedule.Endtime > startTime))
                {
                    return false;
                }

            }
            return true;

        }

      

        public List<AllocateClassSchedule> GetAllClassSchedules()
        {
            return classRoomGateway.GetAllAllocateClassSchedules();
        }




        public string GetAllClassSchedulesByDeparmentId(int departmentId, int courseId)
        {
            List<AllocateClassSchedule> classSchedules = classRoomGateway.GetAllClassSchedulesByDeparmentId(departmentId, courseId);

            string output = "";

            foreach (var aClass in classSchedules)
            {

                if (aClass.RoomName.StartsWith("R"))
                {
                    output +="R. No : "+ aClass.RoomName + ", " + aClass.DayName + ", " + aClass.StartTime.ToShortTimeString() + " - " + aClass.EndTime.ToShortTimeString() + ";<br />";
                }

                else if (aClass.RoomName.StartsWith("N"))
                {
                    output = aClass.RoomName;

                }


            }

            return output;
        }

        public String UnAllocateClassRoom()
        {
            if (classRoomGateway.UnAllocateClassRoom() > 0)
            {
                return "UnAllocated ClassRoom Successfully";
            }
            return "Failed to Unallocate";
        }
    }
}