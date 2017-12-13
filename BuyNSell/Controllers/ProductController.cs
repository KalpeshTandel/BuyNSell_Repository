using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BuyNSell.Models;

namespace BuyNSell.Controllers
{
    public class ProductController : Controller
    {
        BuyNSell_DbEntities objDbEntities = new BuyNSell_DbEntities();

        // GET: Product
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
                    return RedirectToAction("LogInAgain", "Login");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult AddProduct()
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    //ShowPhoto(1);
                    ViewBag.CategoryList = new SelectList(objDbEntities.ProductCategoryMasters.ToList(), "ProductCategoryId", "ProductCategoryName");

                    return View();
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
        public ActionResult AddProduct(ProductMaster objPM)
        {
            try
            {
                if (Session["UserId"] != null)
                {

                    ViewBag.CategoryList = new SelectList(objDbEntities.ProductCategoryMasters.ToList(), "ProductCategoryId", "ProductCategoryName");
                    if (ModelState.IsValid)
                    {
                        objPM.ProductId = 0;
                        objPM.UserId = Convert.ToInt32(Session["UserId"]);
                        objPM.Active = true;
                        objPM.Deleted = false;
                        objPM.AddedDate = DateTime.Now;

                        ProductPicture(objPM);

                        objDbEntities.ProductMasters.Add(objPM);
                        objDbEntities.SaveChanges();
                    }

                    return View();
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


        public ActionResult ViewProductDetails(int ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    ProductList_ViewModel ProductDetails = new ProductList_ViewModel();

                    var Result = objDbEntities.ProductMasters
                               .Join(objDbEntities.UserMasters, p => p.UserId, u => u.UserId, (p, u) => new { p, u })
                               .Join(objDbEntities.ProductCategoryMasters, pc => pc.p.ProductCategoryId, p => p.ProductCategoryId, (p, pc) =>
                               new
                               {
                                   ProductId = p.p.ProductId,
                                   ProductName = p.p.ProductName,
                                   ProductDescription = p.p.ProductDescription,
                                   UserId = p.u.UserId,
                                  // UserName = p.u.FirstName + ' ' + p.u.LastName,
                                   ProductCategoryId = pc.ProductCategoryId,
                                   ProductCategoryName = pc.ProductCategoryName,
                                   Quantity = p.p.Quantity,
                                   Active = p.p.Active,
                                   Deleted = p.p.Deleted,
                                   AddedDate = p.p.AddedDate,
                                   PictureContent1 = p.p.PictureContent1,
                                   ContentType1 = p.p.ContentType1,
                                   Price = p.p.Price
                               })
                               .Where(c => c.ProductId == ProductId)
                               .Select(a => new
                               {
                                   ProductId = a.ProductId,
                                   ProductCategoryName = a.ProductCategoryName

                               }).FirstOrDefault();



                    ProductDetails.ProductId = Result.ProductId;
                    //ProductDetails.ProductName = Result.ProductName;
                    //ProductDetails.ProductDescription = Result.ProductDescription;
                    //ProductDetails.UserId = Result.UserId;
                    //ProductDetails.UserName = Result.UserName;
                    //ProductDetails.ProductCategoryId = Result.ProductCategoryId;
                    ProductDetails.ProductCategoryName = Result.ProductCategoryName;
                    //ProductDetails.Quantity = Result.Quantity;
                    //ProductDetails.Active = Result.Active;
                    //ProductDetails.Deleted = Result.Deleted;
                    //ProductDetails.AddedDate = Result.AddedDate;
                    //ProductDetails.PictureContent1 = Result.PictureContent1;
                    //ProductDetails.ContentType1 = Result.ContentType1;
                    //ProductDetails.Price = Result.Price;

                    return View();

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




        public ProductMaster ProductPicture(ProductMaster objPM)
        {
            try
            {

                HttpPostedFileBase file1 = Request.Files["SelectPicture1"];
                HttpPostedFileBase file2 = Request.Files["SelectPicture2"];
                HttpPostedFileBase file3 = Request.Files["SelectPicture3"];

                if (file1 != null)
                {

                    objPM.ContentType1 = file1.ContentType;
                  
                    Int32 Length = file1.ContentLength;
                    byte[] tempImage = new byte[Length];
                    file1.InputStream.Read(tempImage, 0, Length);
                    objPM.PictureContent1 = tempImage;

                  }

                if (file2 != null)
                {

                    objPM.ContentType2 = file2.ContentType;
                   
                    Int32 Length = file2.ContentLength;
                    byte[] tempImage = new byte[Length];
                    file2.InputStream.Read(tempImage, 0, Length);
                    objPM.PictureContent2 = tempImage;

                }

                if (file3 != null)
                {

                    objPM.ContentType3 = file3.ContentType;
                   
                    Int32 Length = file3.ContentLength;
                    byte[] tempImage = new byte[Length];
                    file3.InputStream.Read(tempImage, 0, Length);
                    objPM.PictureContent3 = tempImage;
                  
                }

                return objPM;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //public void ProductPicture(int ProductId)
        //{
        //    try
        //    {

        //        PictureMaster objPic = new PictureMaster();
        //        HttpPostedFileBase file1 = Request.Files["SelectPicture1"];
        //        HttpPostedFileBase file2 = Request.Files["SelectPicture2"];
        //        HttpPostedFileBase file3 = Request.Files["SelectPicture3"];

        //        if (file1 != null)
        //        {
        //            objPic.PictureName = file1.FileName;
        //            objPic.ProductId = ProductId;
        //            objPic.ContentType = file1.ContentType;
        //            objPic.Deleted = false;

        //            Int32 Length = file1.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file1.InputStream.Read(tempImage, 0, Length);
        //            objPic.PictureContent = tempImage;

        //            objDb.PictureMasters.Add(objPic);
        //            objDb.SaveChanges();
        //        }

        //        if(file2 != null)
        //        {
        //            objPic.PictureName = file2.FileName;
        //            objPic.ProductId = ProductId;
        //            objPic.ContentType = file2.ContentType;
        //            objPic.Deleted = false;

        //            Int32 Length = file2.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file2.InputStream.Read(tempImage, 0, Length);
        //            objPic.PictureContent = tempImage;

        //            objDb.PictureMasters.Add(objPic);
        //            objDb.SaveChanges();
        //        }

        //        if (file3 != null)
        //        {
        //            objPic.PictureName = file3.FileName;
        //            objPic.ProductId = ProductId;
        //            objPic.ContentType = file3.ContentType;
        //            objPic.Deleted = false;

        //            Int32 Length = file3.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file3.InputStream.Read(tempImage, 0, Length);
        //            objPic.PictureContent = tempImage;

        //            objDb.PictureMasters.Add(objPic);
        //            objDb.SaveChanges();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }

        //}



        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowPhoto(Int32 ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {

                    int value = ProductId;
                    //This is my method for getting the image information
                    // including the image byte array from the image column in
                    // a database.
                    //PictureMaster image = objDb.PictureMasters.Where(s => s.PictureId == 1).FirstOrDefault();
                    ProductMaster image = objDbEntities.ProductMasters.Where(s => s.ProductId == value).FirstOrDefault();
                    //As you can see the use is stupid simple.  Just get the image bytes and the
                    //  saved content type.  See this is where the contentType comes in real handy.
                    //ImageResult result = new ImageResult(image.PictureContent, image.ContentType);

                    ImageResult asaas = new ImageResult(image.PictureContent1, image.ContentType1);

                    return asaas;
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

    }
}