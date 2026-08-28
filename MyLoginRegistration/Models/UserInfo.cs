using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MyLoginRegistration.Models
{
    public class UserInfo
    {
        public int UserID { get; set; }

        public string FirstName { get; set; }

        public String LastName { get; set; }

        public string Email { get; set; }
    }
}