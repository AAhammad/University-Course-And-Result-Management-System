using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a course code")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Code must be at least five (5) characters long.!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter a course name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a Course credit!")]
        [Range(0.5, 5.0, ErrorMessage = "Credit must be between 0.5 and 5.0")]
        public decimal Credit { get; set; }

        [Required(ErrorMessage = "Please enter course description!")]
        public string Description { get; set; }
        [DisplayName("Department")]
        [Required(ErrorMessage = "Please Select a department!")]
        public int DepartmentId { get; set; }
        [DisplayName("Semester")]
        [Required(ErrorMessage = "Please Select a semester!")]
        public int SemesterId { get; set; }
    }
}