using PizzeriaMassagotti.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaMassagotti.Models
{
    public class Ingredient
    {
        
        public int IngredientId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}