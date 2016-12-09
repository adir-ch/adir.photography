using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using log4net;
using System.Runtime.Serialization;
using PhotosRepository.Model; 

namespace PhotosRepository
{
    public class PhotoMetadata : IPhotoMetadata
    {
        private static readonly ILog _log = LogManager.GetLogger("PhotoReprository");
        public int Width { get; set; }
        public int Height { get; set; }

        public PhotoMetadata()
        {
            Width = 0;
            Height = 0;
        }

        public bool InitMetadata(int width, int height)
        {
            Width = width;
            Height = height;
            return true;
        }

        public bool InitMetadata(string width, string height)
        {
            bool status = false; 
            int parsedWidth = 0, parsedHeight = 0;

            try
            {
                status = Int32.TryParse(width, out parsedWidth);
                status = status & Int32.TryParse(height, out parsedHeight);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Exception while trying to parse the photo metadata from DB: {0}", e);
            }
            if (status == true)
            {
                Width = parsedWidth;
                Height = parsedHeight;
                _log.DebugFormat("Photo already has metadata in DB: {0}", GetMetadataAsString());
            }
            else
            {
                _log.Error("Unable to parse the photo metadata from DB");
            }

            return status; 
        }

        public string GetMetadataAsString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Photo Metadata: ");
            result.Append("dimensions: " + Width + "x" + Height);
            return result.ToString(); 
        }
    }
}
