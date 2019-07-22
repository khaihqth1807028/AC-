using System;
using System.Collections.Generic;
using System.Globalization;
using HelloCSharp.entity;
using HelloCSharp.model;

namespace HelloCSharp.controller
{
    public class StudentController
    {
        private static StudentModel studentModel;

        public StudentController()
        {
            if (studentModel == null)
            {
                studentModel = new StudentModel();
            }
        }

        public void CreateStudent()
        {
            Console.Clear();
            Console.WriteLine("==================CREATE NEW STUDENT=================");
            var student = new Student();
            Console.WriteLine("Please enter student information.");
            Console.WriteLine("Rollnumber: ");
            student.RollNumber = Console.ReadLine();
            Console.WriteLine("Email: ");
            student.Email = Console.ReadLine();
            Console.WriteLine("Fullname: ");
            student.FullName = Console.ReadLine();
            Console.WriteLine("Phone: ");
            student.Phone = Console.ReadLine();
            Console.WriteLine(student.ToString());
            studentModel.Save(student);
        }

        public void ShowList()
        {
            Console.Clear();
            Console.WriteLine("==================SHOW LIST STUDENT=================");
            var list = studentModel.FindAll();
            if (list.Count == 0)
            {
                Console.WriteLine("Have no student. Please create new one!");
                return;
            }

            Console.WriteLine("{0,-20} {1,-20} {2,-30} {3,-20}", "Rollnumber", "Fullname", "Email", "Phone");
            foreach (var student in list)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-30} {3,-20}", student.RollNumber, student.FullName,
                    student.Email, student.Phone);
            }
        }

        public void UpdateStudent()
        {
            Console.Clear();
            Console.WriteLine("==================UPDATE STUDENT=================");
            Console.WriteLine("Please enter student rollnumber to update:");
            var rollNumber = Console.ReadLine();
            var student
                = studentModel.FindById(rollNumber);
            if (student == null)
            {
                Console.WriteLine("Student is not found. Please try again.");
                return;
            }

            Console.WriteLine("Please enter student information.");
            Console.WriteLine("Email: ");
            student.Email = Console.ReadLine();
            Console.WriteLine("Fullname: ");
            student.FullName = Console.ReadLine();
            Console.WriteLine("Phone: ");
            student.Phone = Console.ReadLine();
            Console.WriteLine(student.ToString());
            studentModel.Update(student);
        }

        public void DeleteStudent()
        {
            Console.Clear();
            Console.WriteLine("==================REMOVE STUDENT=================");
            Console.WriteLine("Please enter student rollnumber to remove:");
            var rollNumber = Console.ReadLine();
            var student = studentModel.FindById(rollNumber);
            if (student == null)
            {
                Console.WriteLine("Student is not found. Please try again.");
                return;
            }

            Console.WriteLine($"Are you sure wanna delete student with rollnumber {student.RollNumber} (y/n)");
            var choice = Console.ReadLine().ToLower();
            if (choice.Equals("n"))
            {
                return;
            }

            studentModel.Delete(rollNumber);
        }
    }
}