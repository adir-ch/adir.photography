using System.Collections.Generic;

namespace PhotosRepository.Model
{
    public interface IPhoto
    {
        string FileName { get; set; }
        string Title {get; set;}
        string Caption { get; set; }
        IPhotoMetadata Metadata { get; set; }
        List<string> Tags { get; set; }

        bool Init(string filePath, 
                  string fileName, 
                  string title, 
                  string caption, 
                  IPhotoMetadata metadata, 
                  List<string> tags);

        void AddTag(string iTag);
    }
}
