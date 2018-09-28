using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityCourseAndResultManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a teacher name!")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Name must be less than 100 characters long.!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a address!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a Email address!")]
        [EmailAddress(ErrorMessage = "Please Enter valid Email address")]
        [StringLength(100, MinimumLength = 0, ErrorMessage = "Email must be less than 100 characters long.!")]
        [Remote("CheckingExistingEmail", "Teacher", HttpMethod = "POST", ErrorMessage = "This Email Already Exists try another one")]
        public string Email { get; set; }

        [Required(ErrorMessage = "please enter a contact Number!")]
        [DisplayName("Contact No.")]
        [StringLength(20,MinimumLength = 0,ErrorMessage = "Contact must be less than 20 characters")]
        public string Contact { get; set; }

        [DisplayName("Designation")]
        public int DesignationId { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter Credit!")]
        [DisplayName("Credit To be Taken")]
        [RegularExpression("^0*[1-9][0-9]*(\\.[0-9]+)?", ErrorMessage = "Please enter a positive number! ")]
        [Range(0,10000000)]
        public decimal CreditTobeTaken { get; set; }

        public decimal CreditTaken { get; set; }
    }
}