using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Tournament.Models;

namespace Tournament.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            if (User.Identity.IsAuthenticated)
            {
                IList<string> roles = new List<string> { "роль не определена" };

                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                ApplicationUser user = userManager.FindByName(User.Identity.Name);

                if (user != null)
                    roles = userManager.GetRoles(user.Id);
                return View(roles);
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}