using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Name must be less than 100 characters long.!")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Email must be less than 100 characters long.!")]
        [Remote("CheckingExistingEmail", "Student", HttpMethod = "POST", ErrorMessage = "This Email Already Exists try another one")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact is required!")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Contact Number must be less than 50 characters long.!")]

        [DisplayName("Contact No.")]
        public string Contact { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "Please select a date")]
        //[DataType(DataType.Date,ErrorMessage = "This is not a correct date value!")]
        public DateTime RegDate { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [StringLength(499, MinimumLength = 0, ErrorMessage = "Address must be less than 100 characters long.!")]

        public string Address { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }
    }
}