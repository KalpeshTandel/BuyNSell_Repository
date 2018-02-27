using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class LoginController : Controller
    {
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: Login
        public ActionResult Login()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }


      [HttpPost]
      public ActionResult Login(UserMaster_Login_ViewModel objUM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserMaster UserInfo = objDbEntities.UserMasters.Where(a => a.EmailId.Equals(objUM.EmailId) && a.Password.Equals(objUM.Password)).FirstOrDefault();

                    if (UserInfo != null)
                    {
                        StoreUserInfoInSession(UserInfo);
                        StoreNotificationInfoInSession();
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The EmailId Or Password Is Incorrect");
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Signup()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }     
        }


        [HttpPost]
        public ActionResult Signup(UserMaster objUM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    objUM.UserId = 0;
                    objUM.Active = true;
                    objUM.Deleted = false;
                    objUM.UserTypeId = 2;

                    objDbEntities.UserMasters.Add(objUM);
                    objDbEntities.SaveChanges();

                    StoreUserInfoInSession(objUM);

                    return RedirectToAction("Home", "Home");
                }
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
        public void StoreUserInfoInSession(UserMaster objUM)
        {
            try
            {
                Session["UserId"] = objUM.UserId;
                Session["UserTypeId"] = objUM.UserTypeId;
                Session["FirstName"] = objUM.FirstName;
                 
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void StoreNotificationInfoInSession()
        {
            //IsNew Flag in OrderMaster Meaning --> 1 -New for Buyer, 2 -New for Seller, 0 -Normal
            try
            {
                if (Session["UserId"] != null)
                {
                    int UserId = Convert.ToInt16(Session["UserId"]);
                    Session["NewMyOrder"] = objDbEntities.OrderMasters.Where(o => o.UserId == UserId && o.IsNew == 1).Count();
                    Session["NewCustomerOrder"] = objDbEntities.OrderMasters.Join(objDbEntities.ProductMasters, o => o.ProductId, p => p.ProductId, (o, p) => new { o, p }).Where(op => op.p.UserId == UserId && op.o.IsNew == 2).Count();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult LogInAgain()
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }         
        }


        public ActionResult Logout()
        {
            try
            {              
                Session.Clear();
                Session.Abandon();
          
                return RedirectToAction("Login", "Login");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}