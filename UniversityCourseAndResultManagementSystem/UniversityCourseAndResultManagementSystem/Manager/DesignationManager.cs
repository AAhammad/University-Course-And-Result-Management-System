using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class DesignationManager
    {
        DesignationGateway designationGateway = new DesignationGateway();
        public List<Designation> GetAllDesignations()
        {

            return designationGateway.GetAllDesignations(); 
        } 
    }
}