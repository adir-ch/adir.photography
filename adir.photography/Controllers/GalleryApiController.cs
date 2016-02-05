using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using adir.photography.Models;
using adir.photography.Services.WebSiteConfig;
using adir.photography.Services.Gallery; 

namespace adir.photography.Controllers
{
    [RoutePrefix("api")]
    public class GalleryApiController : ApiController
    {
        
        IWebSiteConfigService _config;
        GalleryDataService _galleryDataService = new GalleryDataService(); 

        public GalleryApiController()
        {
            _config = WebSiteFileConfigService.Instance(); // TODO: inject

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

        // GET api/galleryapi
        public HomeGalleryModel Get()
        {
            return BuildGalleryModel("Main"); 
           
        }

        // GET api/galleryapi/<gallery name> 
        public HomeGalleryModel Get(string name)
        {
            return BuildGalleryModel(name); 
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
            galleryModel.ImagesLocation = _galleryDataService.GetGalleryConfig(galleryName).ImageLocation;
            return galleryModel;
        }

    }
}
