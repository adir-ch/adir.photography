using log4net;
using PhotosRepository.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PhotosRepository.DataAccess.XML
{
    /*** 
     * Since I don't use and ORM framework, this class actually mimics the ORM object!!!
     * For example in NHibernate or EF, this will be the Entity representation class
     */
    public class XMLPhotoDBEntity : IPhotoDBEntity 
    {
        private static readonly ILog _log = LogManager.GetLogger("PhotoReprository"); //System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private XElement _dbEntry;

        public string EntityName { get; set; }
        public string EntityDescription { get; set; }

        private XElement _fileName;
        private XElement _title;
        private XElement _caption; 
        private XElement _width;
        private XElement _height;
        private XElement _tagsElemet;
        
        private List<string> _tags;
        private PhotoMetadata _metadata;
        private bool _dbDataUpdateNeeded; 

        public XMLPhotoDBEntity(XElement dbEntry)
        {
            EntityName = "PhotoEntity";
            EntityDescription = "Represents a photo";

            _dbEntry = dbEntry;
            InitPhotoData(dbEntry);
        }

        private void InitPhotoData(XElement dbEntry)
        {
            _dbDataUpdateNeeded = false; 

            _fileName = dbEntry.Element("filename");
            _width = dbEntry.Element("metadata").Element("width");
            _height = dbEntry.Element("metadata").Element("height");
            _title = dbEntry.Element("title");
            _caption = dbEntry.Element("caption");

            _metadata = new PhotoMetadata();
            _metadata.InitMetadata(_width.Value, _height.Value);
            _tagsElemet = dbEntry.Element("tags");
            _tags = _tagsElemet.Descendants().Select(tag => tag.Value).ToList();
        }

        public IPhoto GetPhoto(string filePath, bool forceUpdateFromImageFile = false)
        {
            var photo = new Photo(_fileName.Value);
            bool forceDataReRead = forceUpdateFromImageFile; 
            bool initStatus = false; 

            if (forceDataReRead == false)
            {
                forceDataReRead = IsUpdateNeeded(); 
            }

            if (forceDataReRead == true)
            {
                initStatus = photo.Init(filePath);
                UpdateDBEntityData(photo); // check if there is an update from the image file Exif
            }
            
            initStatus = photo.Init(filePath, 
                                    _fileName.Value, 
                                    _title.Value, 
                                    _caption.Value, 
                                    _metadata, 
                                    _tags);

            if (initStatus == false)
            {
                _log.ErrorFormat("Unable to init photo {0}", _fileName.Value); 
                return null;
            }

            return photo;
        }

        public bool IsDBUpdateNeeded()
        {
            return _dbDataUpdateNeeded; 
        }

        private void UpdateDBEntityData(IPhoto photo)
        {
            // Keep XML configuration if exist 
            if (String.IsNullOrEmpty(_width.Value))
            {
                if (photo.Metadata.Width != 0)
                {
                    _width.Value = Convert.ToString(photo.Metadata.Width);
                    _dbDataUpdateNeeded = true; 
                }
            }

            if (String.IsNullOrEmpty(_height.Value))
            {
                if (photo.Metadata.Height != 0)
                {
                    _height.Value = Convert.ToString(photo.Metadata.Height);
                    _dbDataUpdateNeeded = true;
                }
            }

            // Check if empty or default 
            if (String.IsNullOrEmpty(_title.Value) || (_title.Value == Path.GetFileNameWithoutExtension(_fileName.Value)))
            {
                if (String.IsNullOrEmpty(photo.Title) == false)
                {
                    _title.Value = photo.Title; 
                    _dbDataUpdateNeeded = true;
                } 
                else
                {
                    // not data in both file and DB - use filename 
                    _title.Value = Path.GetFileNameWithoutExtension(_fileName.Value); 
                }
            }

            if (String.IsNullOrEmpty(_caption.Value) || (_caption.Value == Path.GetFileNameWithoutExtension(_fileName.Value)))
            {
                if (String.IsNullOrEmpty(photo.Caption) == false)
                {
                    _caption.Value = photo.Caption;
                    _dbDataUpdateNeeded = true;
                }
                else
                {
                    // not data in both file and DB - use filename 
                    _caption.Value = Path.GetFileNameWithoutExtension(_fileName.Value);
                }
            }

            // merge tags 
            foreach (var tag in photo.Tags)
            {
                if (_tags.Contains(tag) == false)
                {
                    _tagsElemet.Add(new XElement("tag", tag));
                    _dbDataUpdateNeeded = true;
                }
            }
        }

        private bool IsUpdateNeeded()
        {
            return (String.IsNullOrEmpty(_title.Value) ||
                    String.IsNullOrEmpty(_caption.Value) ||
                    String.IsNullOrEmpty(_width.Value) ||
                    String.IsNullOrEmpty(_height.Value) || 
                    (_tags != null && _tags.Count() == 0)); 
        }
    }
}
