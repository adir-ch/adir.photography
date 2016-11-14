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

        public bool TryParseMetadataFromImage(string path, string fileName)
        {
            bool status = false; 
            Bitmap image;

            try 
	        {	        
                image = new Bitmap(path + fileName, true);
                Width = image.Width;
                Height = image.Height;
                //_log.DebugFormat("Photo metadata retrieved successfully: {0}", GetMetadataAsString());
                status = true; 
	        }
	        catch (Exception e)
	        {
                _log.ErrorFormat("Photo metadata parse failed with  {0}", e);
	        }
            return status; 
        }
        public bool TryParseMetadataFromData(string width, string height)
        {
            int parsedWidth = 0, parsedHeight = 0; 
            bool status = false; 
            try 
            {
                status = Int32.TryParse(width, out parsedWidth); 
                status = status & Int32.TryParse(height, out parsedHeight);
            }
            catch(Exception e) 
            {
                _log.ErrorFormat("Exception while trying to parse the photo metadata from DB: {0}", e);
            }
            if(status == true) 
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
