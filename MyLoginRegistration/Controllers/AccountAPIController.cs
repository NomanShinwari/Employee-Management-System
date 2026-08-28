using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Mvc;
using MyLoginRegistration.Common;
using MyLoginRegistration.Models;
using MyLoginRegistration.Security;
using MyLoginRegistration.Services;

namespace MyLoginRegistration.Controllers
{
    
    public class AccountAPIController : ApiController
    {
        private UserService usrservice = new UserService();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/Login")]
        public IHttpActionResult Login(UserAccount user)
        {
            var loggedInUser = usrservice.Login(user);

            if (loggedInUser == null)
            {
                return Unauthorized();
            }

            var token = Security.JwtService.GenerateToken(loggedInUser);
            return Ok(new { Token = token });
        }

        [JwtAuthorize(Roles = "Admin")]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Account/GetAdmin")]
        public IHttpActionResult GetAdmin()
        {
            if (!User.IsInRole("Admin"))
            {
                return Unauthorized();
            }

            var UserAdmin = usrservice.GetAdmins();

            var identity = (ClaimsIdentity)User.Identity;

            var userId = identity.FindFirst("UserID")?.Value;
            var username = identity.FindFirst(ClaimTypes.Name)?.Value;
            var role = identity.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(UserAdmin);

        }

        [JwtAuthorize(Roles = "Admin")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/Create")]
        public IHttpActionResult Create(UserAccount user)
        {
            if (!User.IsInRole("Admin"))
            {
                // Return a meaningful response for unauthorized access
                return Unauthorized();
            }
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            usrservice.Create(user);
            return Ok("User created successfully.");
        }

        [RoleAuthorize("Admin")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/Edit")]
        public IHttpActionResult Edit(UserAccount user)
        {
            if (!User.IsInRole("Admin"))
            {
                // Return a meaningful response for unauthorized access
                return Unauthorized();
            }
            if (user == null)
            {
                return BadRequest("User data is null.");
            }
            usrservice.Edit(user);
            return Ok();
        }

        [RoleAuthorize("Admin")]
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/Delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            usrservice.SoftDelete(id);
            return Ok();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Dashboard/GetDashboardInfo")]
        public IHttpActionResult GetDashboardInfo()
        {
            var dashboardData = usrservice.GetDashboardData();

            if (dashboardData == null)
            {
                var errorResponce = ApiResponse<Dashboard>.Fail("Failed to retrieve dashboard data.");

                return Content(HttpStatusCode.NotFound, errorResponce);
            }
            var successresponce = ApiResponse<Dashboard>.Success(dashboardData, "Data Retrieved Successfully.");
            return Ok(successresponce);
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Account/GoogleLogin")]
        public IHttpActionResult GoogleLogin(UserAccount user)
        {
            var loggedInUser = usrservice.GoogleLogin(user);

            if (loggedInUser == null)
            {
                return Unauthorized();
            }

            var token = JwtService.GenerateToken(loggedInUser);

            return Ok(new { Token = token });
        }
    }
}
