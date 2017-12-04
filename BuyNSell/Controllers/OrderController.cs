using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyNSell.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult AddOrder(int ProductId)
        {
            return PartialView("_AddOrder");
        }
    }
}