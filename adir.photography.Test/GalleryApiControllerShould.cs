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
        }

        [TestMethod]
        public void GetGalleryConfigData()
        {
            int timeout = 10;
            _mockGalleryDataService.Setup(g => g.GetGalleryConfig(_galleryName)).Returns(new GalleryConfig
            {
                TimeOut = 10,
                AutoCycle = true,
                PhotosLocation = _photolocation
            });

            var result = _controller.Get(_galleryName) as OkNegotiatedContentResult<HomeGalleryModel>;
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(HomeGalleryModel));
            Assert.AreEqual(model.Timeout, timeout);
            Assert.AreEqual(model.AutoCycle, true);
            Assert.AreEqual(model.ImagesLocation, _photolocation); 
        }
    }
}
