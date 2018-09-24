using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class RoomManager
    {
        RoomGateway roomGateway = new RoomGateway();
        public List<Room> GetAllRooms()
        {
            return roomGateway.GetAllRooms();
        }
    }
}