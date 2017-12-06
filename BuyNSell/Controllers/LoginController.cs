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
        BuyNSell_DbEntities objDb = new BuyNSell_DbEntities();

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
                    UserMaster UserInfo = objDb.UserMasters.Where(a => a.EmailId.Equals(objUM.EmailId) && a.Password.Equals(objUM.Password)).FirstOrDefault();

                    if (UserInfo != null)
                    {
                        StoreUserInfoInSession(UserInfo);
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

                    objDb.UserMasters.Add(objUM);
                    objDb.SaveChanges();

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

    }
}