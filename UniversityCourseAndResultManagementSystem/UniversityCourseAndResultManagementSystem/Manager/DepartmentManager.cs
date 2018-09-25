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
            if (!(IsDepartmentCodeValid(aDepartment)))
            {
                return "Department code must be between 2 to 7 character long";
            }
         

            if (aDepartmentGateway.GetAllDepts().Exists(x=>x.Code.Equals(aDepartment.Code,StringComparison.OrdinalIgnoreCase)))
            {
                return "Department Code Already Exists";
            }
            if (aDepartmentGateway.GetAllDepts().Exists(x => x.Name.Equals(aDepartment.Name,StringComparison.OrdinalIgnoreCase)))
            {
                return "Department Name Already Exists";
            }
            if (aDepartmentGateway.SaveDept(aDepartment) > 0)
            {
                return "Saved";
            }
            return "failed";
        }

       

        private bool IsDepartmentCodeValid(Department aDepartment)
        {
            if (aDepartment.Code.Length >= 2 || aDepartment.Code.Length <= 7)
            {
                return true;
            }
            return false;
        }
    }
}