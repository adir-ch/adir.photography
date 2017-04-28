using System;
using System.Web.Mvc;

namespace adir.photography.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound(string aspxerrorpath)
        {
            Response.StatusCode = 404; 
            ViewBag.Message = String.Format("Oops, could not find the page you requested");
            return View();
        }

        public ViewResult ServerException(string aspxerrorpath)
        {
            Response.StatusCode = 500; 
            ViewBag.Message = String.Format("Server exception :(");
            return View();
        }
    }
}
