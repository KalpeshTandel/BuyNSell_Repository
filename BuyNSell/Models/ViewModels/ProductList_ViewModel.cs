using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyNSell.Models
{
    public class ProductList_ViewModel
    {
        public int SrNo { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> ProductCategoryId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public int PictureId { get; set; }
        public byte[] PictureContent { get; set; }
        public Nullable<int> Price { get; set; }


        public string UserName { get; set; }
        public string ProductCategoryName { get; set; }

        //public Int32 TotalRecords { get; set; }


    }
}