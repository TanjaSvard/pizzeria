using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagotti.Services
{
    public class CartCalculationService
    {
        public int TotalForCart(ShoppingCart cart)
        {
            var tot = 0;
            foreach (var item in cart.CartItems)
            {
                tot += item.Dish.Price * item.Quantity;
            }     
            return tot;
        }

        public int TotalPriceForCartItemIngredients(CartItem cartItem)
        {
            var tot = 0;
            //lista m dishIngredientsID för dishen i cartitem
            var listOfDishIngredientsId = cartItem.Dish.DishIngredients.Select(c=>c.IngredientId);
            //lista m cartItemIngredientsId för cartitem
            var listOfCartItemIngredientsId = cartItem.CartItemIngredients.Select(c=>c.IngredientId);
            //tar bort alla dishingredients från cartitemingredients
            var extraId = listOfCartItemIngredientsId.Except(listOfDishIngredientsId);

            foreach (var ingredientId in extraId)
            {
                tot += (cartItem.CartItemIngredients
                    .FirstOrDefault(m => m.IngredientId == ingredientId))
                    .Ingredient.Price;
            }         
            return tot;
        }
    }
}
