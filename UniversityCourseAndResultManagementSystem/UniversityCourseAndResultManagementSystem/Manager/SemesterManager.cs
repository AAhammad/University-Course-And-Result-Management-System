using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class SemesterManager
    {
        SemesterGateway semesterGateway = new SemesterGateway();
        public List<Semester> GetAllSemesters()
        {
            return semesterGateway.GetAllSemester();
        }
    }
}