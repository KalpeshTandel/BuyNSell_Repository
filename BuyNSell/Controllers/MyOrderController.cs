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

            List<OrderList_ViewModel> FullOrderList = objDataBase.OrderMasters.Where(om => om.UserId == UserId)
                                                      .Join(objDataBase.ProductMasters, om => om.ProductId, pm => pm.ProductId, (om, pm) => new { OrderMaster = om, ProductMaster = pm })
                                                      .Join(objDataBase.UserMasters, ompm => ompm.ProductMaster.UserId, um => um.UserId, (ompm, um) => new { OrderMaster = ompm.OrderMaster, ProductMaster = ompm.ProductMaster, UserMaster = um })
                                                      .Join(objDataBase.OrderStatusMasters, ompmum => ompmum.OrderMaster.OrderStatusId, os => os.OrderStatusId, (ompmum, os) => new { OrderMaster = ompmum.OrderMaster, ProductMaster = ompmum.ProductMaster, UserMaster = ompmum.UserMaster, OrderStatusMasters = os })
                                                      .Select(m => new OrderList_ViewModel
                                                      {
                                                          OrderId = m.OrderMaster.OrderId,
                                                          ProductId = m.OrderMaster.ProductId,
                                                          ProductName = m.ProductMaster.ProductName,
                                                          CustomerId = m.ProductMaster.UserId,
                                                          CustomerName = m.UserMaster.FirstName+" "+m.UserMaster.LastName,
                                                          OrderQuantity = m.OrderMaster.OrderQuantity,
                                                          PaymentAmount = m.OrderMaster.PaymentAmount,
                                                          OrderAddedDate = m.OrderMaster.OrderAddedDate,
                                                          OrderStatusId = m.OrderMaster.OrderStatusId,
                                                          OrderStatusName = m.OrderStatusMasters.OrderStatusName
                                                      }).ToList();

            return Json(FullOrderList,JsonRequestBehavior.AllowGet);
        }

    }
}