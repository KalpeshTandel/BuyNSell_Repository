using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace BuyNSell.Models
{

    public class CustomValidationDataAnnotation
    {
    }

    public class EmailIdExistOrNot : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            BuyNSell_DbEntities objDB = new BuyNSell_DbEntities();

            if (value != null)
            {

                String EmailId = value.ToString();

                UserMaster CheckEmailId = objDB.UserMasters.Where(e => e.EmailId.Equals(EmailId)).FirstOrDefault();

                if (CheckEmailId != null)
                {
                    return new ValidationResult("EmailId Already Exist");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return null;    
        }
    }


}