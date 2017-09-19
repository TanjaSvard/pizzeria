using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class Dish
    {     
        public int DishId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        public List<DishIngredient> DishIngredients { get; set; }

        [Required(ErrorMessage = "Choose Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
