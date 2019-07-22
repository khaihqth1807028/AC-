using System;

namespace HelloCSharp.entity
{
    public class Student
    {
        
        public Student(string rollNumber, string fullName)
        {
            RollNumber = rollNumber;
            FullName = fullName;
        }
    
        public Student()
        {
        }

        public string RollNumber { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public override string ToString()
        {
            return $"Rollnumber: {RollNumber}, name: {FullName}, email: {Email}, address: {Phone}";            
        }
    }
}