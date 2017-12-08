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
        public ActionResult AverageRating(int ProductId)
        {
            var SumRate = objDB.RatingMasters.Where(m => m.ProductId == ProductId).Sum(m => m.Rate);

            var CountRate = objDB.RatingMasters.Where(m => m.ProductId == ProductId).Count();

            int Rate = Convert.ToInt32(SumRate / CountRate);

            ViewBag.ProductId = ProductId;

            ViewBag.Rate = Rate;  
           

            return PartialView("_AverageRating", ProductId);
        }


        public ActionResult AddOwnRate(int ProductId)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            Session["RatingProductId"] = ProductId;

            RatingMaster RatingMaster = objDB.RatingMasters.Where(m => m.ProductId == ProductId && m.UserId == UserId).FirstOrDefault();

            if(RatingMaster != null)
            {
                ViewBag.RatingLog = "You aleady rated this Product";
                Session["RatingMaster"] = RatingMaster;
                ViewBag.RateLog = RatingMaster.Rate;
            }
            else
            {
                ViewBag.RatingLog = "  Please Rate this Product !";
                Session["RatingMaster"] = null;
                ViewBag.RateLog = 0;
            }

            return PartialView("_AddOwnRate");

        }


        [HttpPost]
        public ActionResult AddOwnRate_Click(int Rate)
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            int RatingProductId = Convert.ToInt32(Session["RatingProductId"]);

            if(Session["RatingMaster"] != null)
            {
                RatingMaster RatingMaster = Session["RatingMaster"] as RatingMaster;
                RatingMaster objRM = objDB.RatingMasters.Single(m => m.ProductId == RatingProductId && m.UserId == UserId);

                objRM.Rate = Rate;
                objRM.RatingModifiedDate = DateTime.Now;
                objDB.SaveChanges();
            }
            else
            {
                RatingMaster objRM = new RatingMaster();
                objRM.ProductId = RatingProductId;
                objRM.UserId = UserId;
                objRM.Rate = Rate;
                objRM.RatingAddedDate = DateTime.Now;
                objRM.Active = true;

                objDB.RatingMasters.Add(objRM);
                objDB.SaveChanges();

            }

            return JavaScript("alert('hello')");
        }


    }
}