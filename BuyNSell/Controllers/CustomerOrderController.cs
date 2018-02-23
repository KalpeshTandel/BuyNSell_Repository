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
        //static variables are used when only one copy of the variable is required.
        //A static variable shares the value of it among all instances of the class.
        #region ClassLevel Variabels Static        
        static int PageSize = 5;
        static int LastPageNumber;
        static int StartRecord;
        static int EndRecord;
        static int TotalNumberOfRecords;
        static int CurrentPageNumber;
        static string OrderBy;
        static string OrderByAscOrDesc;
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
                CurrentPageNumber = 1;
                PageSize = 5;
                StartRecord = 0;
                EndRecord = 4;
                OrderBy = "OrderAddedDate";
                OrderByAscOrDesc = "Desc";
                var Response = GetOrderList(StartRecord, EndRecord);
                return Json(Response, JsonRequestBehavior.AllowGet);
                //return Response;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult ddlPageSize_Change(int ddlPageSizeSelected)
        {
            PageSize = ddlPageSizeSelected;
            CurrentPageNumber = 1;
            StartRecord = (PageSize * CurrentPageNumber) - PageSize;
            EndRecord = (PageSize * CurrentPageNumber) - 1;
            var Response = GetOrderList(StartRecord, EndRecord);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult btnNextPageOrder_Click(int ddlPageSizeSelected)
        {
            PageSize = ddlPageSizeSelected;
            CurrentPageNumber = CurrentPageNumber + 1;
            StartRecord = (PageSize * CurrentPageNumber) - PageSize;
            EndRecord = (PageSize * CurrentPageNumber) - 1;
            var Response = GetOrderList(StartRecord, EndRecord);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult btnPreviousPageOrder_Click()
        {
            CurrentPageNumber = CurrentPageNumber - 1;
            StartRecord = (PageSize * CurrentPageNumber) - PageSize;
            EndRecord = (PageSize * CurrentPageNumber) - 1;
            var Response = GetOrderList(StartRecord, EndRecord);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ddlSortBy_Change(string ddlSortBySelected)
        {
            OrderBy = ddlSortBySelected;
            if (OrderBy.Contains("Desc"))
            {
                OrderByAscOrDesc = "Desc";
            }
            else
            {
                OrderByAscOrDesc = "Asc";
            }
            OrderBy = OrderBy.Replace("Desc", "");

            CurrentPageNumber = 1;
            StartRecord = (PageSize * CurrentPageNumber) - PageSize;
            EndRecord = (PageSize * CurrentPageNumber) - 1;

            var Response = GetOrderList(StartRecord, EndRecord);
            return Json(Response, JsonRequestBehavior.AllowGet);
        }



        public dynamic GetOrderList(int StartRecord, int EndRecord)
        {
            try
            {

                int UserId = Convert.ToInt16(Session["UserId"]);
                var OrderByProperty = typeof(OrderList_ViewModel).GetProperty(OrderBy);
                
                List<OrderList_ViewModel> FullOrderList = (from om in objDbEntities.OrderMasters
                                                          join pm in objDbEntities.ProductMasters on om.ProductId equals pm.ProductId
                                                          where pm.UserId == UserId
                                                          join picm in objDbEntities.PictureMasters on om.ProductId equals picm.ProductId
                                                          join um in objDbEntities.UserMasters on om.UserId equals um.UserId
                                                          join os in objDbEntities.OrderStatusMasters on om.OrderStatusId equals os.OrderStatusId
                                                          //where pm.UserId == UserId
                                                          group new { om, pm , picm ,um,os} by new { om.OrderId} into grp
                                                          select new OrderList_ViewModel
                                                          {
                                                           OrderId = grp.FirstOrDefault().om.OrderId,
                                                           ProductId = grp.FirstOrDefault().om.ProductId,
                                                           ProductName = grp.FirstOrDefault().pm.ProductName,
                                                           PictureId =grp.FirstOrDefault().picm.PictureId,
                                                           PictureContent = grp.FirstOrDefault().picm.PictureContent,
                                                           BuyerId = grp.FirstOrDefault().om.UserId,
                                                           BuyerName = grp.FirstOrDefault().um.FirstName + " " + grp.FirstOrDefault().um.LastName,
                                                           OrderQuantity = grp.FirstOrDefault().om.OrderQuantity,
                                                           PaymentAmount = grp.FirstOrDefault().om.PaymentAmount,
                                                           OrderAddedDate = grp.FirstOrDefault().om.OrderAddedDate,
                                                           OrderStatusId = grp.FirstOrDefault().om.OrderStatusId,
                                                           OrderStatusName = grp.FirstOrDefault().os.OrderStatusName,
                                                          }).OrderByDescending(x => x.OrderId).ToList();

                if(OrderByAscOrDesc == "Asc")
                {
                    FullOrderList = FullOrderList.OrderBy(x => OrderByProperty.GetValue(x, null)).ToList();
                }
                else
                {
                    FullOrderList = FullOrderList.OrderByDescending(x => OrderByProperty.GetValue(x, null)).ToList();
                }

                //int Start = (Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"]) ;
                //int End = (Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - 1;
                //Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

                TotalNumberOfRecords = FullOrderList.Count;
                LastPageNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(TotalNumberOfRecords) / Convert.ToDecimal(PageSize)));
            
                List<OrderList_ViewModel> SpecificOrderList = new List<OrderList_ViewModel>() ;

                for (int i = StartRecord; i <= EndRecord && i < TotalNumberOfRecords ; i++)
                {
                        SpecificOrderList.Add(FullOrderList[i]);
                }


                List<OrderStatusMaster> OrderStatusList = objDbEntities.OrderStatusMasters.Where(s => s.AccessFor == 2).ToList();

                foreach (var OrderItem in SpecificOrderList)
                {
                    OrderItem.PictureSrc = Convert.ToBase64String(OrderItem.PictureContent);


                    OrderItem.OrderStatusList = new List<OrderStatusMaster>();
                    //var OrderStageNumber = (from o in objDbEntities.OrderStatusMasters
                    //                        where o.OrderStatusId == OrderItem.OrderStatusId
                    //                        select o.StageNumber).SingleOrDefault();

                    var OrderStageNumber = objDbEntities.OrderStatusMasters.Where(s => s.OrderStatusId == OrderItem.OrderStatusId).Select(s => s.StageNumber).SingleOrDefault();

                    OrderStageNumber = OrderStageNumber + 1;

                    foreach (var OrderStatusItem in OrderStatusList)
                    {
                        if (OrderStageNumber == OrderStatusItem.StageNumber && OrderItem.OrderStatusId != 4 && OrderItem.OrderStatusId != 2)
                        {
                            OrderItem.OrderStatusList.Add(new OrderStatusMaster()
                            {
                                OrderStatusId = OrderStatusItem.OrderStatusId,
                                OrderStatusName = OrderStatusItem.OrderStatusName
                            });
                        }
                    }
                }

                var Response = new { OrderList = SpecificOrderList, TotalRecords = TotalNumberOfRecords, CurrentPageNumber = CurrentPageNumber,LastPageNumber = LastPageNumber };

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
                                                    where om.OrderId == OrderId
                                                    join pm in objDbEntities.ProductMasters on om.ProductId equals pm.ProductId
                                                    join picm in objDbEntities.PictureMasters on  om.ProductId equals picm.ProductId
                                                    join um in objDbEntities.UserMasters on om.UserId equals um.UserId
                                                    join os in objDbEntities.OrderStatusMasters on om.OrderStatusId equals os.OrderStatusId
                                                    //where om.OrderId == OrderId -- Write where condition on top increase performance
                                                    select new OrderList_ViewModel
                                                    {
                                                        OrderId = om.OrderId,
                                                        ProductId = om.ProductId,
                                                        ProductName = pm.ProductName,
                                                        PictureId = picm.PictureId,
                                                        PictureContent = picm.PictureContent,
                                                        Price = pm.Price,
                                                        BuyerId = om.UserId,
                                                        BuyerName = um.FirstName + " " + um.LastName,
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