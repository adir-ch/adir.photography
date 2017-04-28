
namespace PhotosRepository.Model
{
    public interface IDBEntity
    {
        string EntityName { get; set; }
        string EntityDescription { get; set; }
        bool IsDBUpdateNeeded(); 
    }
}
