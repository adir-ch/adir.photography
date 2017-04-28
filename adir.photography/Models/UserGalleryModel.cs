using PhotosRepository;
using PhotosRepository.Model;
using System.Collections.Generic;
using System.Runtime.Serialization; 

namespace adir.photography.Models
{
    [KnownType(typeof(Photo))]
    [KnownType(typeof(PhotoMetadata))]
    [DataContract] 
    public class UserGalleryModel
    {
        public UserGalleryModel()
        {

        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string OpeningPhoto { get; set; }

        [DataMember]
        public IEnumerable<IPhoto> GalleryPhotos { get; set; }

        [DataMember]
        public int Timeout { get; set; }
        
        [DataMember]
        public bool AutoCycle { get; set; }

        [DataMember]
        public string ImagesLocation { get; set; }
    }
}