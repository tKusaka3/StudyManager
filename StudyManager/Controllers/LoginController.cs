using StudyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace StudyManager.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user)
        {
            if (user.Password == "0320" && user.Username == "Terumi")
            {
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }, "applicationcookie");
                Request.GetOwinContext().Authentication.SignIn(identity);
                return RedirectToAction("Index", "Attendance");
            }
            return RedirectToAction("Index");


        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index");
        }

    }
}