using System.Collections.Generic;

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
