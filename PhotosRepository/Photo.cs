using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Runtime.Serialization;
using System.Drawing;
using System.Drawing.Imaging; 

namespace PhotosRepository
{
    public class Photo : IPhoto
    {
        private readonly ILog _log = LogManager.GetLogger("PhotoReprository");
        public string FileName { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public IPhotoMetadata Metadata { get; set; }
        public List<string> Tags { get; set; }
        
        public Photo()
        {
            Metadata = new PhotoMetadata();
            Tags = new List<string>(); 
        }

        public Photo(string fileName) : this()
        {
            FileName = fileName;
            //FilePath = filePath; 
        }

        public bool Init(string width, string height)
        {
            return Metadata.InitMetadata(width, height);
        }

        public bool Init(string filePath)
        {
            bool status = false;
            try
            {
                TryGetDataFromImageFile(filePath);
                status = true; 
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Exception while getting data from image file {0}: {1}", FileName, e.Message); 
            }

            return status; 
        }

        public void AddTag(string iTag)
        {
            //_log.DebugFormat("Adding tag to {0}: {1}", FileName, iTag); 

            if (Tags.Contains(iTag) == false)
                Tags.Add(iTag); 
        }

        private void TryGetDataFromImageFile(string path)
        {
            var ascii = new System.Text.ASCIIEncoding();

            // Create an Image object. 
            using (var image = new Bitmap(path + FileName))
            {
                Metadata.InitMetadata(image.Width, image.Height); // init meta-data (dimensions)

                foreach (PropertyItem propertyItem in image.PropertyItems)
                {
                    var propertyData = image.GetPropertyItem(propertyItem.Id);

                    switch (propertyItem.Id) 
                    {
                        case 270: // title 
                            {
                                Title = ascii.GetString(propertyData.Value).Replace("\0", string.Empty);
                                break; 
                            }
                        case 40094: // tags 
                            {
                                var keywords = Encoding.Unicode.GetString(propertyData.Value).Replace("\0", string.Empty);
                                List<string> keys = new List<string>(keywords.Split(';').ToList());
                                foreach (var key in keys)
                                {
                                    AddTag(key); 
                                }

                                break; 
                            }
                        case 40092: // caption 
                            {
                                Caption = Encoding.Unicode.GetString(propertyData.Value).Replace("\0", string.Empty);
                                break; 
                            }
                    }
                }

                //if (imageProperties.Where(i => i.Id == 270).Count() > 0) // can also be done with this
                //{
                //    Title = ascii.GetString(image.GetPropertyItem(270).Value);
                //}
            }
        }
    }
}
