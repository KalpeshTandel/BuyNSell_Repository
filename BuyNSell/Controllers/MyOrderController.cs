using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class MyOrderController : Controller
    {
        BuyNSell_DbEntities objDataBase = new BuyNSell_DbEntities();
        // GET: MyOrder
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetMyOrderList()
        {
            int UserId = Convert.ToInt16(Session["UserId"]);

            var FullOrderList = objDataBase.OrderMasters.Join(objDataBase.ProductMasters,o => o.ProductId,p => p.ProductId,(o,p)=>new {
                p.
            })
        }

    }
}