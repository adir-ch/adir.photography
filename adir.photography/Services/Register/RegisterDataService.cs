
using System; 
using adir.photography.Models;
using System.Threading.Tasks; 

namespace adir.photography.Services.Register 
{
    public class RegisterDataService : IRegisterDataService  
    {
        public int AddNewInfoSubscriber(string emailAddress)
        {
            // write data to file and return true if amount of data written to file equals to the length of the email 
            return 0; 
        }

        public int AddNewNewsLetterSubscriber(RegisterInfoModel subscriberInfo)
        {
            throw new NotImplementedException(); 
        }
    }
}