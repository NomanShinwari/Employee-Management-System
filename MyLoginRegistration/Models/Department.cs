using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyLoginRegistration.Models
{
    public class Department
    {
        [Key]
        public int UserID { get; set; }
        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }
    }


}