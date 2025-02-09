﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAssignment.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Extention { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
