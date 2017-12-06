using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace BuyNSell.Models
{
    public class ProductMaster_MetaData
    {

        [Required(ErrorMessage ="*")]
        public string ProductName { get; set; }


        [Required(ErrorMessage ="*")]
        public string ProductDescription { get; set; }


        [Required(ErrorMessage ="*")]
        public Nullable<int> ProductCategoryId { get; set; }


        [Required(ErrorMessage ="*")]
        [Range(1,100,ErrorMessage = "Quantity must be 1 to 100")]
        public Nullable<int> Quantity { get; set; }


        [Required(ErrorMessage ="*")]
        public Nullable<int> Price { get; set; }




    }
}