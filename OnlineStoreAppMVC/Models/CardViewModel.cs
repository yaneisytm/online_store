using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnlineStoreAppMVC.Models.ValidationAttributes;

namespace OnlineStoreAppMVC.Models
{
    public class CardViewModel
    {
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        [StringLength(16, ErrorMessage="Invalid card number length.")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Card Holder")]
        public string CardHolder { get; set; }

        [Required]
        [Display(Name = "Expiration Month")]
        [IntegerParse("Not a valid month.")]
        [StringLength(2, ErrorMessage = "Invalid month length.")]
        public string ExpMonth { get; set; }

        [Required]
        [Display(Name = "Expiration Year")]
        [NotExpiredCard]
        [StringLength(2, ErrorMessage = "Invalid year length.")]
        public string ExpYear { get; set; }

        [Required]
        [IntegerParse("Not a valid CVC.")]
        [StringLength(4, ErrorMessage = "Invalid CVC.", MinimumLength=3)]
        public string CVC { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}