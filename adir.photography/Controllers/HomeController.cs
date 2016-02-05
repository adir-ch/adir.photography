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
            //HomeGalleryModel model = new HomeGalleryModel(Server.MapPath("~"), id);
            ViewBag.Title = "adir.photography";
            return View();
        }
    }
}
