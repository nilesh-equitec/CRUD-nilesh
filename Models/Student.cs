using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperMVC.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime DateOfBrith { get; set; }

        public int Salary { get; set; }
    }
}