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
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: Order


        //[ChildActionOnly]
        //[NonAction]
        public ActionResult AddOrder(int ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    ProductMaster ProductMaster = objDbEntities.ProductMasters.Where(p => p.ProductId == ProductId).FirstOrDefault();

                    Session["OrderProductId"] = ProductId;
                    Session["PaymentAmount"] = ProductMaster.Price;

                    List<SelectListItem> listOrderQuantity = new List<SelectListItem>();

                    for (int i = 1; i <= ProductMaster.Quantity; i++)
                    {
                        listOrderQuantity.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
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
                    if (ModelState.IsValid)
                    {
                        objOM.OrderId = 0;
                        objOM.UserId = Convert.ToInt32(Session["UserId"]);
                        objOM.PaymentAmount = Convert.ToInt32(Session["PaymentAmount"]);
                        objOM.OrderStatusId = 1;
                        objOM.Active = true;
                        objOM.Deleted = false;
                        objOM.OrderAddedDate = DateTime.Now;

                        objDbEntities.OrderMasters.Add(objOM);
                        objDbEntities.SaveChanges();

                        return RedirectToAction("Home", "Home");

                        //return JavaScript("alert('hello')");
                        //return Content("<script language='javascript' type='text/javascript'>alert('hello');</script>");
                        //return Content("<script language='javascript' type='text/javascript'>$('#divAddOrder').empty();$('#divPopupBackground').empty();$('#divAddOrder').hide();$('#divPopupBackground').hide();</script>");


                        //return JavaScript("alert('Order Placed Succesfully');");
                        //return Json(" Order Placed Succesfully ");
                        //return JavaScript("</script>Callback()</script>");
                        // return RedirectToAction("Home", "Home");
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


        public ActionResult OrderQuantity_Change(int OrderQuantity)
        {
            try
            {
                if (Session["UserId"] != null)
                {

                    int OrderProductId = Convert.ToInt16(Session["OrderProductId"]);

                    var Price = objDbEntities.ProductMasters.Where(p => p.ProductId == OrderProductId).Select(p => p.Price).FirstOrDefault();

                    int PaymentAmount = Convert.ToInt32(Price * OrderQuantity);

                    Session["PaymentAmount"] = PaymentAmount;

                    return Json(PaymentAmount, JsonRequestBehavior.AllowGet);
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



    }
}