using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        //public ActionResult Index()
        //{
        //    return View();
        //}
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        public void StoreNotificationCountInSession(RequestContext context)
        {
            //IsNew Flag in OrderMaster Meaning --> 1 -New for Buyer, 2 -New for Seller, 0 -Normal
            try
            {
                base.Initialize(context);

                if (Session["UserId"] != null)
                {
                    int UserId = Convert.ToInt16(Session["UserId"]);
                    Session["NotificationMyOrder"] = objDbEntities.OrderMasters.Where(o => o.UserId == UserId && (o.NotificationStatusId == 2 || o.NotificationStatusId ==3)).Count();
                    Session["NotificationCustomerOrder"] = objDbEntities.OrderMasters.Join(objDbEntities.ProductMasters, o => o.ProductId, p => p.ProductId, (o, p) => new { o, p }).Where(op => op.p.UserId == UserId && op.o.NotificationStatusId == 2).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}