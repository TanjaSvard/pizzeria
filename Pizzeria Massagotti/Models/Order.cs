using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public bool Anonymous { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime OrderDateTime { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "The name can only be 100 characters long")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "The delivery can only be 100 characters long")]
        [Display(Name = "Delivery Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [MaxLength(5, ErrorMessage = "The name can only be 100 characters long")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Credit card number is required")]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Credit Card")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVC number is required")]
        [Display(Name = "CVC")]
        public string CVC { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Range(2017,2050)]
        //[DisplayFormat(DataFormatString ="YYYY-MM", ApplyFormatInEditMode = true)]
        //public DateTime ExpireDate { get; set; }

        [Required]
        [Display(Name = "Month")]
        public string ExpireMonth { get; set; }
        [Required]
        [Display(Name = "Year")]
        public string ExpireYear { get; set; }
    }
}
