﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhotosRepository;
using System.Runtime.Serialization;
using PhotosRepository.Model; 

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