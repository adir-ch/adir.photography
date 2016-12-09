using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotosRepository;
using adir.photography.Models;
using PhotosRepository.Model;

namespace adir.photography.Services.Gallery
{
    public interface IGalleryDataService
    {
        UserGalleryModel GetGalleryData(string galleryName);
        IEnumerable<UserGalleryModel> GetAllGalleries();
        IEnumerable<IPhoto> InitPhotos();
    }
}
