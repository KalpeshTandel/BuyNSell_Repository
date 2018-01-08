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
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
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
                    return RedirectToAction("Login", "Login");
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

                        //ProductPicture(objPM);
                        objDbEntities.ProductMasters.Add(objPM);
                        objDbEntities.SaveChanges();

                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];

                            PictureMaster objPic = new PictureMaster();
                            objPic.PictureName = file.FileName;
                            objPic.ProductId = objPM.ProductId;
                            objPic.ContentType = file.ContentType;
                            objPic.Deleted = false;
                            objPic.PictureAddedDate = DateTime.Now;

                            Int32 Length = file.ContentLength;
                            byte[] Image = new byte[Length];
                            file.InputStream.Read(Image, 0, Length);
                            objPic.PictureContent = Image;

                            objDbEntities.PictureMasters.Add(objPic);
                            objDbEntities.SaveChanges();

                        }
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
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

                    //var Result = objDbEntities.ProductMasters
                    //           .Join(objDbEntities.UserMasters, p => p.UserId, u => u.UserId, (p, u) => new { p, u })
                    //           .Join(objDbEntities.PictureMasters, pic => pic.ProductId, abc =>abc.ProductId)
                    //           .Join(objDbEntities.ProductCategoryMasters, pc => pc.p.ProductCategoryId, p => p.ProductCategoryId, (p, pc) =>
                    //           new
                    //           {
                    //               ProductId = p.p.ProductId,
                    //               ProductName = p.p.ProductName,
                    //               ProductDescription = p.p.ProductDescription,
                    //               UserId = p.u.UserId,
                    //               FirstName = p.u.FirstName,
                    //               LastName = p.u.LastName,
                    //               ProductCategoryId = pc.ProductCategoryId,
                    //               ProductCategoryName = pc.ProductCategoryName,
                    //               Quantity = p.p.Quantity,
                    //               Active = p.p.Active,
                    //               Deleted = p.p.Deleted,
                    //               AddedDate = p.p.AddedDate,
                    //               Price = p.p.Price
                    //           })
                    //           .Where(c => c.ProductId == ProductId)
                    //           .Select(a => new
                    //           {
                    //               ProductId = a.ProductId,
                    //               ProductName = a.ProductName,
                    //               ProductDescription = a.ProductDescription,
                    //               UserId = a.UserId,
                    //               FirstName = a.FirstName,
                    //               LastName = a.LastName,
                    //               ProductCategoryId = a.ProductCategoryId,
                    //               ProductCategoryName = a.ProductCategoryName,
                    //               Quantity = a.Quantity,
                    //               Active = a.Active,
                    //               Deleted = a.Deleted,
                    //               AddedDate = a.AddedDate,
                    //               Price = a.Price
                    //           }).FirstOrDefault();

                    var Result = (from pm in objDbEntities.ProductMasters
                                  join um in objDbEntities.UserMasters on pm.UserId equals um.UserId
                                  join pcm in objDbEntities.ProductCategoryMasters on pm.ProductCategoryId equals pcm.ProductCategoryId
                                  join picm in objDbEntities.PictureMasters on pm.ProductId equals picm.ProductId
                                  where pm.ProductId == ProductId
                                  orderby picm.PictureId
                                  select new
                                  {
                                      ProductId = pm.ProductId,
                                      ProductName = pm.ProductName,
                                      ProductDescription = pm.ProductDescription,
                                      UserId = pm.UserId,
                                      FirstName = um.FirstName,
                                      LastName = um.LastName,
                                      ProductCategoryId = pm.ProductCategoryId,
                                      ProductCategoryName = pcm.ProductCategoryName,
                                      Quantity = pm.Quantity,
                                      Active = pm.Active,
                                      Deleted = pm.Deleted,
                                      AddedDate = pm.AddedDate,
                                      PictureId = picm.PictureId,
                                      PictureContent = picm.PictureContent,
                                      Price = pm.Price
                                  }).FirstOrDefault();

                    ProductDetails.SrNo = 1;
                    ProductDetails.ProductId = Result.ProductId;
                    ProductDetails.ProductName = Result.ProductName;
                    ProductDetails.ProductDescription = Result.ProductDescription;
                    ProductDetails.UserId = Result.UserId;
                    ProductDetails.UserName = Result.FirstName + ' ' + Result.LastName;
                    ProductDetails.ProductCategoryId = Result.ProductCategoryId;
                    ProductDetails.ProductCategoryName = Result.ProductCategoryName;
                    ProductDetails.Quantity = Result.Quantity;
                    ProductDetails.Active = Result.Active;
                    ProductDetails.Deleted = Result.Deleted;
                    ProductDetails.AddedDate = Result.AddedDate;
                    ProductDetails.PictureId = Result.PictureId;
                    ProductDetails.PictureContent = Result.PictureContent;
                    ProductDetails.Price = Result.Price;

                    var PictureCount = objDbEntities.PictureMasters.Where(pic => pic.ProductId == ProductId).Count();
                    Session["PictureCount"] = PictureCount;
                    Session["PictureNumber"] = 0;

                    return PartialView("_ViewProductDetails", ProductDetails);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult btnnext_Click(int ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    int PictureCount = Convert.ToInt32(Session["PictureCount"]);
                    Session["PictureNumber"] = Convert.ToInt32(Session["PictureNumber"]) + 1;
                    int PictureNumber = Convert.ToInt32(Session["PictureNumber"]);

                    //var AllPictures =  (from picm in objDbEntities.PictureMasters
                    //                   where picm.ProductId == ProductId

                    //                   orderby picm.PictureId

                    //                   select new
                    //                   {
                    //                       PictureId = picm.PictureId,
                    //                       PictureContent = picm.PictureContent
                    //                   }).ToList();

                    if(PictureNumber < PictureCount && PictureNumber >= 0)
                    {
                        var AllPictures = objDbEntities.PictureMasters.Where(p => p.ProductId == ProductId).OrderBy(p => p.PictureId).Select(p => new { PictureId = p.PictureId, PictureContent = p.PictureContent }).ToList();

                        var PictureContent = AllPictures[PictureNumber].PictureContent;

                        var PictureData = Convert.ToBase64String(PictureContent);

                        var src = "data:image; base64," + PictureData;

                        return Json(src, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var NoPicture = "No Picture";
                        return Json(NoPicture, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult btnPreviousPicture_click(int ProductId)
        {
            try
            {
                if (Session["UserId"] != null)
                {
                    int PictureCount = Convert.ToInt32(Session["PictureCount"]);
                    Session["PictureNumber"] = Convert.ToInt32(Session["PictureNumber"]) - 1;
                    int PictureNumber = Convert.ToInt32(Session["PictureNumber"]);

                    if (PictureNumber < PictureCount && PictureNumber >= 0)
                    {
                        var AllPictures = objDbEntities.PictureMasters.Where(p => p.ProductId == ProductId).OrderBy(p => p.PictureId).Select(p => new { PictureId = p.PictureId, PictureContent = p.PictureContent }).ToList();

                        var PictureContent = AllPictures[PictureNumber].PictureContent;

                        var PictureData = Convert.ToBase64String(PictureContent);

                        var src = "data:image; base64," + PictureData;

                        return Json(src, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        var NoPicture = "No Picture";
                        return Json(NoPicture, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public ProductMaster ProductPicture(ProductMaster objPM)
        //{
        //    try
        //    {

        //        //HttpPostedFileBase[] file1 = Request.Files["SelectPicture1"];
        //        HttpFileCollectionBase files = Request.Files;

        //        HttpPostedFileBase file1 = files[0];
        //        HttpPostedFileBase file2 = files[1];

        //       // HttpPostedFileBase file2 = Request.Files["SelectPicture2"];
        //        HttpPostedFileBase file3 = Request.Files["SelectPicture3"];

        //        if (file1 != null)
        //        {

        //            objPM.ContentType1 = file1.ContentType;

        //            Int32 Length = file1.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file1.InputStream.Read(tempImage, 0, Length);
        //            objPM.PictureContent1 = tempImage;

        //          }

        //        if (file2 != null)
        //        {

        //            objPM.ContentType2 = file2.ContentType;

        //            Int32 Length = file2.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file2.InputStream.Read(tempImage, 0, Length);
        //            objPM.PictureContent2 = tempImage;

        //        }

        //        if (file3 != null)
        //        {

        //            objPM.ContentType3 = file3.ContentType;

        //            Int32 Length = file3.ContentLength;
        //            byte[] tempImage = new byte[Length];
        //            file3.InputStream.Read(tempImage, 0, Length);
        //            objPM.PictureContent3 = tempImage;

        //        }

        //        return objPM;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


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



        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult ShowPhoto(Int32 ProductId)
        //{
        //    try
        //    {
        //        if (Session["UserId"] != null)
        //        {

        //            int value = ProductId;
        //            //This is my method for getting the image information
        //            // including the image byte array from the image column in
        //            // a database.
        //            //PictureMaster image = objDb.PictureMasters.Where(s => s.PictureId == 1).FirstOrDefault();
        //            ProductMaster image = objDbEntities.ProductMasters.Where(s => s.ProductId == value).FirstOrDefault();
        //            //As you can see the use is stupid simple.  Just get the image bytes and the
        //            //  saved content type.  See this is where the contentType comes in real handy.
        //            //ImageResult result = new ImageResult(image.PictureContent, image.ContentType);

        //            ImageResult asaas = new ImageResult(image.PictureContent1, image.ContentType1);

        //            return asaas;
        //        }
        //        else
        //        {
        //            return RedirectToAction("LogInAgain", "Login");
        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

    }
}