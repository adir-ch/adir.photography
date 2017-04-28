using adir.photography.Models;
using log4net;
using System;
using System.Net.Mail;
using System.Text;

namespace adir.photography.Services.EmailSender
{
    public class EmailSendingService : IEmailSendingService
    {
        private const string _to = "adir.work@gmail.com"; // TODO: take from DB  
        private const string _from = "info@adir.photography"; // TODO: take from DB  
        //private static bool _asyncEmailSendStatus; 
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool SendEmail(ContactFormModel contactFormInputData)
        {
            if (ValidateInputData(contactFormInputData) == false)
            {
                return false; 
            }

            var message = BuildEmailMessage(contactFormInputData);
            bool status = SMTPSendEmail(message);
            _log.DebugFormat("SMTP send mail: {0}", (status ? "Successful" : "Failed"));
            message.Dispose();
            return status;
        }

        private bool ValidateInputData(ContactFormModel contactFormInputData)
        {
            if (String.IsNullOrEmpty(contactFormInputData.EmailAddress))
            {
                contactFormInputData.EmailAddress = "none@given.com";
            }

            if (String.IsNullOrEmpty(contactFormInputData.FirstName))
            {
                contactFormInputData.FirstName = "n/a";
            }

            if (String.IsNullOrEmpty(contactFormInputData.LastName))
            {
                contactFormInputData.LastName = "n/a";
            }

            if (String.IsNullOrEmpty(contactFormInputData.City))
            {
                contactFormInputData.City = "n/a";
            }

            if (String.IsNullOrEmpty(contactFormInputData.Country))
            {
                contactFormInputData.Country = "n/a";
            }

            if (String.IsNullOrEmpty(contactFormInputData.Message))
            {
                return false; 
            }

            return true; 
        }

        private MailMessage BuildEmailMessage(ContactFormModel contactFormInputData)
        {
            _log.DebugFormat("Building contact form message from: {0}", contactFormInputData.EmailAddress);
            string messageSubject = "New contact form submit from adir.photography";
            var messageBody = string.Format("Comment From: <b>{1} {2}{0}</b><br><br>Email: <b>{3}</b> {0}<br>Location: <b>{4}, {5}</b><br><br>{0}Comment:<br>{6}",
                                            Environment.NewLine,
                                            contactFormInputData.FirstName,
                                            contactFormInputData.LastName,
                                            contactFormInputData.EmailAddress,
                                            contactFormInputData.City,
                                            contactFormInputData.Country,
                                            contactFormInputData.Message);

            MailMessage mailMessage = new MailMessage(_from, _to);
            mailMessage.BodyEncoding = Encoding.UTF8; 
            //mailMessage.Sender = new MailAddress(contactFormInputData.EmailAddress);
            mailMessage.Subject = messageSubject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;

            return mailMessage;
        }

        private bool SMTPSendEmail(MailMessage message)
        {
            bool status = false;

            using (var smtp = new SmtpClient())
            {
                // settings are taken from web.config
                //smtp.Host = "smtp.gmail.com";
                //smtp.Credentials = new System.Net.NetworkCredential("adir.work@gmail.com", "****");
                //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                smtp.EnableSsl = false;

                try
                {
                    smtp.Send(message); // not doing async for now
                    _log.DebugFormat("email sent successfuly");
                    status = true;
                }
                catch (Exception e)
                {
                    _log.ErrorFormat("Error sending email: {0}", e.ToString());
                }
            }
            return status;
        }

        //private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        //{
        //    // Get the unique identifier for this asynchronous operation.
        //    String token = (string)e.UserState;

        //    if (e.Error != null)
        //    {
        //        _log.ErrorFormat("Error sending email: {0}", e.Error.ToString());
        //    }
        //    else
        //    {
        //        _log.DebugFormat("email sent successfuly (id: {0})", token);
        //    }
        //    _asyncEmailSendStatus = true;
        //}
    }
}