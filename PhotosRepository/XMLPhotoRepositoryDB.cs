using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml; 

namespace PhotosRepository
{
    class XMLPhotoRepositoryDB : IPhotoRepositoryDB
    {
        List<Photo> _photos;
        XmlDocument _db; 

        public void initDB()
        {
            _db = new XmlDocument();
            _db.Load("~/galleries.xml"); 
        }

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetGalleryPhotos(string galleryName)
        {
            throw new NotImplementedException();
        }
    }
}
