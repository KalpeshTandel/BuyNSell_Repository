using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using BuyNSell.Models;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;

namespace BuyNSell.Controllers.DataAccess
{
    public class DataAccess
    {
        //public ProductList_ViewModel GetProductList(string SearchText, int Start, int End, string OrderBy)
        //{
        //    SqlDataReader sdr;
        //    ProductList_ViewModel objPL;
        //    SqlParameter[] param;

        //    try
        //    {
        //        param = new SqlParameter[4];

        //        param[0] = new SqlParameter("@SearchText", SearchText);
        //        param[1] = new SqlParameter("@Start", Start);
        //        param[2] = new SqlParameter("@End", End);
        //        param[3] = new SqlParameter("@OrderBy", OrderBy);

        //        sdr = SqlHelper.ExecuteReader(System.Configuration.ConfigurationManager.ConnectionStrings["BuyNSell_DbEntities_Ado"].ConnectionString, CommandType.StoredProcedure, "Sp_GetProductData", param);

        //    objPL = new ProductList_ViewModel();

        //        while (sdr.Read())
        //        {
        //            objPL.TotalRecords = sdr.GetInt32(0);
        //        }

        //        sdr.NextResult();

        //        objPL.listProduct = new GenericDataPopulator<ProductMaster>().CreateList(sdr);

        //        sdr.Close();

        //        return objPL;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        sdr = null;
        //        objPL = null;
        //    }
        //}




        public List<ProductList_ViewModel> GetProductList(string SearchText, int Start, int End, string OrderBy)
        {
            SqlDataReader sqldr;
            SqlParameter[] param;
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["BuyNSell_DbEntities_Ado"].ConnectionString);

            List<ProductList_ViewModel> ProductList = new List<ProductList_ViewModel>();
            try
            {
                //sqlConn.Open();
                param = new SqlParameter[4];

                param[0] = new SqlParameter("@SearchText", SearchText);
                param[1] = new SqlParameter("@Start", Start);
                param[2] = new SqlParameter("@End", End);
                param[3] = new SqlParameter("@OrderBy", OrderBy);

                sqldr = SqlHelper.ExecuteReader(sqlConn , CommandType.StoredProcedure, "Sp_GetProductData", param);

                //if (sqldr.HasRows)
                //{
                    while (sqldr.Read())
                    {
                        ProductList.Add(CreateProductList(sqldr));
                    }

                    sqldr.NextResult();

                    while (sqldr.Read())
                    {
                        HttpContext context = HttpContext.Current;
                        context.Session["TotalRecords"] = Convert.ToInt32(sqldr["TotalRecords"]);
                    }

                sqldr.Close();

                return ProductList;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                //sqlConn.Close();
            }
        }


        private ProductList_ViewModel CreateProductList(IDataRecord Record)
        {
            try
            {
                ProductList_ViewModel objProduct = new ProductList_ViewModel();

                objProduct.SrNo = Convert.ToInt32(Record["SrNo"]);
                objProduct.ProductId = Convert.ToInt32(Record["ProductId"]);
                objProduct.ProductName = Record["ProductName"].ToString();
                objProduct.ProductDescription = Record["ProductDescription"].ToString();
                objProduct.UserId = Convert.ToInt32(Record["UserId"]);
                objProduct.ProductCategoryId = Convert.ToInt32(Record["ProductCategoryId"]);
                objProduct.Quantity = Convert.ToInt32(Record["Quantity"]);
                objProduct.Active = Convert.ToBoolean(Record["Active"]);
                objProduct.Deleted = Convert.ToBoolean(Record["Deleted"]);
                objProduct.AddedDate = Convert.ToDateTime(Record["AddedDate"]);
                objProduct.PictureId = Convert.ToInt32(Record["PictureId"]);
                objProduct.PictureContent = (byte[])Record["PictureContent"];
                objProduct.Price = Convert.ToInt32(Record["Price"]);
                objProduct.UserName = Record["UserName"].ToString();
                objProduct.ProductCategoryName = Record["ProductCategoryName"].ToString();
                //objProduct.TotalRecords = Convert.ToInt32(Record["TotalRecords"]);
  
                return objProduct;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public class GenericDataPopulator<T>
            {
                public List<T> CreateList(SqlDataReader dr)
                {
                    // create list of whatso soever list type
                    var lst = new List<T>();

                    // get all propreites of the type T
                    var properties = typeof(T).GetProperties();
                    // readd from datareader
                    List<string> _columlist = SqlColumnList.ColumnList(dr);
                    while (dr.Read())
                    {
                        // crate instances of the class
                        // crate instances of the class
                        var item = Activator.CreateInstance<T>();
                        foreach (var property in typeof(T).GetProperties())
                        {
                            if (_columlist.Any(property.Name.ToString().ToUpper().Equals))
                            {

                                if (!dr.IsDBNull(dr.GetOrdinal(property.Name)))
                                {
                                    Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                    property.SetValue(item, Convert.ChangeType(dr[property.Name], convertTo), null);
                                }
                            }
                        }
                        lst.Add(item);
                    }
                    return lst;
                }

            }


            public static class SqlColumnList
            {
                public static List<string> ColumnList(SqlDataReader dr)
                {
                    List<string> lst = new List<string>();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        lst.Add(dr.GetName(i).ToUpper());
                    }
                    return lst;
                }
            }


        }


    }
