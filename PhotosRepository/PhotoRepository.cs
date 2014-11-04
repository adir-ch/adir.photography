using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public class PhotoRepository
    {
        private readonly string _path = "~/Content/images/";
        private IPhotoRepositoryDB _db; 

        public PhotoRepository()
        {
            // Temporary repository 

            _db = new List<Photo> { new Photo { FileName = "DSC_0193.jpg" },
                                    new Photo { FileName = "DSC_1657-Edit.jpg" }, 
                                    new Photo { FileName = "DSC_1707_HDR.jpg" }, 
                                    new Photo { FileName = "DSC_5690.jpg" }};

            _db[0].SetTag(PhotoTag.MainGalleryOpening);
            
            var photos = _db.Where(p => p.GetTags().Contains(PhotoTag.MainGalleryOpening) == false); 
            foreach (var p in photos)
                p.SetTag(PhotoTag.MainGallery);
        }

        public IEnumerable<Photo> GetMainGalleryPhotos()
        {
            return _db.Where(p => p.GetTags().Contains(PhotoTag.MainGallery)); 
        }

        public Photo GetMainGalleryOpeningPhotos()
        {
            return _db.Where(p => p.GetTags().Contains(PhotoTag.MainGalleryOpening)).FirstOrDefault(); 
        }
    }
}