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

            // Temporary repository 

            _db = new List<Photo> { new Photo { FileName = "DSC_0193.jpg" },
                                    new Photo { FileName = "DSC_1657-Edit.jpg" }, 
                                    new Photo { FileName = "DSC_1707_HDR.jpg" }, 
                                    new Photo { FileName = "DSC_5690.jpg" }};

            _db[0].SetTag(PhotoTag.MainGalleryOpening);

            var photos = _db.Where(p => p.GetTags().Contains(PhotoTag.MainGalleryOpening) == false);
            foreach (var p in photos)
                p.SetTag(PhotoTag.MainGallery);

        }

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            // do something here! 
            throw new NotImplementedException();
            
            return _db.Where(p => p.GetTags().Contains(PhotoTag.MainGalleryOpening)).FirstOrDefault(); 
        }

        public IEnumerable<string> GetGalleryPhotos(string galleryName)
        {
            return _db.Where(p => p.GetTags().Contains(PhotoTag.MainGallery));
        }
    }
}
