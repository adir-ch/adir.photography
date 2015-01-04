using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotosRepository; 

namespace adir.photography.Models
{
    public class HomeGalleryModel
    {
        public string OpeningImage { get; set; }
        public IEnumerable<string> MainGalleryImages { get; set; }

        public HomeGalleryModel(string serverRunningPath)
        {

            PhotoRepository repo = new PhotoRepository(serverRunningPath); 
            OpeningImage = repo.GetMainGalleryOpeningPhotos("main");
            MainGalleryImages = repo.GetMainGalleryPhotos("main"); 
        }
    }
}