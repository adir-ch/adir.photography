using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhotosRepository
{
    public interface IPhotoMetadata
    {
        int Width { get; set; }
        int Height { get; set; }
        bool TryParseMetadataFromFile(string path, string fileName);
        string GetMetadataAsString();
    }
}
