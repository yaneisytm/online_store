using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStoreAppMVC.Models.ValidationAttributes
{
    public class ValidMonth : ValidationAttribute
    {
        string errorMsg { get; }

        public ValidMonth(string message = "Not a valid month.")
        {
            errorMsg = message;
        }

        public string MonthErrorMessage() => errorMsg;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            try
            {
                var parsedValue = int.Parse((string)value);
                if (parsedValue < 1 || parsedValue > 12)
                    return new ValidationResult(MonthErrorMessage());
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(MonthErrorMessage());
            }
        }
    }
}