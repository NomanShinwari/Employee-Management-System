using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using MyLoginRegistration.Controllers;
using MyLoginRegistration.Models;

namespace MyLoginRegistration.Services
{
    public class UserService
    {
        private OurDbContext db = new OurDbContext();

        public object UserAccount { get; internal set; }

        public List<UserAccount> GetAdmins()
        {
            return db.UserAccount
                .Where(u => u.Role == "Admin" && u.IsActive == true)
                .ToList();
        }

        /*  public UserAccount getcredentials(UserAccount cred)
          {
              if (cred is null)
              {
                  throw new ArgumentNullException(nameof(cred));
              }
              var logincred = db.UserAccount.FirstOrDefault(u => u.UserID == cred.UserID && u.Role == cred.Role);

              return logincred;
          }
        */
        public UserAccount Login(UserAccount user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var existingUser = db.UserAccount.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            return existingUser;
        } 

        public void Register(UserAccount user)
        {
            user.IsActive = true;
            user.Role = "Employee";
            db.UserAccount.Add(user);
            db.SaveChanges();
        }

        public void Create(UserAccount user)
        {
            user.IsActive = true;
            user.Role = "Admin";
            db.UserAccount.Add(user);
            db.SaveChanges();
        }

        public UserAccount GetById(int id)
        {
            return db.UserAccount.FirstOrDefault(u => u.UserID == id);
        }
        public void Edit(UserAccount user)
        {
            var existing = db.UserAccount.FirstOrDefault(u => u.UserID == user.UserID);

            if (existing != null)
            {
                existing.FirstName = user.FirstName;
                existing.LastName = user.LastName;
                existing.Email = user.Email;
                db.SaveChanges();
            }
        }

        public void SoftDelete(int id)
        {
            var usr = db.UserAccount.FirstOrDefault(u => u.UserID == id);

            if (usr != null)
            {
                usr.IsActive = false;
                int rows = db.SaveChanges();
            }
        }

        public Dashboard GetDashboardData()
        {
            var dashboard = new Dashboard
            {
                TotalUsers = db.UserAccount.Count(),
                ActiveUsers = db.UserAccount.Count(u => u.IsActive),
                NewRegistrations = db.UserAccount.Count(u => u.IsActive && u.Role == "Employee"),
                TotalDepartments = db.Department.Count(),
                TotalEmployees = db.UserAccount.Count(u => u.Role == "Employee")
            };
            return dashboard;
        }

        public UserAccount GoogleLogin(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var existingUser = db.UserAccount
                    .FirstOrDefault(x => x.Email == user.Email);


                if (existingUser != null)
                {
                    return existingUser;
                }


                var newUser = new UserAccount
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                    // Google users do not need password
                    Username = user.Email,

                    Password = null,

                    Role = "Employee",

                    DepartmentId = 0,

                    IsActive = true,

                    GoogleId = user.GoogleId,

                    LoginProvider = "Google"
                };


                db.UserAccount.Add(newUser);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in error.ValidationErrors)
                        {
                            Console.WriteLine(
                                validationError.PropertyName
                                + " : " +
                                validationError.ErrorMessage
                            );
                        }
                    }

                    throw;
                }


                return newUser;
            }
        }
    }
}