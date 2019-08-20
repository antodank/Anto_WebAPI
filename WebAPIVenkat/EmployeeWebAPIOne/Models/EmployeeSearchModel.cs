using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebAPIOne.Models
{
    public class EmployeeSearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Salary { get; set; }
    }
}