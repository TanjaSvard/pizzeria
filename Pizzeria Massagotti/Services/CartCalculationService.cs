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
            //lista m dishIngredients för dishen i cartitem
            var listOfDishIngredients = cartItem.Dish.DishIngredients.Where(c => c.DishId == cartItem.DishId);
          
            var tot = 0;
            foreach (var cartItemIng in cartItem.CartItemIngredients)//går ignm CartItemIngredienter
            {
                foreach (var dishIng in listOfDishIngredients)//går ignm DishIngredienter
                {
                    if (cartItemIng.IngredientId == dishIng.IngredientId)
                    {
                        tot += 0;
                        
                    }
                    else
                    {
                        tot += cartItemIng.Ingredient.Price;//lägger till pris bara ifall ing inte finns på dishen
                    }
                }               
            }
            return tot;
        }
    }
}
