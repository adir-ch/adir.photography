using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public class PhotoRepository
    {
        private readonly string _path = "~/Content/images/";
        private IPhotoRepositoryDB _db; 

        public PhotoRepository()
        {
           // inject the repo DB here! 
        }

        public IEnumerable<string> GetMainGalleryPhotos(string galleryName)
        {
            return _db.GetGalleryPhotos(galleryName);
        }

        public string GetMainGalleryOpeningPhotos(string galleryName)
        {
            return _db.GetGalleryOpeningPhoto(galleryName);
        }
    }
}