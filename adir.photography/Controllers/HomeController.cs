using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net; 
using System.Web.Mvc;
using adir.photography.Models;
using log4net;

namespace adir.photography.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public HomeController()
        {
            // setting logger for current user name
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) == false)
            {
                log4net.ThreadContext.Properties["UserId"] = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {
                log4net.ThreadContext.Properties["UserId"] = "Anonymous"; // TODO, Change it to something else
            }
        }

        // GET: /Home/

        public ActionResult Index(string id)
        {
            // TODO: check the logged in user, and get his gallery
            return RedirectToAction("Gallery");
        }

        public ActionResult Gallery(string id)
        {
            // TODO: check the logged in user, and get his gallery
            ViewBag.Title = "adir.photography";
            _log.ErrorFormat("Logger test {0}", "ADIR");
            return View();
        }
    }
}
