using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace PhotosRepository
{
    public class XMLPhotoRepositoryDB : IPhotosRepository
    {
        List<Photo> _photos;
        XElement _db;
        string _serverRunningPath;
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public XMLPhotoRepositoryDB()
        {
            _serverRunningPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            initDB();
        }

        private void initDB()
        {
            try
            {
                _db = XDocument.Load(_serverRunningPath + "/galleries.xml").Element("root");
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Unable to load XML repo: {0}", e.Message);
            }


            _photos = new List<Photo>();
            var photos = _db.Element("photos").Descendants().Where(tag => tag.Name == "photo");

            if (photos.Count() == 0)
            {
                _log.Error("No photos were found in XML repo");
                return;
            }

            Photo currentPhoto;
            foreach (var photo in photos)
            {
                var captions = photo.Descendants().Where(tag => tag.Name == "caption");
                currentPhoto = new Photo()
                {
                    FileName = photo.Descendants().Where(tag => tag.Name == "filename").FirstOrDefault().Value,
                    Caption = (captions.Count() > 0 ? captions.FirstOrDefault().Value : "N/A")
                };

                var tags = photo.Element("tags").Descendants();
                foreach (var tag in tags)
                    currentPhoto.SetTag(tag.Value);

                _photos.Add(currentPhoto);
            }
        }

        private XElement GetGalleryEntry(string galleryName)
        {
            //throw new Exception(String.Format("Cannot find Gallery {0}", galleryName));  --- for testing
            return _db.Element("galleries").Descendants().Where(g => String.Equals(g.Element("name").Value, galleryName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        }

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            return GetGalleryEntry(galleryName).Element("openingPhoto").Value;
        }

        public IEnumerable<string> GetGalleryPhotos(string galleryName)
        {
            string openingPhoto = GetGalleryOpeningPhoto(galleryName);
            string galleryIdentifyingTag = GetGalleryEntry(galleryName).Element("tag").Value;
            return _photos.Where(p => p.GetTags().Where(t => t.IndexOf(galleryIdentifyingTag, StringComparison.OrdinalIgnoreCase) >= 0).Any() &&
                                        ! String.Equals(p.FileName, openingPhoto, StringComparison.CurrentCultureIgnoreCase)).Select(f => f.FileName);
        }


        public GalleryConfig GetGalleryConfig(string galleryName)
        {
            return new GalleryConfig
            {
                TimeOut = Int32.Parse(GetGalleryEntry(galleryName).Element("config").Element("PhotoCycle").Attribute("Timeout").Value),
                AutoCycle = Boolean.Parse(GetGalleryEntry(galleryName).Element("config").Element("PhotoCycle").Attribute("AutoCycle").Value),
                PhotosLocation = "/Content/images/"
            };
        }
    }
}
