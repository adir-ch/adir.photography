using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public class GalleryConfig
    {
        public string Name { get; set; }
        public int TimeOut { get; set; }
        public bool AutoCycle { get; set; }
        public string PhotosLocation { get; set; }
    }
}
