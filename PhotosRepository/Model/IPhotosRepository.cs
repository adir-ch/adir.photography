using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository.Model
{
    public interface IPhotosRepository
    {
        IEnumerable<GalleryConfig> GetAllGalleries();
        IEnumerable<IPhoto> GetGalleryPhotos(string galleryName);
        string GetGalleryOpeningPhoto(string galleryName);
        GalleryConfig GetGalleryConfig(string galleryName);

        bool Init();
        IEnumerable<IPhoto> InitPhotos(); 
    }
}
