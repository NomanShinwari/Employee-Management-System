using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using MyLoginRegistration.Models;
using MyLoginRegistration.Security;
using MyLoginRegistration.Services;
using NLog;

namespace MyLoginRegistration.Controllers
{
    public class EmpAPIController : ApiController
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private EmployeeService empService = new EmployeeService();

       
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Employee/GetEmployee")]

        public IHttpActionResult GetEmployee()
        {
           var Employees = empService.GetAllEmployees();
            return Ok(Employees);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Employee/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var emp = empService.GetById(id);
            return Ok(emp);
        }

        [JwtAuthorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Create")]
        public IHttpActionResult Create(UserAccount Emp)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            empService.Create(Emp);
            return Ok();
        }

        [RoleAuthorize("Admin")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Edit")]
        public IHttpActionResult Edit(UserAccount Emp)
        {
            // Log an information message
            Logger.Info("The Update Employee Method was Requested");

            try
            {
                empService.Update(Emp);
            }
            catch (Exception ex)
            {
                // Log errors and exceptions
                Logger.Error(ex, "An error occurred while updating employee.");
            }
            return Ok();
        }

        [RoleAuthorize("Admin")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Employee/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            empService.SoftDelete(id);
            return Ok();
        }

    }
}
