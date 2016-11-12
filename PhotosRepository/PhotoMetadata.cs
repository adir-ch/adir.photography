using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using log4net;
using System.Runtime.Serialization; 

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

        public bool TryParseMetadataFromFile(string path, string fileName)
        {
            bool status = false; 
            Bitmap image;

            try 
	        {	        
                image = new Bitmap(path + fileName, true);
                Width = image.Width;
                Height = image.Height;
                //_log.DebugFormat("Photo metadata retrived successfully: {0}", GetMetadataAsString());
                status = true; 
	        }
	        catch (Exception e)
	        {
                _log.ErrorFormat("Photo metadata parse failed with  {0}", e);
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
