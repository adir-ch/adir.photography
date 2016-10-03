using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public interface IPhotosRepository
    {
        IEnumerable<GalleryConfig> GetAllGalleries();
        IEnumerable<string> GetGalleryPhotos(string galleryName);
        string GetGalleryOpeningPhoto(string galleryName);
        GalleryConfig GetGalleryConfig(string galleryName);
    }
}
