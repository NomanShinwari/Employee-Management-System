using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MyLoginRegistration.Models;
using MyLoginRegistration.Services;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Runtime.InteropServices;



namespace MyLoginRegistration.Controllers
{
    // [Authorize]
     
    public class DepartmentController : Controller
    {
        HttpClient hc = new HttpClient();

        private DepartmentService deptService = new DepartmentService();

        [JwtAuthorize]
        public ActionResult Index()
        {
            List<Department> deptt = new List<Department>();
            hc.BaseAddress = new Uri("https://localhost:44393/api/Deppt/GetDeptt");

                  var responce = hc.GetAsync("GetDeptt");
                  responce.Wait();

                  var D = responce.Result;

                  if (D.IsSuccessStatusCode)
                      {
                      var display = D.Content.ReadAsAsync<List<Department>>();
                        display.Wait();
                        deptt = display.Result;
                  }
                  return View(deptt);
        }

        [JwtAuthorize]
        public ActionResult Create()
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["Error"] = "You are not authorized to access this page.";
                return RedirectToAction("LoggedIn", "Account");
            }
            return PartialView("_CreateDeptt");
        }

        [JwtAuthorize]
        public ActionResult Edit(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                TempData["Error"] = "You are not authorized to access this page.";
                return RedirectToAction("LoggedIn", "Account");
            }

            var dept = deptService.GetById(id);
            return PartialView("_EditDeptt", dept);
        }

    }
}