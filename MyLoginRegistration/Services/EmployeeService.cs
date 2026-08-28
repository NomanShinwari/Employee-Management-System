using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLoginRegistration.Models;
using NLog;
namespace MyLoginRegistration.Services
{
    public class EmployeeService
    {

        private OurDbContext db = new OurDbContext();

        public List<UserAccount> GetAllEmployees()
        {
            return db.UserAccount
                .Where(u => u.Role == "Employee" && u.IsActive == true)
                .ToList();
        }

        public UserAccount GetById(int id)
        {
            return db.UserAccount.FirstOrDefault(u => u.UserID == id);
        }

        public void Create(UserAccount emp)
        {
            emp.Role = "Employee";
            emp.IsActive = true;

            db.Database.ExecuteSqlCommand(
            "EXEC sp_AddEmployee @p0,@p1,@p2,@p3,@p4,@p5,@p6",
            emp.FirstName,
            emp.LastName,
            emp.Email,
            emp.Username,
            emp.Password,
            emp.ConfirmPassword,
            emp.DepartmentId
            );
        }

        public void Update(UserAccount emp)
        {

            var existing = db.UserAccount.FirstOrDefault(u => u.UserID == emp.UserID);

            if (existing != null)
            {
                existing.FirstName = emp.FirstName;
                existing.LastName = emp.LastName;
                existing.Email = emp.Email;
                existing.DepartmentId = emp.DepartmentId;

                db.SaveChanges();
            }
        }

        public void SoftDelete(int id)
        {
            var emp = db.UserAccount.FirstOrDefault(u => u.UserID == id);

            if (emp != null)
            {
                emp.IsActive = false;
                int rows = db.SaveChanges();
            }
        }
    }
}