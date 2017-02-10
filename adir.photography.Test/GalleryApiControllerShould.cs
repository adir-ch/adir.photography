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
using PhotosRepository.Model; 

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
        private Mock<IGalleryDataService> _galleryDataServiceMock; 

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
            _galleryDataServiceMock = new Mock<IGalleryDataService>();
            _controller = new GalleryApiController(_galleryDataServiceMock.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

            _galleryDataServiceMock.Setup(g => g.GetGalleryData(_galleryName)).Returns(new UserGalleryModel
            {
                Name = _galleryName, 
                OpeningPhoto = "opening.jpg",
                GalleryPhotos = SetFakePhotos(),
                Timeout = 10,
                AutoCycle = true,
                ImagesLocation = _photolocation
            });
        }

        private IEnumerable<IPhoto> SetFakePhotos()
        {
            List<Photo> photos = new List<Photo>();
            photos.Add(new Photo("p1.jpg"));
            photos.Add(new Photo("p2.jpg"));

            return photos;
        }

        [TestMethod]
        public void GetGalleryConfigData()
        {
            int timeout = 10;
            

            var result = _controller.GetGalleryDataByName(_galleryName) as OkNegotiatedContentResult<UserGalleryModel>;
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(UserGalleryModel));
            Assert.AreEqual(model.Timeout, timeout);
            Assert.AreEqual(model.AutoCycle, true);
            Assert.AreEqual(model.ImagesLocation, _photolocation); 
        }

        [TestMethod]
        public void GetGalleryOpeningPhotos()
        {
            IHttpActionResult response = _controller.GetGalleryDataByName(_galleryName); 
            var result =  response as OkNegotiatedContentResult<UserGalleryModel>;
            Assert.IsNotNull(result);
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(UserGalleryModel));
            Assert.AreEqual(model.OpeningPhoto, "opening.jpg");
        }

        [TestMethod]
        public void GetGalleryPhotos()
        {
            IHttpActionResult response = _controller.GetGalleryDataByName(_galleryName);
            var result = response as OkNegotiatedContentResult<UserGalleryModel>;
            Assert.IsNotNull(result);
            var model = result.Content;
            Assert.IsNotNull(model);
            Assert.IsInstanceOfType(model, typeof(UserGalleryModel));
            
            IEnumerable<IPhoto> galleryPhotos = model.GalleryPhotos;
            Assert.AreEqual(galleryPhotos.ToList().Count, 2);
            Assert.AreEqual(galleryPhotos.Where(p => p.FileName.Equals("p1.jpg") == true).Count(), 1);
            Assert.AreEqual(galleryPhotos.Where(p => p.FileName.Equals("p2.jpg") == true).Count(), 1); 
        }

        [TestMethod]
        public void ReturnAnInternalServerExceptionIfGalleryNotFound()
        {
            _galleryDataServiceMock.Setup(m => m.GetGalleryData(It.IsAny<string>())).Throws(new Exception("exception"));
            IHttpActionResult response = _controller.GetGalleryDataByName("not-existing-gallery");
            Assert.IsInstanceOfType(response, typeof(ExceptionResult));
        }
    }
}
