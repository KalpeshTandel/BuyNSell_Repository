using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using BuyNSell.Models.MetaData;



namespace BuyNSell.Models
{

[MetadataType(typeof(UserMaster_MetaData))]
    public partial class UserMaster
    {
        public string ConfirmPassword { get; set; }

    }
}