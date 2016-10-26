using System;
using System.Collections.Generic;
using PhotosRepository;
using adir.photography.Services.WebSiteConfig;
using adir.photography.Models;
using PhotosRepository.DataAccess;
using PhotosRepository.DataAcess.XML;

namespace adir.photography.Services.Gallery
{
    public class GalleryDataService : IGalleryDataService
    {
        private IPhotosRepository _repo; // TODO: inject as singleton
        private IWebSiteConfigService _siteConfig; // TODO: inject as singleton

        public GalleryDataService() : this(XMLPhotoRepositoryDB.GetInstance(), WebSiteFileConfigService.Instance())
        {

        }

        public GalleryDataService(IPhotosRepository repo, IWebSiteConfigService siteConfig)
        {
            _repo = repo;
            _repo.Init(); 
            _siteConfig = siteConfig; 
        }

        public UserGalleryModel GetGalleryData(string galleryName)
        {
            UserGalleryModel galleryModel = new UserGalleryModel();

            galleryModel.Name = galleryName; 
            galleryModel.OpeningPhoto = GetGalleryOpeningPhoto(galleryName);
            galleryModel.GalleryPhotos = GetGalleryPhotos(galleryName);
            galleryModel.Timeout = GetGalleryConfig(galleryName).TimeOut;
            galleryModel.AutoCycle = GetGalleryConfig(galleryName).AutoCycle;
            galleryModel.ImagesLocation = GetGalleryConfig(galleryName).PhotosLocation;
            return galleryModel;
        }

        public IEnumerable<UserGalleryModel> GetAllGalleries()
        {
            List<UserGalleryModel> allGalleries = new List<UserGalleryModel>(); 
            var galleries = _repo.GetAllGalleries(); 
            foreach(var gallery in galleries)
            {
                allGalleries.Add(new UserGalleryModel
                {
                    Name = gallery.Name, 
                    OpeningPhoto = GetGalleryOpeningPhoto(gallery.Name), 
                    ImagesLocation = GetGalleryConfig(gallery.Name).PhotosLocation
                });
            }

            return allGalleries; 
        }

        private IEnumerable<IPhoto> GetGalleryPhotos(string galleryName)
        {
            return _repo.GetGalleryPhotos(galleryName);
        }

        private string GetGalleryOpeningPhoto(string galleryName)
        {
            return _repo.GetGalleryOpeningPhoto(galleryName);
        }

        private GalleryConfig GetGalleryConfig(string galleryName)
        {
            GalleryConfig config = _repo.GetGalleryConfig(galleryName);
            config.PhotosLocation = _siteConfig.PhotosLocation;
            return config;
        }
    }
}