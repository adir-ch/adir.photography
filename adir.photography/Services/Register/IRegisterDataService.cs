
using System; 
using adir.photography.Models; 

namespace adir.photography.Services.Register 
{
    public interface IRegisterDataService
    {
        bool AddNewInfoSubscriber(string emailAddress);
        bool AddNewNewsLetterSubscriber(RegisterInfoModel subscriberInfo);
    }
}