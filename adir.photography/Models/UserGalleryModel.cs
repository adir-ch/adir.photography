using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotosRepository; 

namespace adir.photography.Models
{
    public class UserGalleryModel
    {
        public string Name { get; set; }
        public string OpeningPhoto { get; set; }
        public IEnumerable<string> GalleryPhotos { get; set; }
        public int Timeout { get; set; }
        public bool AutoCycle { get; set; }
        public string ImagesLocation { get; set; }
    }
}