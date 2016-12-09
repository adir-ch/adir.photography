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
using PhotosRepository.DataAccess.XML;
using PhotosRepository.Model;

namespace PhotosRepository.DataAcess.XML
{
    public class XMLPhotoRepositoryDB : IPhotosRepository
    {
        private static XMLPhotoRepositoryDB _instance = null; 

        private XMLDbContext _db;
        private List<IPhoto> _photos;
        private string _serverRunningPath;
        private string _photosLocalLocation = "Content\\Photos\\";
        private string _fullPhotosPath;
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

        public bool Init()
        {
            return InitXmlRepository(); 
        }

        public IEnumerable<IPhoto> InitPhotos()
        {
            // force reading photos data from files
            ParsePhotosData(true);
            return _photos.ToList();
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
            foreach (var gallery in _db.Fetch("all-galleries"))
            {
                galleries.Add(GetGalleryConfig((gallery as XElement).Value));
            }

            return galleries;
        }

        private bool InitXmlRepository(XElement root = null)
        {
            _serverRunningPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            _fullPhotosPath = _serverRunningPath + _photosLocalLocation;
            return InitXmlRepositoryDB(root);    
        }

        private XElement LoadDocumentRoot()
        {
            return XDocument.Load(_serverRunningPath + "/galleries.xml").Element("root");
        }

        private bool InitXmlRepositoryDB(XElement root = null)
        {
            if (root == null)
            {
                try
                {
                    _db = new XMLDbContext(LoadDocumentRoot(), _serverRunningPath); 
                }
                catch (Exception e)
                {
                    _log.ErrorFormat("Unable to load XML repo: {0}", e.Message);
                }
            }
            else
            {
                _db = new XMLDbContext(root, _serverRunningPath); 
            }

            return ParsePhotosData();
        }

        private bool ParsePhotosData(bool forceUpdateFromImageFile = false)
        {
            _photos = new List<IPhoto>();
            var photosFound = _db.Fetch("all-photos"); 
            _log.DebugFormat("parsing photos DB, number of photos found: {0}", photosFound.Count()); 

            if (photosFound.Count() == 0)
            {
                _log.Error("No photos were found in XML repo");
                return false; 
            }

            Photo currentPhoto;
            foreach (XMLPhotoDBEntity photo in photosFound)
            {
                currentPhoto = InitPhoto(photo, forceUpdateFromImageFile); 
                if (currentPhoto == null)
                    continue; 
                _photos.Add(currentPhoto);
            }

            if(photosFound.Count() != _photos.Count())
            {
                _log.WarnFormat("Could not load all photos from DB, number of actual loaded photos: {0}", _photos.Count());
                // return false; // TODO - what to do here? 
            }

            return true;
        }

        private Photo InitPhoto(XMLPhotoDBEntity photoEntity, bool forceUpdateFromImageFile = false)
        {
            var photo = photoEntity.GetPhoto(_fullPhotosPath, forceUpdateFromImageFile);

            if (photoEntity.IsDBUpdateNeeded() == true)
            {
                _db.Save(photoEntity); // do it in a thread!
            }
            return photo as Photo; 
        }

        private XElement GetGalleryEntry(string galleryName)
        {
            XElement galleryEntry;
            //throw new Exception(String.Format("Cannot find Gallery {0}", galleryName));  //--- for testing
            try
            {
                galleryEntry = _db.Fetch("gallery", galleryName).FirstOrDefault() as XElement; 
            }
            catch (Exception)
            {
                throw;
            }

            return galleryEntry.Parent;
        }
    }
}
