using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotosRepository;
using System.Xml.Linq;
using PhotosRepository.DataAcess.XML;
using PhotosRepository.Model;

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
                          <tag>ap-main</tag>
                        </gallery>
                      </galleries>
                      <photos>
                        <photo>
                          <filename>girona.jpg</filename>
                          <title>girona</title>
                          <metadata>
                            <width>1440</width>
                            <height>1080</height>
                          </metadata>
                          <caption>Ally-bike</caption>
                          <tags>
                            <tag></tag>
                            <tag>ap-main</tag>
                            <tag>ap-website</tag>
                          </tags>
                        </photo>
                        <photo>
                          <filename>br1.jpg</filename>
                          <title>br1</title>
                          <metadata>
                            <width>1080</width>
                            <height>1626</height>
                          </metadata>
                          <caption>A.C(C)</caption>
                          <tags>
                            <tag></tag>
                            <tag>Portfolio-1</tag>
                            <tag>ap-main</tag>
                            <tag>ap-website</tag>
                          </tags>
                        </photo>
                        <photo>
                          <filename>fe1.jpg</filename>
                          <title>fe1</title>
                          <metadata>
                            <width>1920</width>
                            <height>981</height>
                          </metadata>
                          <caption>A.C(C)</caption>
                          <tags>
                            <tag></tag>
                            <tag>Portfolio-1</tag>
                            <tag>Portfolio-2</tag>
                            <tag>ap-main</tag>
                            <tag>ap-wildlife</tag>
                          </tags>
                        </photo>
                        <photo>
                          <filename>fe2.jpg</filename>
                          <title>fe2</title>
                          <metadata>
                            <width>1080</width>
                            <height>1626</height>
                          </metadata>
                          <caption>A.C(C)</caption>
                          <tags>
                            <tag></tag>
                            <tag>Portfolio-1</tag>
                            <tag>Portfolio-2</tag>
                            <tag>ap-website</tag>
                          </tags>
                        </photo>
                      </photos>
                    </root>";

            _repo = XMLPhotoRepositoryDB.GetInstance(); 
            _repo.Init(XDocument.Parse(repoData).Element("root")); 
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
        public void ReturnAllGalleryPhotos()
        {
            IEnumerable<IPhoto> repoGalleryPhotos = _repo.GetGalleryPhotos(_galleryName);
            Assert.AreEqual(repoGalleryPhotos.Count(), 3);
            Assert.AreEqual(repoGalleryPhotos.Where(p => p.FileName.Equals("girona.jpg") == true).Count(), 1);
            Assert.AreEqual(repoGalleryPhotos.Where(p => p.FileName.Equals("br1.jpg") == true).Count(), 1);
            Assert.AreEqual(repoGalleryPhotos.Where(p => p.FileName.Equals("fe1.jpg") == true).Count(), 1);
            Assert.IsTrue(repoGalleryPhotos.Where(p => p.FileName.Equals("fe2.jpg") == true).Count() == 0); 
        }
    }
}
