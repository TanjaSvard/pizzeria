using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }
        public List<CartItem> CartItems { get; set; }  
        public int OrderId { get; set; }        
        public int TotalPrice { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
