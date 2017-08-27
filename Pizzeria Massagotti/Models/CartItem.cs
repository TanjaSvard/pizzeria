using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
