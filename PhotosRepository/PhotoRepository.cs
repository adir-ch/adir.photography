using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public class PhotoRepository
    {
        private IPhotoRepositoryDB _db; 

        public PhotoRepository(string serverRunningPath)
        {
            // inject the repo DB here! 
            _db = new XMLPhotoRepositoryDB();
            _db.initDB(serverRunningPath); 
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