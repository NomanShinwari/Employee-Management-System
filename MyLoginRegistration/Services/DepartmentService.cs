using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyLoginRegistration.Models;

namespace MyLoginRegistration.Services
{
    public class DepartmentService
    {
        private OurDbContext db = new OurDbContext();

          public List<Department> GetAll()
           {
               return db.Department.Where(d => d.IsActive == true).ToList();
           }
        
        public Department GetById(int id)
        {
            return db.Department.FirstOrDefault(d => d.UserID == id);
        }

        public void Create(Department dept)
        {
            dept.IsActive = true;
            db.Department.Add(dept);
            db.SaveChanges();
        }

        public void Update(Department dept)
        {
            var existing = db.Department.FirstOrDefault(d => d.UserID == dept.UserID);

            if (existing != null)
            {
                existing.DepartmentName = dept.DepartmentName;
                existing.IsActive = dept.IsActive;
                db.SaveChanges();
            }
        }

        public void SoftDelete(int id)
        {
            var dept = db.Department.FirstOrDefault(d => d.UserID == id);
            if (dept != null)
            {
                dept.IsActive = false;
                db.SaveChanges();
            }
        }
    }
}