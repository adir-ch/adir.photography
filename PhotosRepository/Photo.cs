using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net; 

namespace PhotosRepository
{
    public class Photo
    {
        private readonly ILog _log = LogManager.GetLogger("repository"); 
        private List<string> _Tags = new List<string>();
        
        public string FileName { get; set; }
        public string Caption { get; set; }

        public IEnumerable<string> GetTags()
        {
            return _Tags; 
        }

        public void SetTag(string iTag)
        {
            _log.DebugFormat("Adding tag to {0}: {1}", FileName, iTag); 

            if (_Tags.Contains(iTag) == false)
                _Tags.Add(iTag); 
        }
    }
}
