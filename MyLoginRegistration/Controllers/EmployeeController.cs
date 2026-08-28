using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyLoginRegistration.Models;
using MyLoginRegistration.Services;

namespace MyLoginRegistration.Controllers
{

    public class EmployeeController : Controller
    {
        HttpClient hc = new HttpClient();

        private EmployeeService empService = new EmployeeService();
        private DepartmentService deptService = new DepartmentService();
        [JwtAuthorize]
        public ActionResult Index()
        {
            List<UserAccount> employee = new List<UserAccount>();

            hc.BaseAddress = new Uri("https://localhost:44393/api/Employee/GetEmployee");

            var responce = hc.GetAsync("GetEmployee");
            responce.Wait();

            var E = responce.Result;

            if (E.IsSuccessStatusCode)
            {
                var display = E.Content.ReadAsAsync<List<UserAccount>>();
                display.Wait();
                employee = display.Result;
            }
            return View(employee);
        }

        [JwtAuthorize]
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                if (Request.IsAjaxRequest())
                {
                    return new HttpStatusCodeResult(403);
                }

                return RedirectToAction("LoggedIn", "Account");
            }

            ViewBag.Departments = deptService.GetAll();
            // Return only the form HTML, not the full _Layout
            return PartialView("_CreatePartial");
        }

        [JwtAuthorize]
        public ActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                if (Request.IsAjaxRequest())
                {
                    return new HttpStatusCodeResult(403);
                }

                return RedirectToAction("LoggedIn", "Account");
            }


            var emp = empService.GetById(id);
            ViewBag.Departments = deptService.GetAll();
            return PartialView("_EditEmp", emp);
        }
    }
}