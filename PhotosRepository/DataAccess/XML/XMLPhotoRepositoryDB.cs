using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using log4net;
using PhotosRepository;
using PhotosRepository.DataAccess;
using System.Xml.Serialization;
using System.IO;

namespace PhotosRepository.DataAcess.XML
{
    public class XMLPhotoRepositoryDB : IPhotosRepository
    {
        private static XMLPhotoRepositoryDB _instance = null; 

        private XElement _db;
        private string _serverRunningPath;
        private List<IPhoto> _photos;
        private string _photosLocalLocation = "Content\\Photos\\";
        
        private static readonly ILog _log = LogManager.GetLogger("PhotoReprository"); //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static XMLPhotoRepositoryDB GetInstance()
        {
            if(_instance == null) 
                _instance = new XMLPhotoRepositoryDB();
            return _instance;
        }

        protected XMLPhotoRepositoryDB()
        {
        }

        public void Init()
        {
            InitXmlRepository(); 
        }

        public void InitXmlRepository(XElement root = null)
        {
            _serverRunningPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            InitXmlRepositoryDB(root);    
        }

        private XElement LoadDocumentRoot()
        {
            return XDocument.Load(_serverRunningPath + "/galleries.xml").Element("root");
        }

        private void InitXmlRepositoryDB(XElement root = null)
        {
            if (root == null)
            {
                try
                {
                    _db = LoadDocumentRoot(); 
                }
                catch (Exception e)
                {
                    _log.ErrorFormat("Unable to load XML repo: {0}", e.Message);
                }
            }
            else
            {
                _db = root; 
            }

            ParsePhotosData();
        }

        private void ParsePhotosData()
        {
            _photos = new List<IPhoto>();
            var photos = _db.Element("photos").Descendants().Where(tag => tag.Name == "photo");

            if (photos.Count() == 0)
            {
                _log.Error("No photos were found in XML repo");
                return;
            }

            Photo currentPhoto;
            foreach (var photo in photos)
            {
                string caption = photo.Element("caption").Value;
                string title = photo.Element("title").Value;
                string fileName = photo.Element("filename").Value;
                string width = photo.Element("metadata").Element("width").Value;
                string height = photo.Element("metadata").Element("height").Value;
                string fullPhotoPath = _serverRunningPath + _photosLocalLocation;
                bool isDbUpdateNeeded = false;

                currentPhoto = new Photo(fileName)
                {
                    Caption = (String.IsNullOrEmpty(caption) ? Path.GetFileNameWithoutExtension(fileName) : caption),
                    Title = (String.IsNullOrEmpty(title) ? Path.GetFileNameWithoutExtension(fileName): title),
                };

                if (String.IsNullOrEmpty(width) == true || 
                    String.IsNullOrEmpty(height) == true || 
                    String.IsNullOrEmpty(title) == true || 
                    String.IsNullOrEmpty(caption) == true)
                {
                    if (currentPhoto.Init(fullPhotoPath) == false) // don't add the photo is init failed!
                    {
                        _log.ErrorFormat("Unable to init photo {0}, removing from list");
                        continue; 
                    }
                
                    isDbUpdateNeeded = true; 
                }
                else
                {
                    currentPhoto.Init(width, height); 
                }

                var tags = photo.Element("tags").Descendants();
                foreach (var tag in tags)
                    currentPhoto.AddTag(tag.Value);

                if (isDbUpdateNeeded == true)
                {
                    UpdateDBPhotoInfo(currentPhoto); // do it in a thread!
                }
                
                _photos.Add(currentPhoto);
            }
        }

        private XElement GetGalleryEntry(string galleryName)
        {
            XElement galleryEntry; 
            //throw new Exception(String.Format("Cannot find Gallery {0}", galleryName));  //--- for testing
            try
            {
                galleryEntry = _db.Element("galleries").Descendants("name").Where(g => 
                    String.Equals(g.Value, galleryName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            
            return galleryEntry.Parent; 
        }

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            return GetGalleryEntry(galleryName).Element("openingPhoto").Value;
        }

        public IEnumerable<IPhoto> GetGalleryPhotos(string galleryName)
        {
            string openingPhotoName = GetGalleryOpeningPhoto(galleryName);
            string galleryIdentifyingTag = GetGalleryEntry(galleryName).Element("tag").Value;
            if (galleryIdentifyingTag == "*")
            {
                return _photos.ToList(); 
            }

            return _photos.Where(p => p.Tags.Where(t => t.Equals(galleryIdentifyingTag, StringComparison.OrdinalIgnoreCase)).Any());
        }

        public GalleryConfig GetGalleryConfig(string galleryName)
        {
            return new GalleryConfig
            {
                Name = galleryName,
                TimeOut = Int32.Parse(GetGalleryEntry(galleryName).Element("config").Element("PhotoCycle").Attribute("Timeout").Value),
                AutoCycle = Boolean.Parse(GetGalleryEntry(galleryName).Element("config").Element("PhotoCycle").Attribute("AutoCycle").Value),
                PhotosLocation = "" // will be set later on according to global site configuration
            };
        }

        public IEnumerable<GalleryConfig> GetAllGalleries()
        {
            List<GalleryConfig> galleries = new List<GalleryConfig>(); 
            foreach(var gallery in _db.Element("galleries").Descendants().Where(g => g.Name == "name"))
            {
                galleries.Add(GetGalleryConfig(gallery.Value)); 
            }

            return galleries; 
        }
        private bool UpdateDBPhotoInfo(IPhoto photo)
        {
            bool status = false;
            var dbPhotoEntry = _db.Element("photos").Descendants("photo").Where(g =>
                        String.Equals(g.Element("filename").Value, photo.FileName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            dbPhotoEntry.Element("metadata").Element("width").Value = Convert.ToString(photo.Metadata.Width);
            dbPhotoEntry.Element("metadata").Element("height").Value = Convert.ToString(photo.Metadata.Height);
            dbPhotoEntry.Element("title").Value = Convert.ToString(photo.Title);
            dbPhotoEntry.Element("caption").Value = Convert.ToString(photo.Caption);

            var tags = dbPhotoEntry.Element("tags");
            var existingTags = tags.Descendants();
            
            foreach (var tag in photo.Tags)
            {
                if (existingTags.Where(t => t.Value == tag).Count() == 0)
                {
                    tags.Add(new XElement("tag", tag));
                }
            }
                
            
            _db.Save(_serverRunningPath + "/galleries.xml");

            // add the tags as well 
            _log.DebugFormat("Photo: {0} was updated with new metadata: {1}", photo.FileName, photo.Metadata.GetMetadataAsString());
            return status; 
        } 
    }
}
