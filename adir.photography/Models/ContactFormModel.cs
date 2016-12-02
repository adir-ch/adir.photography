using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace adir.photography.Models
{
    public class ContactFormModel
    {
        [Required]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        [Required]
        public string Message { get; set; }

        public void Clear()
        {
            FirstName = "";
            LastName = "";
            EmailAddress = "";
            PhoneNumber = "";
            City = "";
            Country = "";
            Message = "";
        }
    }
}