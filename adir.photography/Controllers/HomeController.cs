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

        public ActionResult Index()
        {
            HomeGalleryModel model = new HomeGalleryModel(Server.MapPath("~"));
            return View(model);
        }

    }
}
