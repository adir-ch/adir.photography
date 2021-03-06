﻿
namespace PhotosRepository.Model
{
    public interface IPhotoMetadata
    {
        int Width { get; set; }
        int Height { get; set; }
        bool InitMetadata(int width, int height);
        bool InitMetadata(string width, string height);
        string GetMetadataAsString();
    }
}
