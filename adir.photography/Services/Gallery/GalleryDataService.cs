using System;
using System.Collections.Generic;
using PhotosRepository;
using adir.photography.Services.WebSiteConfig;

namespace adir.photography.Services.Gallery
{
    public class GalleryDataService : IGalleryDataService
    {
        private IPhotosRepository _repo; // TODO: inject as singleton
        private IWebSiteConfigService _siteConfig; // TODO: inject as singleton

        public GalleryDataService() : this(new XMLPhotoRepositoryDB(), WebSiteFileConfigService.Instance())
        {

        }

        public GalleryDataService(IPhotosRepository repo, IWebSiteConfigService siteConfig)
        {
            _repo = repo; 
            _siteConfig = siteConfig; 
        }

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
            GalleryConfig config = _repo.GetGalleryConfig(galleryName); 
            config.PhotosLocation = _siteConfig.PhotosLocation;
            return config;
        }
    }
}