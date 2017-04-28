using adir.photography.Models;

namespace adir.photography.Services.EmailSender
{
    public interface IEmailSendingService
    {
        bool SendEmail(ContactFormModel contactFormInputData);
    }
}
