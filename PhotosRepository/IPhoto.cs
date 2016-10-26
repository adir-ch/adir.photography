using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public interface IPhoto
    {
        string FileName { get; set; }
        string Caption { get; set; }
        IPhotoMetadata Metadata { get; set; }
        List<string> Tags { get; set; }

        void AddTag(string iTag);
    }
}
