using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class OrderController : Controller
    {
        BuyNSell_DbEntities objDB = new BuyNSell_DbEntities();

        // GET: Order
        public ActionResult AddOrder(int ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {

                    ProductMaster ProductMaster = objDB.ProductMasters.Where(p => p.ProductId == ProductId).FirstOrDefault();

                    Session["OrderProductId"] = ProductId;
                    Session["PaymentAmount"] = ProductMaster.Price;

                    List <SelectListItem> listOrderQuantity = new List<SelectListItem>();

                    for(int i = 1; i <= ProductMaster.Quantity; i++)
                    {
                        listOrderQuantity.Add(new SelectListItem { Text = i.ToString(), Value= i.ToString()});
                    }


                    Session["ProductInfo"] = ProductMaster;
                    Session["ddlOrderQuantity"] = listOrderQuantity;


                    return PartialView("_AddOrder");
                }
                else
                {
                    return RedirectToAction("LogInAgain", "Login");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult AddOrder(OrderMaster objOM)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    if(ModelState.IsValid)
                    {
                        objOM.OrderId = 0;
                        objOM.UserId = Convert.ToInt32(Session["UserId"]);
                        objOM.PaymentAmount = Convert.ToInt32(Session["PaymentAmount"]);
                        objOM.OrderStatusId = 1;
                        objOM.Active = true;
                        objOM.Deleted = false;
                        objOM.OrderAddedDate = DateTime.Now;

                        objDB.OrderMasters.Add(objOM);
                        objDB.SaveChanges();

                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("LogInAgain", "Login");
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public JsonResult OrderQuantity_Change(int OrderQuantity)
        {

            int OrderProductId = Convert.ToInt16(Session["OrderProductId"]);

            var Price = objDB.ProductMasters.Where(p => p.ProductId == OrderProductId).Select(p => p.Price).FirstOrDefault();

            int PaymentAmount = Convert.ToInt32(Price * OrderQuantity);

            Session["PaymentAmount"] = PaymentAmount;

            return Json(PaymentAmount, JsonRequestBehavior.AllowGet);

        }



    }
}