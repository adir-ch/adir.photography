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
    [RoutePrefix("api")]
    public class GalleryApiController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IWebSiteConfigService _config;
        GalleryDataService _galleryDataService;

        public GalleryApiController()
        {
            _config = WebSiteFileConfigService.Instance(); // TODO: inject
            _galleryDataService = new GalleryDataService(); // TODO: inject

            // setting logger for current user name
            if (String.IsNullOrEmpty(System.Web.HttpContext.Current.User.Identity.Name) == false)
            {
                log4net.ThreadContext.Properties["UserId"] = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {
                log4net.ThreadContext.Properties["UserId"] = "Anonymous";
            }

        }

        // ---- TODO: convert all API's to web api 2 - similar to the below example. 
        //[HttpGet]
        //[Route("gallery")]
        //public HttpResponseMessage GetGalleryPhotos(HttpRequestMessage request)
        //{
        //    HomeGalleryModel gallery = new HomeGalleryModel();
        //    gallery.OpeningImage = _repo.GetMainGalleryOpeningPhotos("main");
        //    gallery.MainGalleryImages = _repo.GetMainGalleryPhotos("main");
        //    return request.CreateResponse<HomeGalleryModel>(HttpStatusCode.OK, gallery);
        //}

        // GET api/galleryapi - changed to WebApi 2 style 
        public IHttpActionResult Get()
        {
            try
            {
                var model = BuildGalleryModel("Main");
                return Ok(model);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

        }

        //public HttpResponseMessage Get() - can also be done with: 
        //{
        //    try
        //    {
        //        var model = BuildGalleryModel("Main");
        //        return Request.CreateResponse(HttpStatusCode.OK, model);
        //    }
        //    catch (Exception e)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        // GET api/galleryapi/<gallery name> 
        public IHttpActionResult Get(string name)
        {
            try
            {
                var model = BuildGalleryModel(name);
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

        private HomeGalleryModel BuildGalleryModel(string galleryName)
        {
            HomeGalleryModel galleryModel = new HomeGalleryModel();

            galleryModel.OpeningPhoto = _galleryDataService.GetGalleryOpeningPhoto(galleryName);
            galleryModel.GalleryPhotos = _galleryDataService.GetGalleryPhotos(galleryName);
            galleryModel.Timeout = _galleryDataService.GetGalleryConfig(galleryName).TimeOut;
            galleryModel.AutoCycle = _galleryDataService.GetGalleryConfig(galleryName).AutoCycle;
            galleryModel.ImagesLocation = _galleryDataService.GetGalleryConfig(galleryName).PhotosLocation;
            return galleryModel;
        }

    }
}
