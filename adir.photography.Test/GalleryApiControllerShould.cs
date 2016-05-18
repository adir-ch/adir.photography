using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using adir.photography;
using adir.photography.Controllers;
using adir.photography.Services.Gallery;
using adir.photography.Services.WebSiteConfig; 
using Moq;
using PhotosRepository;
using System.Web.Http;
using adir.photography.Models;
using System.Web.Http.Results;
using System.Net.Http;
using System.Linq; 

namespace adir.photography.Test
{
    /// <summary>
    /// Summary description for GalleryApiControllerShould
    /// </summary>
    [TestClass]
    public class GalleryApiControllerShould
    {
        private readonly string _galleryName = "Main"; 
        private readonly string _photolocation = "/content/photos";
        private GalleryApiController _controller; 
        private Mock<IGalleryDataService> _mockGalleryDataService; 

        public GalleryApiControllerShould()
        {
            //
            // TODO: Add constructor logic here
            //
        }

   
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void TestInitialize() 
        {
            _mockGalleryDataService = new Mock<IGalleryDataService>();
            _controller = new GalleryApiController(_mockGalleryDataService.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

            _mockGalleryDataService.Setup(g => g.GetGalleryConfig(_galleryName)).Returns(new GalleryConfig
            {
                TimeOut = 10,
                AutoCycle = true,
                PhotosLocation = _photolocation
            });

            _mockGalleryDataService.Setup(g => g.GetGalleryOpeningPhoto(_galleryName)).Returns("opening.jpg");
            _mockGalleryDataService.Setup(g => g.GetGalleryPhotos(_galleryName)).Returns(new List<string> {"p1.jpg", "p2.jpg"}); 

        }

        [TestMethod]
        public void GetGalleryConfigData()
        {
            int timeout = 10;
            

            var result = _controller.Get(_galleryName) as OkNegotiatedContentResult<HomeGalleryModel>;
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(HomeGalleryModel));
            Assert.AreEqual(model.Timeout, timeout);
            Assert.AreEqual(model.AutoCycle, true);
            Assert.AreEqual(model.ImagesLocation, _photolocation); 
        }

        [TestMethod]
        public void GetGalleryOpeningPhotos()
        {
            IHttpActionResult response = _controller.Get(_galleryName); 
            var result =  response as OkNegotiatedContentResult<HomeGalleryModel>;
            Assert.IsNotNull(result);
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(HomeGalleryModel));
            Assert.AreEqual(model.OpeningPhoto, "opening.jpg");
        }

        [TestMethod]
        public void GetGalleryPhotos()
        {
            IHttpActionResult response = _controller.Get(_galleryName);
            var result = response as OkNegotiatedContentResult<HomeGalleryModel>;
            Assert.IsNotNull(result);
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(HomeGalleryModel));
            
            List<string> galleryPhotos = model.GalleryPhotos as List<string>;
            Assert.AreEqual(galleryPhotos.Count, 2);
            Assert.AreEqual(galleryPhotos.Where(p => p.Equals("p1.jpg") == true).Count(), 1);
            Assert.AreEqual(galleryPhotos.Where(p => p.Equals("p2.jpg") == true).Count(), 1); 
        }

        [TestMethod]
        public void ReturnAnInternalServerExceptionIfGalleryNotFound()
        {
            IHttpActionResult response = _controller.Get("not-existing-gallery");
            Assert.IsInstanceOfType(response, typeof(ExceptionResult));
        }
    }
}
