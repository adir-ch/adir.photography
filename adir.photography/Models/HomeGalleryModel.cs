using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotosRepository; 

namespace adir.photography.Models
{
    public class HomeGalleryModel
    {
        public Photo OpeningImage { get; set; }
        public IEnumerable<Photo> MainGalleryImages { get; set; }

        public HomeGalleryModel()
        {
            PhotoRepository repo = new PhotoRepository(); 
            OpeningImage = repo.GetMainGalleryOpeningPhotos();
            MainGalleryImages = repo.GetMainGalleryPhotos(); 
        }
    }
}