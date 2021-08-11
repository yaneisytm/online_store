using OnlineStoreAppMVC.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreAppMVC.Models.ValidationAttributes
{
    class NotExpiredCard : ValidationAttribute
    {
        public string GetErrorMessage() =>
            $"Cards must not expire before today.";

        public string ParseErrorMessage() =>
            $"Not a valid date.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var card = (CardViewModel)validationContext.ObjectInstance;

            try
            {
                var releaseYear = int.Parse("20" + (string)value);
                var today = DateTime.Today;

                var expMonth = int.Parse(card.ExpMonth);

                if (releaseYear < today.Year || (releaseYear == today.Year && today.Month > expMonth))
                {
                    return new ValidationResult(GetErrorMessage());
                }
                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult(ParseErrorMessage());
            }
        }
    }
}
