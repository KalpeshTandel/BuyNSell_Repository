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
                    Session["ProductId"] = ProductId;

                    ViewBag.ProductInfo = objDB.ProductMasters.Where(p => p.ProductId == ProductId).FirstOrDefault();

                    var Quantity = objDB.ProductMasters.Where(p => p.ProductId == ProductId).Select(p => p.Quantity).FirstOrDefault();

                    List<SelectListItem> listQuantity = new List<SelectListItem>();

                    for(int i = 1; i <= Quantity; i++)
                    {
                        listQuantity.Add(new SelectListItem { Text = i.ToString(), Value= i.ToString()});
                    }

                    ViewBag.ddlQuantity = listQuantity;


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


        public ActionResult OrderQuantity_Change(int OrderQuantity)
        {
            int ProductId = Convert.ToInt16(Session["ProductId"]);

            var Price = objDB.ProductMasters.Where(p => p.ProductId == ProductId).Select(p => p.Price).FirstOrDefault();

            int PaymentAmount = Convert.ToInt32(Price * OrderQuantity);


        }



    }
}