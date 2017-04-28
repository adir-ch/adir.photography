using adir.photography.Models;
using adir.photography.Services.Register;
using log4net;
using System;
using System.Web.Http;

namespace adir.photography.Controllers
{
    [RoutePrefix("api/registerapi")]
    public class RegisterApiController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IRegisterDataService _registerDataService; 

        public RegisterApiController(IRegisterDataService registerDataService)
        {
            _registerDataService = registerDataService; 

            // setting logger for current user name
            if (System.Web.HttpContext.Current != null && String.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) == false)
            {
                log4net.ThreadContext.Properties["UserId"] = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {
                log4net.ThreadContext.Properties["UserId"] = "Anonymous";
            }
        }

        public RegisterApiController() : this (new RegisterDataService()) 
        {

        }

        // POST api/registerapi/updates
        [Route("updates")]
        [HttpPost]
        public IHttpActionResult UpdatesSubscriber([FromBody]RegisterInfoModel value)
        {
            _log.InfoFormat("NEW-SUBSCRIBER: {0}", value.Email); 
            
            try 
            {
                bool status = _registerDataService.AddNewInfoSubscriber(value.Email);
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
