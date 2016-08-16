using adir.photography.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adir.photography.Services.EmailSender
{
    public interface IEmailSendingService
    {
        bool SendEmail(ContactFormModel contactFormInputData);
    }
}
