using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace BuyNSell.Models
{
    public class RatingMaster_MetaData
    {

       [Required]
        public Nullable<int> Rate { get; set; }

    }
}