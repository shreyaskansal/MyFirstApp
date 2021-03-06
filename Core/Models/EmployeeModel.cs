﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class EmployeeModel : User
    {
        public int ID { get; set; }
        public int DepartmentId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
        public int Year { get; set; }
        public string ProfileName { get; set; }
    }

    //public class DepartmentModel : User
    //{
    //    public int Id { set; get; }
    //    public string Name { set; get; }
    //    public int EmployeeCount { get; set; }
    //}
}
