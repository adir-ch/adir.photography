using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotosRepository;

namespace adir.photography.Services.Gallery
{
    public interface IGalleryDataService
    {
        IEnumerable<string> GetGalleryPhotos(string galleryName);
        string GetGalleryOpeningPhoto(string galleryName);
        GalleryConfig GetGalleryConfig(string galleryName);
    }
}
