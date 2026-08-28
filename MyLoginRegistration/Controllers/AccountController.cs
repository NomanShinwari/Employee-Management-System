using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using MyLoginRegistration.Models;
using MyLoginRegistration.Security;
using MyLoginRegistration.Services;
using System.Net.Http.Headers;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.AspNet.Identity;

namespace MyLoginRegistration.Controllers
{

    public class AccountController : Controller
    {

        HttpClient hc = new HttpClient();


        private UserService usrservice = new UserService();
        [JwtAuthorize]
        public ActionResult Index()
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["Error"] = "You are not authorized to access this page.";
                return RedirectToAction("LoggedIn", "Account");
            }
            // Retrieve the cookie by its name
            HttpCookie tokenCookie = Request.Cookies["jwtToken"];
            string token = tokenCookie != null ? tokenCookie.Value : string.Empty;

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            List<UserAccount> useradmin = new List<UserAccount>();

            hc.BaseAddress = new Uri("https://localhost:44393/api/Account/GetAdmin");

            if (!string.IsNullOrEmpty(token))
            {
                // Adds "Authorization: Bearer <token>" header
                hc.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var responce = hc.GetAsync("GetAdmin");
            responce.Wait();

            var A = responce.Result;

            if (A.IsSuccessStatusCode)
            {
                var display = A.Content.ReadAsAsync<List<UserAccount>>();
                display.Wait();
                useradmin = display.Result;
            }

            return View(useradmin);
        }


        [HttpGet]
        public ActionResult Register()
        {

            using (OurDbContext db = new OurDbContext())
            {
                ViewBag.Departments = db.Department.ToList();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {

            if (!ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    ViewBag.Departments = db.Department.ToList();
                }

                return View(account);
            }
            try 
            {
                // Optional: check for existing username/email to provide a friendly error
                using (OurDbContext db = new OurDbContext())
                {
                    var exists = db.UserAccount.Any(u => u.Username == account.Username || u.Email == account.Email);
                    if (exists)
                    {
                        ModelState.AddModelError("", "Username or email already exists.");
                        ViewBag.Departments = db.Department.ToList();
                        return View(account);
                    }
                }
                usrservice.Register(account);

                TempData["SuccessMessage"] = "Successfully Registered. Please login.";
                return RedirectToAction("Login");
            }
            catch (Exception)
            {
                // Log exception (not shown). Show friendly message.
                TempData["ErrorMessage"] = "Registration failed. Please try again.";
                using (OurDbContext db = new OurDbContext())
                {
                    ViewBag.Departments = db.Department.ToList();
                }
                return View(account);
            }
        }
       [HttpGet]
        public JsonResult IsUsernameAvailable(string username)
        {
            // Call your service layer: bool isTaken = _service.CheckUsername(username);
            bool isTaken = (username == "Agha"); // Simplified for testing

            // Return a simple object
            return Json(new { isAvailable = !isTaken }, JsonRequestBehavior.AllowGet);
        }

        [JwtAuthorize]
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["Error"] = "You are not authorized to access this page.";
                RedirectToAction("LoggedIn", "Account");
            }

            return PartialView("_createUser");
        }
        

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            var usr = usrservice.Login(user);

            var token = JwtService.GenerateToken(user);

            if (usr == null)
            {
                ModelState.AddModelError("", "Invalid User");
                return View();
            }
                  FormsAuthentication.SetAuthCookie(usr.Username, true);
                  Session["UserID"] = usr.UserID.ToString();
                  Session["Role"] = usr.Role.ToString();

            return Content(token);

        }

        [JwtAuthorize]
        public ActionResult LoggedIn()
        {

            var identity = (ClaimsIdentity)User.Identity;

            var userId = identity.FindFirst("UserID")?.Value;
            var username = User.Identity.Name;
            var role = User.IsInRole("Admin");
            return View();

        }
        public ActionResult Logout()
        {
            if (Request.Cookies["jwtToken"] != null)
            {
                var cookie = new HttpCookie("jwtToken");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            HttpContext.GetOwinContext().Authentication.SignOut();

            return RedirectToAction("Login", "Account");
        }

        [JwtAuthorize]
        public ActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["Error"] = "You are not authorized to access this page.";
                RedirectToAction("LoggedIn", "Account");
            }
            var user = usrservice.GetById(id);
            return PartialView("_EditUser", user);
        }

        public ActionResult TestElmah()
        {
            throw new Exception("ELMAH is working properly!");
        }


        public ActionResult GoogleLogin()
        {
            var authentication = HttpContext.GetOwinContext().Authentication;

            authentication.Challenge(
                new AuthenticationProperties
                {
                    // Let OWIN handle the Google return, THEN redirect here:
                    RedirectUri = Url.Action("GoogleCallback", "Account")
                },
                "Google"
            );

            return new HttpUnauthorizedResult();
        }

        public async Task<ActionResult> GoogleCallback()
        {
            // Retrieve the external login info securely from the OWIN context
            var loginInfo = await HttpContext.GetOwinContext().Authentication.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Safely extract claims from loginInfo.ExternalIdentity
            var identity = loginInfo.ExternalIdentity;
            var email = identity.FindFirstValue(ClaimTypes.Email);
            var name = identity.FindFirstValue(ClaimTypes.Name);
            var googleId = identity.FindFirstValue(ClaimTypes.NameIdentifier);

            // Safeguard name splitting in case name is null or a single word
            string firstName = "";
            string lastName = "";
            if (!string.IsNullOrEmpty(name))
            {
                var parts = name.Split(' ');
                firstName = parts[0];
                lastName = parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : ""; // Handles middle/last names safely
            }

            var user = new UserAccount
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                GoogleId = googleId,
                LoginProvider = "Google"
            };

            hc.BaseAddress = new Uri("https://localhost:44393/api/Account/");

            var response = await hc.PostAsJsonAsync("GoogleLogin", user);

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            var result = await response.Content.ReadAsAsync<dynamic>();
            string token = result.Token.ToString();

            HttpCookie cookie = new HttpCookie("jwtToken", token)
            {
                Path = "/"
            };
            Response.Cookies.Add(cookie);

            return RedirectToAction("LoggedIn");
        }
    }
}