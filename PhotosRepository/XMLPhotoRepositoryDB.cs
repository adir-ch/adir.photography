using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml; 
using System.Xml.Linq;

namespace PhotosRepository
{
    class XMLPhotoRepositoryDB : IPhotoRepositoryDB
    {
        List<Photo> _photos;
        XElement _db;

        public XMLPhotoRepositoryDB()
        {
            // currently do nothing!
        }

        public void initDB(string serverRunningPath)
        {
            _db = XDocument.Load(serverRunningPath + "/galleries.xml").Element("root");
            _photos = new List<Photo>(); 

            Photo currentPhoto;
            
            var photos = _db.Element("photos").Descendants().Where(tag => tag.Name == "photo");
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

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            var gallery = _db.Element("galleries").Descendants().Where(g => g.Element("name").Value == "Main").FirstOrDefault();
            return gallery.Element("openingPhoto").Value; 
        }

        public IEnumerable<string> GetGalleryPhotos(string galleryName)
        {
            string openingPhoto = GetGalleryOpeningPhoto(galleryName); 
            return _photos.Where(p => p.GetTags().Contains("main") && p.FileName != openingPhoto).Select(f => f.FileName);
        }
    }
}
