using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day1New.Controllers
{
    public class DashController : Controller
    {
        public IActionResult Index()
        {
            TempData["token"] = HttpContext.Session.GetString("token");

            if (TempData["token"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["fname"] = HttpContext.Session.GetString("fname");
            TempData["lname"] = HttpContext.Session.GetString("lname");
            Console.Write(TempData["token"]);
            return View("Dash");
        }

       public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");


        }
    }
}
