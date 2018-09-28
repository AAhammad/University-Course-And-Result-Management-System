using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseAndResultManagementSystem.Models;

namespace UniversityCourseAndResultManagementSystem.Gateway
{
    public class StudentGateway:DatabaseGateway
    {
        public List<Student> GetAllStudents()
        {
            
                    string query = "SELECT * FROM Student_tbl";
                    CommandObj.CommandText = query;
                    List<Student> students = new List<Student>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
            while (reader.Read())
            {
                Student student = new Student
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    RegNo = reader["RegNo"].ToString(),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    Contact = reader["ContactNo"].ToString(),
                    RegDate = Convert.ToDateTime(reader["RegisterationDate"].ToString()),
                    DepartmentId = Convert.ToInt32(reader["DepartmentId"].ToString())
                };
                students.Add(student);
            }
              reader.Close();
              CommandObj.Dispose();
              ConnectionObj.Close();
              return students;
               
                  
                
            }


        

        public string GetLastAddedStudentRegistration(string searchKey)
        {
            string query = "SELECT * FROM Student_tbl st WHERE RegNo LIKE '%@SearchKey%' and Id=(select Max(Id) FROM Student_tbl st WHERE RegNo LIKE '%@SearchKey%' )";
            CommandObj.CommandText = query;
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("SearchKey", searchKey);
            ConnectionObj.Open();
            Student aStudent = null;
            string regNo = null;
            SqlDataReader reader = CommandObj.ExecuteReader();
            if (reader.Read())
            {
                aStudent = new Student
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    RegNo = reader["RegNo"].ToString(),
                    Email = reader["Email"].ToString(),
                    Contact = reader["ContactNo"].ToString(),

                };
                regNo = aStudent.RegNo;
            }

            ConnectionObj.Close();
            CommandObj.Dispose();
            reader.Close();
            return regNo;
        }

        public int SaveStudent(Student aStudent)
        {


            string query = "INSERT INTO Student_tbl (RegNo,Name,Email,ContactNo,RegisterationDate,Address,DepartmentId) VALUES(@RegNo,@Name,@Email,@ContactNo,@RegisterationDate,@Address,@DepartmentId)";
                CommandObj.CommandText = query;

                CommandObj.Parameters.Clear();

                CommandObj.Parameters.AddWithValue("@RegNo", aStudent.RegNo);
                CommandObj.Parameters.AddWithValue("@Name", aStudent.Name);
                CommandObj.Parameters.AddWithValue("@Email", aStudent.Email.ToLower());
                CommandObj.Parameters.AddWithValue("@ContactNo", aStudent.Contact);
                CommandObj.Parameters.AddWithValue("@RegisterationDate", aStudent.RegDate.ToShortDateString());
                CommandObj.Parameters.AddWithValue("@Address", aStudent.Address);
                CommandObj.Parameters.AddWithValue("@DepartmentId", aStudent.DepartmentId);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                CommandObj.Dispose();
                ConnectionObj.Close();
                return rowAffected;
            
               
            
        }

        public StudentViewModel GetStudentInformationById(int id)
        {

                CommandObj.CommandText = "SELECT s.Id,s.RegNo,s.Name,s.Email,s.ContactNo,s.RegisterationDate,s.Address,d.Name as Department FROM Student_tbl s INNER JOIN Department_tbl d ON s.DepartmentId=d.Id AND s.Id=@Id";
               
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@Id", id);
                StudentViewModel student = null;
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                if (reader.Read())
                {
                    student = new StudentViewModel
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        RegNo = reader["RegNo"].ToString(),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        ContactNo = reader["ContactNo"].ToString(),
                        RegisterationDate = Convert.ToDateTime(reader["RegisterationDate"].ToString()),
                        Address = reader["Address"].ToString(),
                        Department = reader["Department"].ToString()
                    };
                }

                reader.Close();
                CommandObj.Dispose();
                ConnectionObj.Close();
                return student;
           
               
            
        }

        public int EnrollStudentInACourse(EnrollStudentInCourse enrollStudent)
        {
            
                CommandObj.CommandText = "INSERT INTO StudentEnrollInCourse_tbl (StudentId,CourseId,EnrollDate,IsStudentActive) VALUES(@stId,@courseId,@enrollDate,@status)";
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@stId", enrollStudent.StudentId);
                CommandObj.Parameters.AddWithValue("@courseId", enrollStudent.CourseId);
                CommandObj.Parameters.AddWithValue("@enrollDate", enrollStudent.EnrollDate.ToShortDateString());
                CommandObj.Parameters.AddWithValue("@status", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
            
                CommandObj.Dispose();
            ConnectionObj.Close();
                return rowAffected;
        

        }

        public List<EnrollStudentInCourse> GetEnrollCourses()
        {
           
                    CommandObj.CommandText = "SELECT * FROM StudentEnrollInCourse_tbl";
                    List<EnrollStudentInCourse> enrollStudentInCourses = new List<EnrollStudentInCourse>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        EnrollStudentInCourse enrollStudentInCourse = new EnrollStudentInCourse
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            EnrollDate = Convert.ToDateTime(reader["EnrollDate"].ToString()),
                            Status = Convert.ToBoolean(reader["IsStudentActive"].ToString())

                        };
                        enrollStudentInCourses.Add(enrollStudentInCourse);
                    }
                    reader.Close();
                    ConnectionObj.Close();
                    CommandObj.Dispose();
                    return enrollStudentInCourses;
               
                    
                
            }
        

        public int SaveStudentResult(StudentResult studentResult)
        {
         
                CommandObj.CommandText = "INSERT INTO StudentResult_tbl (StudentId,CourseId,Grade,IsStudentActive) VALUES(@stId,@courseId,@grade,@isStudentActive)";
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@stId", studentResult.StudentId);
                CommandObj.Parameters.AddWithValue("@courseId", studentResult.CourseId);
                CommandObj.Parameters.AddWithValue("@grade", studentResult.Grade);
                CommandObj.Parameters.AddWithValue("@isStudentActive", 1);
                ConnectionObj.Open();
                int rowAffected = CommandObj.ExecuteNonQuery();
                ConnectionObj.Close();
                CommandObj.Dispose();
                return rowAffected;
          
          
               
            
        }

        public List<StudentResult> GetAllStudentResults()
        {
            
                    CommandObj.CommandText = "SELECT * FROM StudentResult_tbl";
                    List<StudentResult> studentResults = new List<StudentResult>();
                    ConnectionObj.Open();
                    SqlDataReader reader = CommandObj.ExecuteReader();
                    while (reader.Read())
                    {
                        StudentResult studentResult = new StudentResult
                        {
                            Id = Convert.ToInt32(reader["Id"].ToString()),
                            CourseId = Convert.ToInt32(reader["CourseId"].ToString()),
                            StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                            Grade = reader["Grade"].ToString(),
                            Status = (bool)reader["IsStudentActive"]
                        };
                        studentResults.Add(studentResult);
                    }
                    reader.Close();
                    CommandObj.Dispose();
                    ConnectionObj.Close();
                    return studentResults;
                  
                
            
        }

        public List<StudentResultViewModel> GetStudentResultByStudentId(int id)
        {
            
                List<StudentResultViewModel> studentResults = new List<StudentResultViewModel>();
                CommandObj.CommandText = "SELECT en.StudentId, c.Code,c.Name,COALESCE(r.Grade,'Not Graded yet') as Grade FROM StudentResult_tbl r RIGHT OUTER JOIN ( SELECT e.Id,e.StudentId,e.CourseId FROM StudentEnrollInCourse_tbl e WHERE e.StudentId=@studentId AND e.IsStudentActive=1) en ON r.CourseId=en.CourseId AND r.StudentId=en.StudentId AND r.IsStudentActive=1 INNER JOIN Course_tbl c ON en.CourseId=c.Id";
             
                CommandObj.Parameters.Clear();
                CommandObj.Parameters.AddWithValue("@studentId", id);
                ConnectionObj.Open();
                SqlDataReader reader = CommandObj.ExecuteReader();
                while (reader.Read())
                {
                    StudentResultViewModel studentResult = new StudentResultViewModel
                    {
                        StudentId = Convert.ToInt32(reader["StudentId"].ToString()),
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        Grade = reader["Grade"].ToString()
                    };
                    studentResults.Add(studentResult);
                }
                reader.Close();
                CommandObj.Dispose();
                ConnectionObj.Close();
            
                return studentResults;
           
            
            
        }

        public int UpdateStudentEnrollInCourseIsActive(EnrollStudentInCourse enrollStudentInCourse)
        {
            ConnectionObj.Open();


            //CommandObj.CommandText = "UPDATE StudentEnrollInCourse_tbl SET IsStudentActive=1 WHERE StudentId='" + enrollStudentInCourse.StudentId + "' AND CourseId='" + enrollStudentInCourse.CourseId + "'";
            CommandObj.CommandText = "UPDATE StudentEnrollInCourse_tbl SET IsStudentActive=1 WHERE StudentId=@StudentId AND CourseId=@CourseId";
            CommandObj.Parameters.Clear();

            CommandObj.Parameters.AddWithValue("StudentId",enrollStudentInCourse.StudentId );
            CommandObj.Parameters.AddWithValue("CourseId",enrollStudentInCourse.CourseId );
                int updateResult = CommandObj.ExecuteNonQuery();
       
            // int updateResult = UpdateStudentEnrolledCourses(enrollStudentInCourse);
                CommandObj.Dispose();
                ConnectionObj.Close();
                return updateResult;
            
            
              
            
        }



        public int UpdateStudentResult(StudentResult studentResult)
        {
            //CommandObj.CommandText = "UPDATE StudentResult_tbl SET IsStudentActive=1,Grade='" + studentResult.Grade + "' WHERE StudentId='" +
            //                         studentResult.StudentId + "' AND CourseId='" + studentResult.CourseId + "'";

            CommandObj.CommandText = "UPDATE StudentResult_tbl SET IsStudentActive=1,Grade=@Grade WHERE StudentId=@StudentId AND CourseId=@CourseId";
            CommandObj.Parameters.Clear();
            CommandObj.Parameters.AddWithValue("Grade", studentResult.Grade);
            CommandObj.Parameters.AddWithValue("StudentId", studentResult.StudentId);
            CommandObj.Parameters.AddWithValue("CourseId", studentResult.CourseId);

            ConnectionObj.Open();


            int i = CommandObj.ExecuteNonQuery();
            ConnectionObj.Close();
            return i;
        }
    }
}