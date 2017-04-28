
namespace PhotosRepository.Model
{
    public interface IPhotoDBEntity : IDBEntity
    {
        //string GetFileName();
        //string GetTitle();
        //string GetCaption();
        //List<string> GetTags();
        //IPhotoMetadata GetMetadata();
        //bool IsDBUpdateNeeded(); 

        IPhoto GetPhoto(string filePath, bool forceUpdateFromImageFile = false); 
    }
}
