using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotosRepository;
using System.Xml.Linq;

namespace PhotoRepository.Test
{
    /// <summary>
    /// Summary description for XMLPhotoRepositoryDBShould
    /// </summary>
    [TestClass]
    public class XMLPhotoRepositoryDBShould
    {
        private XMLPhotoRepositoryDB _repo;
        private readonly string _galleryName = "Main"; 

        public XMLPhotoRepositoryDBShould()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext) { }
        
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void ClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void TestInitialize() 
        { 
            string repoData =
                @"<?xml version='1.0' encoding='utf-8' ?>
                    <root>
                      <galleries>
                        <gallery>
                          <config>
                            <PhotoCycle AutoCycle='true' Timeout='50'/>
                          </config>
                          <name>Main</name>
                          <openingPhoto>girona.jpg</openingPhoto>
                          <tag>Main</tag>
                        </gallery>
                      </galleries>
                      <photos>
                        <photo>
                          <filename>br1.jpg</filename>
                          <tags>
                            <tag>main</tag>
                          </tags>
                        </photo>
                        <photo>
                          <filename>fe1.jpg</filename>
                          <tags>
                            <tag>main</tag>
                          </tags>
                        </photo>
                        <photo>
                          <filename>fe2.jpg</filename>
                          <tags>
                            <tag>XmainX</tag>
                          </tags>
                        </photo>
                      </photos>
                    </root>"; 

            _repo = new XMLPhotoRepositoryDB(XDocument.Parse(repoData).Element("root")); 
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void TestCleanup() { }

        [TestMethod]
        public void ReturnOneOpeningPhoto()
        {
            Assert.AreEqual("girona.jpg", _repo.GetGalleryOpeningPhoto(_galleryName));
        }

        [TestMethod]
        public void ReturnAllGalleryPhotosExceptOpeningPhoto()
        {
            List<string> galleryPhotos = new List<string> { "br1.jpg", "fe1.jpg" };
            List<string> repoGalleryPhotos = _repo.GetGalleryPhotos(_galleryName) as List<string>;
            CollectionAssert.AreEqual(galleryPhotos.ToArray(), repoGalleryPhotos.ToArray());
        }
    }
}
