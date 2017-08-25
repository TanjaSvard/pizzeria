using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class OrderDish
    {
        public Dish Dish { get; set; }
        public int DishId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
