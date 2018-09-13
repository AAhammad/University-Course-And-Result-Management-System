using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class DepartmentManager
    {
        private DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public List<Department> GetAllDepts()
        {
            return aDepartmentGateway.GetAllDepts();
        }


        public string SaveDept(Department aDepartment)
        {
            if (aDepartmentGateway.SaveDept(aDepartment) > 0)
            {
                return "Saved";
            }
            return "failed";
        }
    }
}