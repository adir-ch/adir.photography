using System;
using System.Collections.Generic;
using PhotosRepository;

namespace adir.photography.Services.Gallery
{
    public class GalleryDataService
    {
        private IPhotosRepository _repo = new XMLPhotoRepositoryDB(); // TODO: inject as singleton

        public IEnumerable<string> GetGalleryPhotos(string galleryName)
        {
            return _repo.GetGalleryPhotos(galleryName);
        }

        public string GetGalleryOpeningPhoto(string galleryName)
        {
            return _repo.GetGalleryOpeningPhoto(galleryName);
        }

        public GalleryConfig GetGalleryConfig(string galleryName)
        {
            return _repo.GetGalleryConfig(galleryName); 
        }
    }
}