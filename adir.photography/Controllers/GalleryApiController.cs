using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using adir.photography.Models;
using adir.photography.Services.WebSiteConfig;
using adir.photography.Services.Gallery;
using log4net; 

namespace adir.photography.Controllers
{
    [RoutePrefix("api/galleryapi")]
    public class GalleryApiController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IGalleryDataService _galleryDataService;

        public GalleryApiController(IGalleryDataService galleryDataService)
        {
            _galleryDataService = galleryDataService; 

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

        public GalleryApiController() : this (new GalleryDataService())
        {

        }

        // GET api/galleryapi - changed to WebApi 2 style 
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetGalleryData()
        {
            try
            {
                var model = _galleryDataService.GetGalleryData("Main");
                return Ok(model);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // GET api/galleryapi/<gallery name> 
        [Route("{name}")]
        [HttpGet]
        public IHttpActionResult GetGalleryDataByName(string name)
        {
            try
            {
                var model = _galleryDataService.GetGalleryData(name);
                return Ok(model);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // GET api/galleryapi/<gallery name> 
        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllGaleries()
        {
            try
            {
                var model = _galleryDataService.GetAllGalleries();
                return Ok(model);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // GET api/galleryapi/initphotos - changed to WebApi 2 style 
        [Route("initphotos")]
        [HttpGet]
        public IHttpActionResult InitAllPhotos()
        {
            try
            {
                var model = _galleryDataService.InitPhotos();
                return Ok(model);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        // POST api/galleryapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/galleryapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/galleryapi/5
        public void Delete(int id)
        {
        }
    }
}
