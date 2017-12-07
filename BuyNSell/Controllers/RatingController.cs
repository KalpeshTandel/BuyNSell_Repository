using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class RatingController : Controller
    {


        BuyNSell_DbEntities objDB = new BuyNSell_DbEntities();

        // GET: Rating
        public ActionResult Rating(int ProductId)
        {
            var SumRate = objDB.RatingMasters.Where(m => m.ProductId == ProductId).Sum(m => m.Rate);

            var CountRate = objDB.RatingMasters.Where(m => m.ProductId == ProductId).Count();

            int Rate = Convert.ToInt32(SumRate / CountRate);

            ViewBag.ProductId = ProductId;

            ViewBag.Rate = Rate;  
           

            return PartialView("_Rating",ProductId);
        }


        public ActionResult AddRate(int ProductId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);

            RatingMaster RatingMaster = objDB.RatingMasters.Where(m => m.ProductId == ProductId && m.UserId == UserId).FirstOrDefault();

            if(RatingMaster != null)
            {
                ViewBag.RatingLog = "You Are Already Rated";
            }
            else
            {
                ViewBag.RatingLog = "Please Rate !";
            }

            Session["RatingMaster"] = RatingMaster;

            ViewBag.RatingMaster = RatingMaster;

            return PartialView("_AddRate");

        }


        [HttpPost]
        public ActionResult AddRate_Click(int Rate)
        {
            RatingMaster RatingMaster = Session["RatingMaster"] as RatingMaster;

            return View();
        }


    }
}