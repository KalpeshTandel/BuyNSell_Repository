//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BuyNSell.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderMaster
    {
        public int OrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> OrderQuantity { get; set; }
        public Nullable<int> PaymentAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNum { get; set; }
        public Nullable<int> OrderStatusId { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> OrderAddedDate { get; set; }
        public Nullable<int> IsNew { get; set; }
        public Nullable<System.DateTime> OrderModifiedDate { get; set; }
    }
}
