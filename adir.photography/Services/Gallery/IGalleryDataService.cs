using adir.photography.Models;
using PhotosRepository.Model;
using System.Collections.Generic;

namespace adir.photography.Services.Gallery
{
    public interface IGalleryDataService
    {
        UserGalleryModel GetGalleryData(string galleryName);
        IEnumerable<UserGalleryModel> GetAllGalleries();
        IEnumerable<IPhoto> InitPhotos();
    }
}
