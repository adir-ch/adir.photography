using log4net;
using PhotosRepository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging; 

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
        }

        public bool Init(string filePath)
        {
            bool status = false;
            try
            {
                ParsePhotoMetadata(filePath);
                status = true; 
            }
            catch (Exception e)
            {
                _log.ErrorFormat("Exception while getting data from image file {0}: {1}", FileName, e.Message); 
            }

            return status; 
        }

        public bool Init(string filePath, 
                         string fileName, 
                         string title, 
                         string caption, 
                         IPhotoMetadata metadata, 
                         List<string> tags)
        {
            Title = title;
            Caption = caption;
            Tags = tags;
            Metadata = metadata; 
            return true; 
        }

        public void AddTag(string iTag)
        {
            //_log.DebugFormat("Adding tag to {0}: {1}", FileName, iTag); 

            if (Tags.Contains(iTag) == false)
                Tags.Add(iTag); 
        }

        public void ParsePhotoMetadata(string path)
        {
            using (var stream = new System.IO.FileStream((path + FileName), FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                BitmapSource bitmapStream = BitmapFrame.Create(stream);
                Metadata.InitMetadata(bitmapStream.PixelWidth, bitmapStream.PixelHeight);
                BitmapMetadata meta = (BitmapMetadata)bitmapStream.Metadata;
                Title = meta.Title;
                Caption = meta.Comment ?? String.Empty;
                List<string> keys = new List<string>(meta.Keywords);

                foreach (var key in keys)
                {
                    AddTag(key); 
                }
            }
        }

        //private void TryGetDataFromImageFile(string path) // not used anymore 
        //{
        //    var ascii = new System.Text.ASCIIEncoding();

        //    // Create an Image object. 
        //    using (var image = new Bitmap(path + FileName))
        //    {
        //        Metadata.InitMetadata(image.Width, image.Height); // init meta-data (dimensions)

        //        foreach (PropertyItem propertyItem in image.PropertyItems)
        //        {
        //            var propertyData = image.GetPropertyItem(propertyItem.Id);

        //            switch (propertyItem.Id) 
        //            {
        //                case 270: // title 
        //                    {
        //                        Title = ascii.GetString(propertyData.Value).Replace("\0", string.Empty);
        //                        break; 
        //                    }
        //                case 40094: // tags 
        //                    {
        //                        var keywords = Encoding.Unicode.GetString(propertyData.Value).Replace("\0", string.Empty);
        //                        List<string> keys = new List<string>(keywords.Split(';').ToList());
        //                        foreach (var key in keys)
        //                        {
        //                            AddTag(key); 
        //                        }

        //                        break; 
        //                    }
        //                case 40092: // caption 
        //                    {
        //                        Caption = Encoding.Unicode.GetString(propertyData.Value).Replace("\0", string.Empty);
        //                        break; 
        //                    }
        //            }
        //        }

        //        //if (imageProperties.Where(i => i.Id == 270).Count() > 0) // can also be done with this
        //        //{
        //        //    Title = ascii.GetString(image.GetPropertyItem(270).Value);
        //        //}
        //    }
        //}
    }
}
