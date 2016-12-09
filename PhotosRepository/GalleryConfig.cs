using PhotosRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PhotosRepository
{
    public class GalleryConfig : IDBEntity
    {

        public string EntityName { get; set; }
        public string EntityDescription { get; set; }

        public string Name { get; set; }
        public int TimeOut { get; set; }
        public bool AutoCycle { get; set; }
        public string PhotosLocation { get; set; }

        public GalleryConfig()
        {
            EntityName = "GalleryConfigEntity";
            EntityDescription = "Gallery configuration parameters";
            //Init(inputData); 
        }

        public bool IsDBUpdateNeeded()
        {
            return false; 
        }

        //private void Init(XElement inputData) 
        //{
        //    Name = inputData.Element("name").Value;
        //    TimeOut = inputData.Element("name").Value; 
        //}
    }
}
