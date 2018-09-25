using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Required(ErrorMessage = "Please enter Department code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be contains between two(2) and seven(7) characters long")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Department name")]
        public string Name { get; set; }
    }
}