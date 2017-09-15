using System;
using System.Collections.Generic;
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
        public int CardNumber { get; set; }
        public int CVC { get; set; }
        public DateTime ExpireDate { get; set; }



    }
}
