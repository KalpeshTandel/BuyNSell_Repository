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
                          join os in objDbEntities.OrderStatusMasters on om.OrderStatusId equals os.OrderStatusId
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
                              AvailableQuantity = pm.Quantity,
                              PaymentAmount = om.PaymentAmount,
                              DeliveryAddress = om.DeliveryAddress,
                              ContactNum = om.ContactNum,
                              OrderAddedDate = om.OrderAddedDate,
                              OrderStatusId = om.OrderStatusId,
                              OrderStatusName = os.OrderStatusName,
                              Active = om.Active,
                              Deleted = om.Deleted
                          }).ToList();

            var Result2 = objDbEntities.OrderStatusMasters.Where(s => s.AccessFor == 2).ToList();

           foreach(var item in Result)
            {
                OrderList.Add(new OrderList_ViewModel()
                {
                    OrderId = item.OrderId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    CustomerId = item.CustomerId,
                    CustomerName = item.CustomerFirstName + ' ' + item.CustomerLastName,
                    OrderQuantity = item.OrderQuantity,
                    AvailableQuantity = item.AvailableQuantity,
                    PaymentAmount = item.PaymentAmount,
                    DeliveryAddress = item.DeliveryAddress,
                    ContactNum = item.ContactNum,
                    OrderAddedDate = item.OrderAddedDate,
                    OrderStatusId = item.OrderStatusId,
                    OrderStatusName = item.OrderStatusName,
                    Active = item.Active,
                    Deleted = item.Deleted,
                    //OrderStatusList.OrderStatusName = item.OrderStatusName

                });
            }


            foreach (var item in OrderList)
            {
                item.OrderStatusList = new List<OrderStatusMaster>();
                //for (int i = 0; Result2.Count > 0; i++)
                //{
                foreach (var ResultItem in Result2)
                {
                    if (item.OrderStatusId < ResultItem.OrderStatusId)
                    {
                        item.OrderStatusList.Add(new OrderStatusMaster()
                        {
                            OrderStatusId = ResultItem.OrderStatusId,
                            OrderStatusName = ResultItem.OrderStatusName
                        });
                    }
                }
            }
                    //if(item.OrderStatusId < Result2[i].OrderStatusId)
                    //{
                    //    item.OrderStatusList.Add(new OrderStatusMaster()
                    //    {
                    //        OrderStatusId = Result2[i].OrderStatusId,
                    //        OrderStatusName = Result2[i].OrderStatusName
                    //    });
                    //}
                    //OrderStatusMaster obj1 = new OrderStatusMaster();
                    //obj1.OrderStatusId = Result2[i].OrderStatusId;
                    //obj1.OrderStatusName = Result2[i].OrderStatusName;

                    //item.OrderStatusList.Add(obj1);

                    //item.OrderStatusList.Add(new OrderStatusMaster()
                    //{
                    //    OrderStatusId = Result2[i].OrderStatusId,
                    //    OrderStatusName = Result2[i].OrderStatusName
                    //});
                    //OrderStatusList.Add(new OrderStatusMaster
                    //{
                    //    OrderStatusId = Result2[i].OrderStatusId,
                    //    OrderStatusName = Result2[i].OrderStatusName
                    //});

                    //OrderStatusList.Add(Result2[i].OrderStatusId.ToString());
                    //OrderStatusList[i].OrderStatusId = Result2[i].OrderStatusId;
                    //    //OrderStatusName = Result2[i].OrderStatusName
 
                    //item.OrderStatusList[i].OrderStatusName = Result2[i].OrderStatusName;
            //    }
            //}



            //for (int i = 0; i < OrderList.Count; i++)
            //{
            //    foreach (var a in OrderList)
            //    {
            //        a.
            //    }
            //    for (int k=0;OrderList[i].OrderStatusId < Result2[k].OrderStatusId; k++)
            //    {

            //    }

            //for (int j = 0; j < OrderList[i].OrderStatusList.Count; j++)
            //    {
            //        OrderList[i].OrderStatusList[j].OrderStatusId = Result2[i].OrderStatusId;
            //        OrderList[i].OrderStatusList[j].OrderStatusName = Result2[i].OrderStatusName;
            //    }
            //}


            //OrderList = Result.ToList<OrderList_ViewModel>();

            return Json(OrderList, JsonRequestBehavior.AllowGet);
        }



    }
}