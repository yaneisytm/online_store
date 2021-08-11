using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreAppMVC.Models.ValidationAttributes
{
    public class IntegerParse : ValidationAttribute
    {
        string errorMsg { get; }

        public IntegerParse(string message = "Not a valid number.")
        {
            errorMsg = message;
        }

        public string ParseErrorMessage() => errorMsg;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            try
            {
                var parsedValue = int.Parse((string)value);
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(ParseErrorMessage());
            }
        }
    }
}