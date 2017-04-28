using adir.photography.Models;
using adir.photography.Services.EmailSender;
using log4net;
using System;
using System.Web.Mvc;

namespace adir.photography.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IEmailSendingService _emailSendingService;
            
        public HomeController() : this(new EmailSendingService()) // for testing - replace with DI
        {
 
        } 

        public HomeController(IEmailSendingService emailSendingService)
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

            _emailSendingService = emailSendingService; 
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
            return View();
        }

        public ActionResult About(string id)
        {
            // About page
            ViewBag.Title = "adir.photography";
            ViewBag.newContactForm = true;
            
            return View();
        }
        
        [HttpPost]
        public ActionResult About(ContactFormModel contactFormInputData)
        {
            ViewBag.emailWasSentSuccessfuly = false; 
            if(ModelState.IsValid)
            {
                ViewBag.emailWasSentSuccessfuly = _emailSendingService.SendEmail(contactFormInputData);
                contactFormInputData.Clear();
            }
            
            ModelState.Clear();
            ViewBag.newContactForm = false;
            return View(); // TODO: maybe redirect the user to a different page (main gallery?)
        }

        public ActionResult Member(string id)
        {
            ViewBag.Title = "adir.photography";
            return View();
        }
    }
}
