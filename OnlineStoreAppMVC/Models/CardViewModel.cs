using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreAppMVC.Models
{
    public class CardViewModel
    {
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        [StringLength(16, ErrorMessage="Invalid card number length")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Card Holder")]
        public string CardHolder { get; set; }

        [Required]
        [Display(Name = "Expiration Month")]
        [StringLength(2, ErrorMessage = "Invalid month length")]
        public string ExpMonth { get; set; }

        [Required]
        [Display(Name = "Expiration Year")]
        [StringLength(2, ErrorMessage = "Invalid year length")]
        public string ExpYear { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Invalid cvc", MinimumLength=3)]
        public string CVC { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
    }
}