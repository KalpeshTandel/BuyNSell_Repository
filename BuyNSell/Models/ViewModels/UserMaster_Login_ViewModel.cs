using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using BuyNSell.Models.MetaData;

namespace BuyNSell.Models.ViewModels
{
    public class UserMaster_Login_ViewModel
    {

        [Required(ErrorMessage = "*")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Password { get; set; }

    }
}