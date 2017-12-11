using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;
using BuyNSell.Controllers.DataAccess;


namespace BuyNSell.Controllers
{
    public class HomeController : Controller
    {
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: Home



        //[ChildActionOnly]
        //[NonAction]          
        public ActionResult Home()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Session["PageNumber"] = 1;
                    Session["PageSize"] = 5;

                    Session["SearchText"] = "";

                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList("", 1, 5, "ProductName");

                    Session["LastPageNumber"] = Math.Ceiling( Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

                    ViewBag.ProductCategory = new SelectList(objDbEntities.ProductCategoryMasters.ToList(), "ProductCategoryId", "ProductCategoryName");

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



        public ActionResult SearchProduct(string SearchText, string OrderBy, int PageSize)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Session["PageNumber"] = 1;
                    Session["PageSize"] = PageSize;

                    Session["SearchText"] = SearchText;

                    int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;

                    int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);


                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);

                    Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

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


        public ActionResult NextPage_Click(string SearchText,string OrderBy,int PageSize)/*string SearchText, int Start, int End, string OrderBy,int PageSize)*/
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Session["PageSize"] = PageSize;
                    Session["PageNumber"] = Convert.ToInt32(Session["PageNumber"]) + 1;

                    SearchText = Session["SearchText"].ToString();

                    int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;

                    int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);

                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);

                    Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

                    return PartialView("_ProductList", ProductList);

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



        public ActionResult PreviousPage_Click(string SearchText,string OrderBy, int PageSize)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Session["PageSize"] = PageSize;
                    Session["PageNumber"] = Convert.ToInt32(Session["PageNumber"]) - 1;

                    SearchText = Session["SearchText"].ToString();

                    int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;

                    int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);

                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);

                    Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

                    return PartialView("_ProductList", ProductList);

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



        public ActionResult ddlPageSize_Change(string SearchText,string OrderBy, int PageSize)
        {
            try
            {

                if (Session["UserId"] != null)
                {
                    Session["PageNumber"] = 1;
                    Session["PageSize"] = PageSize;

                    SearchText = Session["SearchText"].ToString();

                    int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;
                    int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);

                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);

                    Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

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


        public ActionResult ddlSortBy_Change(string SearchText, string OrderBy, int PageSize)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    Session["PageNumber"] = 1;
                    Session["PageSize"] = PageSize;

                    SearchText = Session["SearchText"].ToString();

                    int Start = ((Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"])) - Convert.ToInt32(Session["PageSize"])) + 1;
                    int End = Convert.ToInt32(Session["PageSize"]) * Convert.ToInt32(Session["PageNumber"]);
                    List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
                    ProductList = GetProductList(SearchText, Start, End, OrderBy);

                    Session["LastPageNumber"] = Math.Ceiling(Convert.ToDecimal(Session["TotalRecords"]) / Convert.ToDecimal(Session["PageSize"]));

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



    }
}