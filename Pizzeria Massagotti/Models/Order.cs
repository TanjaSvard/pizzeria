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
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "CreditCard")]
        public string CardNumber { get; set; }
        [Required]
        public string CVC { get; set; }

        //[Required]
        //[DataType(DataType.Date)]
        //[Range(2017,2050)]
        //[DisplayFormat(DataFormatString ="YYYY-MM", ApplyFormatInEditMode = true)]
        //public DateTime ExpireDate { get; set; }

        [Required]
        public string ExpireMonth { get; set; }
        [Required]
        public string ExpireYear { get; set; }
    }
}
