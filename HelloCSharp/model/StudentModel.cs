using System;
using System.Collections.Generic;
using HelloCSharp.entity;
using MySql.Data.MySqlClient;

namespace HelloCSharp.model
{
    public class StudentModel
    {
        private const string ServerName = "localhost";
        private const string DatabaseName = "student_manager";
        private const string Username = "root";
        private const string Password = "abcD1234";

        public void Save(Student obj)
        {
            var cmd = new MySqlCommand("insert into students (rollNumber, name, email, phone) " +
                                       "values (@rollNumber, @name, @email, @phone)", ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@rollNumber", obj.RollNumber);
            cmd.Parameters.AddWithValue("@name", obj.FullName);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.ExecuteNonQuery();
            ConnectionHelper.CloseConnection();
            Console.WriteLine("Save object success!");
        }

        public List<Student> FindAll()
        {
            var list = new List<Student>();
            var cmd = new MySqlCommand("select * from students", ConnectionHelper.GetConnection());
            var dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                var obj = new Student
                {
                    RollNumber = dataReader.GetString("rollNumber"),
                    FullName = dataReader.GetString("name"),
                    Email = dataReader.GetString("email"),
                    Phone = dataReader.GetString("phone")
                };
                list.Add(obj);
            }
            ConnectionHelper.CloseConnection();
            return list;
        }

        public Student FindById(string id)
        {
            var cmd = new MySqlCommand("select * from students where rollNumber = @rollNumber",
                ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@rollNumber", id);
            var dataReader = cmd.ExecuteReader();
            if (!dataReader.Read())
            {
                return null;
            }

            var obj = new Student
            {
                RollNumber = dataReader.GetString("rollNumber"),
                FullName = dataReader.GetString("name"),
                Email = dataReader.GetString("email"),
                Phone = dataReader.GetString("phone")
            };
            ConnectionHelper.CloseConnection();
            return obj;
        }

        public void Update(Student obj)
        {
            var cmd = new MySqlCommand(
                "update students set name = @name, email = @email, phone = @phone where rollNumber = @rollNumber",
                ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@rollNumber", obj.RollNumber);
            cmd.Parameters.AddWithValue("@name", obj.FullName);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.ExecuteNonQuery();
            ConnectionHelper.CloseConnection();
        }

        public void Delete(string id)
        {
            var cmd = new MySqlCommand("delete from students where rollNumber=@rollNumber",
                ConnectionHelper.GetConnection());
            cmd.Parameters.AddWithValue("@rollNumber", id);
            cmd.ExecuteNonQuery();
            ConnectionHelper.CloseConnection();
            Console.WriteLine("Delete object success!");
        }
    }
}