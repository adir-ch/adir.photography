using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using adir.photography.Models;

namespace adir.photography.Controllers
{
    public class HomeController : Controller
    {
        //
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
            return View();
        }
    }
}
