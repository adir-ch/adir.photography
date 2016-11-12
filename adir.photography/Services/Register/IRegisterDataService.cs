
using System; 
using adir.photography.Models; 

namespace adir.photography.Services.Register 
{
    public interface IRegisterDataService
    {
        int AddNewInfoSubscriber(string emailAddress);
        int AddNewNewsLetterSubscriber(RegisterInfoModel subscriberInfo);
    }
}