using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;
using BuyNSell.Models.ViewModels;
using BuyNSell.Controllers.DataAccess;

//Hiii Shruti 10.14AM

namespace BuyNSell.Controllers
{
    public class HomeController : Controller
    {
        BuyNSell_DbEntities objDB = new BuyNSell_DbEntities();

        // GET: Home
        public ActionResult Home()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList("", 1, 5, "ProductName");

                    Int32 TotalRecords = Convert.ToInt32(Session["TotalRecords"]);

                    Session["PageNumber"] = 1;
                    Session["PageSize"] = 5;

                    return View(ProductList);
                }
                else
                {
                    return RedirectToAction("LogInAgain", "Login");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
              
        }


        //public void SetProductSession()
        //{

        //}
        
        
        public List<ProductList_ViewModel> GetProductList(string SearchText, int Start, int End, string OrderBy)
        {
            try
            {
                List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                DataAccess.DataAccess dataaccess = new DataAccess.DataAccess();
                ProductList = dataaccess.GetProductList(SearchText, Start, End, OrderBy);
                
                return ProductList; 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }




        public ActionResult SearchProduct(string SearchText, int Start, int End, string OrderBy)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);
                    return PartialView("_ProductList", ProductList);
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
        public ActionResult Paging(string SearchText, int Start, int End, string OrderBy,int PageSize)/*string SearchText, int Start, int End, string OrderBy,int PageSize)*/
            {
            try
            {
                Session["PageSize"] = PageSize;
                Session["PageNumber"] = Convert.ToInt32(Session["PageNumber"]) + 1;

                Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1 ;

                End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);

                List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                ProductList = GetProductList("", Start, End, "ProductName");
                return PartialView("_ProductList", ProductList);
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult ddlPageSize_Change(string SearchText,string OrderBy, int PageSize)
        {
            try
            {
                Session["PageNumber"] = 1; 
                Session["PageSize"] = PageSize;
                int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;
                int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);

                List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                ProductList = GetProductList(SearchText, Start, End, OrderBy);
                return PartialView("_ProductList", ProductList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}