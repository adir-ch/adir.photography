
using adir.photography.Models;
using adir.photography.Services.EmailSender;
using System; 

namespace adir.photography.Services.Register 
{
    public class RegisterDataService : IRegisterDataService  
    {
        public bool AddNewInfoSubscriber(string emailAddress)
        {
            ContactFormModel model = new ContactFormModel(); 
            model.EmailAddress = emailAddress;
            model.Message = "New subscriber request";
            model.FirstName = "Subscriber";
            model.LastName = "Request";

            EmailSendingService sender = new EmailSendingService();
            return sender.SendEmail(model); 
        }

        public bool AddNewNewsLetterSubscriber(RegisterInfoModel subscriberInfo)
        {
            throw new NotImplementedException(); 
        }
    }
}