using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class DayManager
    {
        DayGateway dayGateway = new DayGateway();
        public List<Day> GetAllDays()
        {
             return dayGateway.GetAllDays();
        } 
    }
}