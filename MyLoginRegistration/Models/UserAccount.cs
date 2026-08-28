using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MyLoginRegistration.Models
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is Required.")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please Confirm Your Password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Please Select Role")]
        public String Role { get; set; }

        //[Required(ErrorMessage = "Please Select The Department")]
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; }

        // Google Authentication
        public string GoogleId { get; set; }

        public string LoginProvider { get; set; }

    }
}