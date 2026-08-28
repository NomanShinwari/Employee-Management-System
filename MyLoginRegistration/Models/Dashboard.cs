using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyLoginRegistration.Models
{
    public class Dashboard
    {
        public int TotalUsers { get; set; }

        public int ActiveUsers { get; set; }

        public int NewRegistrations { get; set; }

        public int TotalDepartments { get; set; }

        public int TotalEmployees { get; set; }
    }
}