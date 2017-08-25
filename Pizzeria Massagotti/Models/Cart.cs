using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class Cart
    {
        public int DishId { get; set; }
        public int OrderId { get; set; }
        public List<Dish> DishList { get; set; }
        public int Quantity { get; set; }

    }
}
