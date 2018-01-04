﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyNSell.Models
{
    public class OrderList_ViewModel
    {
        public int OrderId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> OrderQuantity { get; set; }
        public Nullable<int> PaymentAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string ContactNum { get; set; }
        public Nullable<System.DateTime> OrderAddedDate { get; set; }
        public Nullable<int> OrderStatusId { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Deleted { get; set; }


    }
}