using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adir.photography.Services.EmailSender
{
    public class EmailSendingService : IEmailSendingService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool SendEmail(string from, string to, string messageSubject, string messageBody)
        {
            _log.DebugFormat("Sending message from contact form: {0}", messageBody); 
            return true;
        }
    }
}