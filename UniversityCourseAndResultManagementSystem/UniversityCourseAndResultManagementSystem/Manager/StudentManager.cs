using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Gateway;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Manager
{
    public class StudentManager
    {
        StudentGateway studentGateway = new StudentGateway();
        DepartmentGateway departmentGateway = new DepartmentGateway();
        public List<Student> GetAllStudents()
        {
            return studentGateway.GetAllStudents();
        }

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            return studentGateway.GetLastAddedStudentRegistration(searchKey);

        }

        public string Save(Student aStudent)
        {
            int counter;
            Department department = departmentGateway.GetAllDepts().Single(depid => depid.DepartmentId == aStudent.DepartmentId);
            string searchKey = department.Code + "-" + aStudent.RegDate.Year + "-";
            string lastAddedRegistrationNo = GetLastAddedStudentRegistration(searchKey);
            if (lastAddedRegistrationNo == null)
            {
                aStudent.RegNo = searchKey + "001";

            }

            if (lastAddedRegistrationNo != null)
            {
                string tempId = lastAddedRegistrationNo.Substring((lastAddedRegistrationNo.Length - 3), 3);
                counter = Convert.ToInt32(tempId);
                string studentSl = (counter + 1).ToString();


                if (studentSl.Length == 1)
                {

                    aStudent.RegNo = searchKey + "00" + studentSl;

                }
                else if (studentSl.Count() == 2)
                {

                    aStudent.RegNo = searchKey + "0" + studentSl;
                }
                else
                {

                    aStudent.RegNo = searchKey + studentSl;
                }

            }
          

            if (studentGateway.GetAllStudents().Any(x => x.Email.Equals(aStudent.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return "Email address already exist";
            }
            if (IsEmailAddressValid(aStudent.Email))
            {
                if (studentGateway.SaveStudent(aStudent) > 0)
                {
                    return "Registration Done!,Name: " + aStudent.Name + ",Email: " + aStudent.Email + ",Contact Number: " + aStudent.Contact+",Date: "+aStudent.RegDate.ToShortDateString()+",Address: "+aStudent.Address+",Registration No: " + aStudent.RegNo ;
                }

                return "Failed to save";
            }
            return "Please enter a valid email address";
        }

        private bool IsEmailAddressValid(string email)
        {
            if (email.Contains(".") && (email.Contains("@") ))
            {
                return true;
            }
            return false;

        }

        public StudentViewModel GetStudentInformationById(int id)
        {
            return studentGateway.GetStudentInformationById(id);
        }

        public string Save(EnrollStudentInCourse enrollStudentInCourse)
        {
            EnrollStudentInCourse enrollStudent =
                GetEnrollCourses().ToList().Find(s =>(s.StudentId == enrollStudentInCourse.StudentId && s.CourseId == enrollStudentInCourse.CourseId) && (s.Status));
            if (enrollStudent == null)
            {
                if (studentGateway.EnrollStudentInACourse(enrollStudentInCourse) > 0)
                {
                    return "Saved Successfully";
                }
                return "Failed to save";
            }

            return "Already Enroll in this course";
        }

        public List<EnrollStudentInCourse> GetEnrollCourses()
        {
            return studentGateway.GetEnrollCourses();
        }

        public string Save(StudentResult studentResult)
        {
            StudentResult result =
                GetAllStudentResults().ToList()
                    .Find(s => s.StudentId == studentResult.StudentId && s.CourseId == studentResult.CourseId && s.Status);
            if (result == null)
            {
                if (studentGateway.SaveStudentResult(studentResult) > 0)
                {
                    return "Saved sucessfully";
                }
                return "Failed to save";

            }
           
            if (result!=null)
            {
                if (studentGateway.UpdateStudentResult(studentResult) > 0)
                {
                    return "update sucessfully";
                }

                return "not updated";
            }
            return "Failed to save";
        }

     

        public IEnumerable<StudentResult> GetAllStudentResults()
        {
            return studentGateway.GetAllStudentResults();

        }

        public List<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            return studentGateway.GetStudentResultByStudentId(id);
        }

    }
}