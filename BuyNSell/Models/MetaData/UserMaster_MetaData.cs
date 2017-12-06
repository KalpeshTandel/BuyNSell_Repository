using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace BuyNSell.Models
{
    public class UserMaster_MetaData
    {
        [Required(ErrorMessage = "*")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "*")]
        public string LastName { get; set; }

        [EmailIdExistOrNot]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid Email Id.")]
        public string EmailId { get; set; }

        
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Required(ErrorMessage = "*")]
        public string MobileNum { get; set; }


        [Required(ErrorMessage = "*")]
        public string Password { get; set; }


        [Required(ErrorMessage = "*")]
        [Compare("Password",ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }


    }
}