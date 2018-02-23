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
            try
            {
                if (Session["UserId"] != null)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult GetMyOrderList()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    int UserId = Convert.ToInt16(Session["UserId"]);

                    //List<OrderList_ViewModel> FullOrderList = objDataBase.OrderMasters.Where(om => om.UserId == UserId)
                    //                              .Join(objDataBase.ProductMasters, om => om.ProductId, pm => pm.ProductId, (om, pm) => new { OrderMaster = om, ProductMaster = pm })
                    //                              .Join(objDataBase.UserMasters, ompm => ompm.ProductMaster.UserId, um => um.UserId, (ompm, um) => new { OrderMaster = ompm.OrderMaster, ProductMaster = ompm.ProductMaster, UserMaster = um })
                    //                              .Join(objDataBase.OrderStatusMasters, ompmum => ompmum.OrderMaster.OrderStatusId, os => os.OrderStatusId, (ompmum, os) => new { OrderMaster = ompmum.OrderMaster, ProductMaster = ompmum.ProductMaster, UserMaster = ompmum.UserMaster, OrderStatusMasters = os })
                    //                              .Select(m => new OrderList_ViewModel
                    //                              {
                    //                                  OrderId = m.OrderMaster.OrderId,
                    //                                  ProductId = m.OrderMaster.ProductId,
                    //                                  ProductName = m.ProductMaster.ProductName,
                    //                                  CustomerId = m.ProductMaster.UserId,
                    //                                  CustomerName = m.UserMaster.FirstName + " " + m.UserMaster.LastName,
                    //                                  OrderQuantity = m.OrderMaster.OrderQuantity,
                    //                                  PaymentAmount = m.OrderMaster.PaymentAmount,
                    //                                  OrderAddedDate = m.OrderMaster.OrderAddedDate,
                    //                                  OrderStatusId = m.OrderMaster.OrderStatusId,
                    //                                  OrderStatusName = m.OrderStatusMasters.OrderStatusName
                    //                              }).ToList();

                    List<OrderList_ViewModel> FullOrderList = objDataBase.OrderMasters.Where(om => om.UserId == UserId)
                                                              .Join(objDataBase.ProductMasters, om => om.ProductId, pm => pm.ProductId, (om, pm) => new { OrderMaster = om, ProductMaster = pm })
                                                              .Join(objDataBase.PictureMasters, ompm => ompm.ProductMaster.ProductId, picm => picm.ProductId, (ompm, picm) => new { OrderMaster = ompm.OrderMaster, ProductMaster = ompm.ProductMaster, PictureMaster = picm })
                                                              .Join(objDataBase.UserMasters, ompmpicm => ompmpicm.ProductMaster.UserId, um => um.UserId, (ompmpicm, um) => new { OrderMaster = ompmpicm.OrderMaster, ProductMaster = ompmpicm.ProductMaster, PictureMaster = ompmpicm.PictureMaster, UserMaster = um })
                                                              .Join(objDataBase.OrderStatusMasters, ompmpicmum => ompmpicmum.OrderMaster.OrderStatusId, os => os.OrderStatusId, (ompmpicmum, os) => new { OrderMaster = ompmpicmum.OrderMaster, ProductMaster = ompmpicmum.ProductMaster, PictureMaster = ompmpicmum.PictureMaster, UserMaster = ompmpicmum.UserMaster, OrderStatusMasters = os })
                                                              //.OrderBy(x => x.OrderMaster.OrderId)
                                                              .GroupBy(x => x.OrderMaster.OrderId)
                                                              .Select(m => new OrderList_ViewModel
                                                              {
                                                                  OrderId = m.FirstOrDefault().OrderMaster.OrderId,
                                                                  ProductId = m.FirstOrDefault().OrderMaster.ProductId,
                                                                  ProductName = m.FirstOrDefault().ProductMaster.ProductName,
                                                                  PictureId = m.FirstOrDefault().PictureMaster.PictureId,
                                                                  PictureContent = m.FirstOrDefault().PictureMaster.PictureContent,
                                                                  //PictureSrc = Convert.ToBase64String(PictureContent),
                                                                  SellerId = m.FirstOrDefault().ProductMaster.UserId,
                                                                  SellerName = m.FirstOrDefault().UserMaster.FirstName + " " + m.FirstOrDefault().UserMaster.LastName,
                                                                  OrderQuantity = m.FirstOrDefault().OrderMaster.OrderQuantity,
                                                                  PaymentAmount = m.FirstOrDefault().OrderMaster.PaymentAmount,
                                                                  OrderAddedDate = m.FirstOrDefault().OrderMaster.OrderAddedDate,
                                                                  OrderStatusId = m.FirstOrDefault().OrderMaster.OrderStatusId,
                                                                  OrderStatusName = m.FirstOrDefault().OrderStatusMasters.OrderStatusName
                                                              }).OrderByDescending(x => x.OrderId).ToList();

                    List<OrderStatusMaster> OrderStatusList = objDataBase.OrderStatusMasters.Where(s => s.AccessFor == 1).ToList();

                    foreach (var OrderItem in FullOrderList)
                    {
                        //var Picture = objDataBase.PictureMasters.Where(p => p.ProductId == OrderItem.ProductId).OrderBy(p => p.PictureId).Select(p => new { PictureId = p.PictureId, PictureContent = p.PictureContent }).FirstOrDefault();
                        //OrderItem.PictureId = Picture.PictureId;
                        //var PictureData = Convert.ToBase64String(Picture.PictureContent);
                        //OrderItem.PictureSrc = "data:image;base64,"+PictureData;
                        //OrderItem.PictureContent = Picture.PictureContent;

                        OrderItem.PictureSrc = Convert.ToBase64String(OrderItem.PictureContent);

                        OrderItem.OrderStatusList = new List<OrderStatusMaster>();

                        var OrderStageNumber = objDataBase.OrderStatusMasters.Where(s => s.OrderStatusId == OrderItem.OrderStatusId).Select(s => s.StageNumber).SingleOrDefault();

                        OrderStageNumber = OrderStageNumber + 1;

                        foreach (var OrderStatusItem in OrderStatusList)
                        {
                            if (OrderStatusItem.StageNumber == OrderStageNumber)
                            {
                                OrderItem.OrderStatusList.Add(new OrderStatusMaster
                                {
                                    OrderStatusId = OrderStatusItem.OrderStatusId,
                                    OrderStatusName = OrderStatusItem.OrderStatusName
                                });
                            }
                        }
                    }

                    return Json(FullOrderList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

    
        }


        //[HttpPost]
        public void ChangeOrderStatus(OrderMaster OrderInfo)
        {
            try
            {
                if (OrderInfo != null)
                {
                    OrderMaster Order = objDataBase.OrderMasters.Where(o => o.OrderId == OrderInfo.OrderId).FirstOrDefault();
                    Order.OrderStatusId = OrderInfo.OrderStatusId;
                    objDataBase.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewMyOrderDetails(int OrderId)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    OrderList_ViewModel OrderDetails = objDataBase.OrderMasters.Where(om => om.OrderId == OrderId)
                                                       .Join(objDataBase.ProductMasters, om => om.ProductId, pm => pm.ProductId, (om, pm) => new { OrderMaster = om, ProductMaster = pm })
                                                       .Join(objDataBase.PictureMasters, ompm => ompm.OrderMaster.ProductId, picm => picm.ProductId, (ompm, picm) => new { OrderMaster = ompm.OrderMaster, ProductMaster = ompm.ProductMaster, PictureMaster = picm })
                                                       .Join(objDataBase.UserMasters, ompmpicm => ompmpicm.ProductMaster.UserId, um => um.UserId, (ompmpicm, um) => new { OrderMaster = ompmpicm.OrderMaster, ProductMaster = ompmpicm.ProductMaster, PictureMaster = ompmpicm.PictureMaster, UserMaster = um })
                                                       .Join(objDataBase.OrderStatusMasters, ompmpicmum => ompmpicmum.OrderMaster.OrderStatusId, os => os.OrderStatusId, (ompmpicmum, os) => new { OrderMaster = ompmpicmum.OrderMaster, ProductMaster = ompmpicmum.ProductMaster, PictureMaster = ompmpicmum.PictureMaster, UserMaster = ompmpicmum.UserMaster, OrderStatusMasters = os })
                                                       .Select(m => new OrderList_ViewModel
                                                       {
                                                           OrderId = m.OrderMaster.OrderId,
                                                           ProductId = m.OrderMaster.ProductId,
                                                           ProductName = m.ProductMaster.ProductName,
                                                           Price = m.ProductMaster.Price,
                                                           PictureId = m.PictureMaster.PictureId,
                                                           PictureContent = m.PictureMaster.PictureContent,
                                                           SellerId = m.ProductMaster.UserId,
                                                           SellerName = m.UserMaster.FirstName + " " + m.UserMaster.LastName,
                                                           OrderQuantity = m.OrderMaster.OrderQuantity,
                                                           AvailableQuantity = m.ProductMaster.Quantity,
                                                           PaymentAmount = m.OrderMaster.PaymentAmount,
                                                           DeliveryAddress = m.OrderMaster.DeliveryAddress,
                                                           ContactNum = m.OrderMaster.ContactNum,
                                                           OrderAddedDate = m.OrderMaster.OrderAddedDate,
                                                           OrderStatusId = m.OrderMaster.OrderStatusId,
                                                           OrderStatusName = m.OrderStatusMasters.OrderStatusName
                                                       }).FirstOrDefault();

                    //return PartialView("_ViewMyOrderDetails", OrderDetails);

                    return PartialView("../CustomerOrder/_ViewCustomerOrderDetails", OrderDetails);

                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}