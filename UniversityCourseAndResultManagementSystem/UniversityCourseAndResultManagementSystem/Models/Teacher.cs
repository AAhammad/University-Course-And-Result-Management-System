using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
       
        public string Address { get; set; }
       
        public string Email { get; set; }
       
        public string Contact { get; set; }
        
        public int DesignationId { get; set; }
       
        public int DepartmentId { get; set; }
       
        public double CreditTobeTaken { get; set; }

        public double CreditTaken { get; set; }
    }
}