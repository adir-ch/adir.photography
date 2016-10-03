using adir.photography.Services.Gallery;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace adir.photography.Controllers
{
    public class GalleriesApiController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IGalleryDataService _galleryDataService;

        public GalleriesApiController() : this (new GalleryDataService())
        {

        }

        public GalleriesApiController(IGalleryDataService galleryDataService) 
        {
            _galleryDataService = galleryDataService; 
        }

        public IHttpActionResult Get()
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

        //// GET api/galleryapi/<gallery name> 
        //public IHttpActionResult Get(string userName)
        //{
        //    try
        //    {
        //        var model = _galleryDataService.GetAllGalleries(); // change it to get by user name
        //        return Ok(model);
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError(e);
        //    }
        //}

        // POST: api/GalleriesApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GalleriesApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GalleriesApi/5
        public void Delete(int id)
        {
        }
    }
}
