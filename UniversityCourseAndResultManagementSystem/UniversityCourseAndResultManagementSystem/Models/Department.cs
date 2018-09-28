using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [Required(ErrorMessage = "Please enter Department code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be contains between two(2) and seven(7) characters long")]
        [Remote("CheckingExistingCode", "Department", HttpMethod = "POST", ErrorMessage = "Code Already Exists")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Please enter Department name")]
        [Remote("CheckingExistingName", "Department", HttpMethod = "POST", ErrorMessage = "Name Already Exists")]
        [StringLength(199, MinimumLength = 0, ErrorMessage = "Name must be less than 200 characters long")]
        public string Name { get; set; }
    }
}