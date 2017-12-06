using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace BuyNSell.Models
{
    public class OrderMaster_MetaData
    {
        [Required(ErrorMessage ="*")]
        public Nullable<int> OrderQuantity { get; set; }

        [Required(ErrorMessage ="*")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage ="*")]
        public string ContactNum { get; set; }

    }
}