using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Runtime.Serialization; 

namespace PhotosRepository
{
    public class Photo : IPhoto
    {
        private readonly ILog _log = LogManager.GetLogger("PhotoReprository");
        public string FileName { get; set; }
        //public string FilePath { get; set; }
        public string Caption { get; set; }
        public IPhotoMetadata Metadata { get; set; }
        public List<string> Tags { get; set; }
        
        public Photo()
        {
            Metadata = new PhotoMetadata();
            Tags = new List<string>(); 
        }

        public Photo(string filePath, string fileName) : this()
        {
            FileName = fileName;
            //FilePath = filePath; 
        }
        
        public bool Init(string filePath)
        {
            return Metadata.TryParseMetadataFromImage(filePath, FileName);
        }

        public bool Init(string filePath, string width, string height)
        {
            if (Metadata.TryParseMetadataFromData(width, height) == true)
            {
                return true; 
            }

            return Metadata.TryParseMetadataFromImage(filePath, FileName);
        }
        public void AddTag(string iTag)
        {
            //_log.DebugFormat("Adding tag to {0}: {1}", FileName, iTag); 

            if (Tags.Contains(iTag) == false)
                Tags.Add(iTag); 
        }
    }
}
