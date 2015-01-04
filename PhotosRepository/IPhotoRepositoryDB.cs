using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public interface IPhotoRepositoryDB
    {
        void initDB(string serverRunningPath);

        string GetGalleryOpeningPhoto(string galleryName);
        IEnumerable<string> GetGalleryPhotos(string galleryName); 
    }
}
