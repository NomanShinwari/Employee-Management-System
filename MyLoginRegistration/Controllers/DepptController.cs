using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using MyLoginRegistration.Models;
using MyLoginRegistration.Security;
using MyLoginRegistration.Services;

namespace MyLoginRegistration.Controllers
{
    public class DepptController : ApiController
    {
        private DepartmentService deptService = new DepartmentService();

        [HttpGet]
        [Route("api/Deppt/GetDeptt")]
        public IHttpActionResult GetDeptt()
        {
            var departments = deptService.GetAll();
            return Ok(departments);
        }

        [RoleAuthorize("Admin")]
        [HttpPost]
        [Route("api/Deppt/Create")]
        public IHttpActionResult Create(Department deptt)
        {
            if(!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }
            deptService.Create(deptt);
            return Ok();
        }

        [RoleAuthorize("Admin")]
        [System.Web.Http.HttpPost]
        [Route("api/Deppt/Edit")]
        public IHttpActionResult Edit(Department dept)
        {
            deptService.Update(dept);
            return Ok();
        }

        [RoleAuthorize("Admin")]
        [HttpPost]
        [Route("api/Deppt/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            deptService.SoftDelete(id);
            return Ok();
        }


    }
}
