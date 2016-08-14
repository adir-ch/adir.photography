using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adir.photography.Models
{
    public class ContactFormModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Message { get; set; }
    }
}