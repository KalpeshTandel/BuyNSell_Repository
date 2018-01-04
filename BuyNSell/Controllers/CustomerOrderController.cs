using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class CustomerOrderController : Controller
    {
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: CustomerOrder
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetOrderList()
        {
            int UserId = Convert.ToInt16(Session["UserId"]);
            List<OrderList_ViewModel> OrderList = new List<OrderList_ViewModel>();
            //OrderList = objDbEntities.OrderMasters.Where(p => p.UserId == 1).ToList();

            var Result = (from om in objDbEntities.OrderMasters
                          join pm in objDbEntities.ProductMasters on om.ProductId equals pm.ProductId
                          join um in objDbEntities.UserMasters on om.UserId equals um.UserId
                          where pm.UserId == UserId
                          orderby om.OrderAddedDate descending
                          select new
                          {
                              OrderId = om.OrderId,
                              ProductId = om.ProductId,
                              ProductName = pm.ProductName,
                              CustomerId = um.UserId,
                              CustomerFirstName = um.FirstName,
                              CustomerLastName = um.LastName,
                              OrderQuantity = om.OrderQuantity,
                              PaymentAmount = om.PaymentAmount,
                              DeliveryAddress = om.DeliveryAddress,
                              ContactNum = om.ContactNum,
                              OrderAddedDate = om.OrderAddedDate,
                              OrderStatusId = om.OrderStatusId,
                              Active = om.Active,
                              Deleted = om.Deleted
                          }).ToList();

           foreach(var item in Result)
            {
                OrderList.Add(new OrderList_ViewModel()
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerFirstName+' '+item.CustomerLastName,
                    OrderQuantity = item.OrderQuantity,
                    PaymentAmount = item.PaymentAmount,
                    DeliveryAddress = item.DeliveryAddress,
                    ContactNum = item.ContactNum,
                    OrderAddedDate =item.OrderAddedDate,
                    OrderStatusId = item.OrderStatusId,
                    Active = item.Active,
                    Deleted = item.Deleted
                });
            }

            //OrderList = Result.ToList<OrderList_ViewModel>();

            return Json(OrderList, JsonRequestBehavior.AllowGet);
        }



    }
}