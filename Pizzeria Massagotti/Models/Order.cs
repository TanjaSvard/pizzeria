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

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "The name can only be 100 characters long")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "The delivery can only be 100 characters long")]
        [Display(Name = "Delivery Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [MaxLength(5, ErrorMessage = "The name can only be 5 characters long")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(50, ErrorMessage = "Too long e-mail address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage ="Email is invalid")]
        //[EmailAddress(ErrorMessage ="Email is invalid")]
        [Display(Name = "Email")]
        public string Email { get; set; }
     
        [Required(ErrorMessage = "Credit card number is required")]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Invalid, can only contain digits, max 16")]   
        [Display(Name = "Credit Card")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "CVC number is required")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Invalid, can only contain 3 digits")]
        [Display(Name = "CVC")]
        public string CVC { get; set; }

           
        [Display(Name = "Month")]
        public string ExpireMonth { get; set; }
        
        [Display(Name = "Year")]
        public string ExpireYear { get; set; }
    }
}
