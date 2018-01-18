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
        #region ClassLevel Variabels
        int CurrentPageNumber;
        int PageSize;
        int NumberOfPages;
        int StartRecord;
        int EndRecord;
        int TotalNumberOfRecords;
        #endregion



        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: CustomerOrder
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

        public JsonResult CustomerOrder_PageLoad()
        {
            try
            {
                StartRecord = 0;
                EndRecord = 4;
                var Response = GetOrderList(StartRecord, EndRecord);
                return Json(Response, JsonRequestBehavior.AllowGet);
                //return Response;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public dynamic GetOrderList(int StartRecord, int EndRecord)
        {
            try
            {

                int UserId = Convert.ToInt16(Session["UserId"]);

                List<OrderList_ViewModel> FullOrderList = (from om in objDbEntities.OrderMasters
                                                       join pm in objDbEntities.ProductMasters on om.ProductId equals pm.ProductId
                                                       join um in objDbEntities.UserMasters on om.UserId equals um.UserId
                                                       join os in objDbEntities.OrderStatusMasters on om.OrderStatusId equals os.OrderStatusId
                                                       where pm.UserId == UserId
                                                       orderby om.OrderAddedDate descending
                                                       select new OrderList_ViewModel
                                                       {
                                                           OrderId = om.OrderId,
                                                           ProductId = om.ProductId,
                                                           ProductName = pm.ProductName,
                                                           CustomerId = om.UserId,
                                                           CustomerName = um.FirstName + " " + um.LastName,
                                                           OrderQuantity = om.OrderQuantity,
                                                           PaymentAmount = om.PaymentAmount,
                                                           OrderAddedDate = om.OrderAddedDate,
                                                           OrderStatusId = om.OrderStatusId,
                                                           OrderStatusName = os.OrderStatusName,
                                                       }).ToList();
                TotalNumberOfRecords = FullOrderList.Count;

                //int Start = (Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"]) ;
                //int End = (Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - 1;

                //Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

                List<OrderList_ViewModel> SpecificOrderList = new List<OrderList_ViewModel>() ;

                for (int i = StartRecord; i <= EndRecord && i < TotalNumberOfRecords ; i++)
                {
                        SpecificOrderList.Add(FullOrderList[i]);
                }


                List<OrderStatusMaster> OrderStatusList = objDbEntities.OrderStatusMasters.Where(s => s.AccessFor == 2).ToList();

                foreach (var OrderItem in SpecificOrderList)
                {
                    OrderItem.OrderStatusList = new List<OrderStatusMaster>();
                    //var OrderStageNumber = (from o in objDbEntities.OrderStatusMasters
                    //                        where o.OrderStatusId == OrderItem.OrderStatusId
                    //                        select o.StageNumber).SingleOrDefault();

                    var OrderStageNumber = objDbEntities.OrderStatusMasters.Where(s => s.OrderStatusId == OrderItem.OrderStatusId).Select(s => s.StageNumber).SingleOrDefault();

                    OrderStageNumber = OrderStageNumber + 1;

                    foreach (var OrderStatusItem in OrderStatusList)
                    {
                        if (OrderStageNumber == OrderStatusItem.StageNumber && OrderItem.OrderStatusId != 4)
                        {
                            OrderItem.OrderStatusList.Add(new OrderStatusMaster()
                            {
                                OrderStatusId = OrderStatusItem.OrderStatusId,
                                OrderStatusName = OrderStatusItem.OrderStatusName
                            });
                        }
                    }
                }

                var Response = new { OrderList = SpecificOrderList, Moredata = "Hii Kalpesh" };

                return Response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ChangeOrderStatus(OrderMaster OrderInfo)
        {
            try
            {
                if (OrderInfo != null)
                {
                    OrderMaster Order = objDbEntities.OrderMasters.Where(o => o.OrderId == OrderInfo.OrderId).FirstOrDefault();
                    Order.OrderStatusId = OrderInfo.OrderStatusId;
                    objDbEntities.SaveChanges();

                    if(OrderInfo.OrderStatusId == 3) //Subtract Product Quantity by 1 When Seller Change Status OrderAccepted
                    {
                        ProductMaster Product = objDbEntities.ProductMasters.Where(p => p.ProductId == OrderInfo.ProductId).FirstOrDefault();
                        Product.Quantity = Product.Quantity - 1;
                        //ModelState.Remove("Quantity");
                        objDbEntities.Configuration.ValidateOnSaveEnabled = false;
                        objDbEntities.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult ViewCustomerOrderDetails(int OrderId)
        {
            try
            {
                // OrderList_ViewModel OrderDetails = new OrderList_ViewModel();

                OrderList_ViewModel OrderDetails = (from om in objDbEntities.OrderMasters
                                                    join pm in objDbEntities.ProductMasters on om.ProductId equals pm.ProductId
                                                    join picm in objDbEntities.PictureMasters on  om.ProductId equals picm.ProductId
                                                    join um in objDbEntities.UserMasters on om.UserId equals um.UserId
                                                    join os in objDbEntities.OrderStatusMasters on om.OrderStatusId equals os.OrderStatusId
                                                    where om.OrderId == OrderId
                                                    select new OrderList_ViewModel
                                                    {
                                                        OrderId = om.OrderId,
                                                        ProductId = om.ProductId,
                                                        ProductName = pm.ProductName,
                                                        PictureId = picm.PictureId,
                                                        PictureContent = picm.PictureContent,
                                                        Price = pm.Price,
                                                        CustomerId = om.UserId,
                                                        CustomerName = um.FirstName + " " + um.LastName,
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
                                                    }).FirstOrDefault();

                return PartialView("_ViewCustomerOrderDetails", OrderDetails);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}