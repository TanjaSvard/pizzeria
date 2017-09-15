using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Services;


namespace PizzeriaMassagottiUnitTest
{
   public class PriceCalculationTests
    {
        [Fact]
        public void TotalPriceForCart()
        {
            var dish = new Dish { Name = "Capricciosa", DishId = 1, Price = 80, DishIngredients = new List<DishIngredient>() };
            var ingredient1 = new Ingredient { Name = "Bacon", IngredientId = 1, Price = 10 };
            var ingredient2 = new Ingredient { Name = "Cheese", IngredientId = 2, Price = 5 };
            var dishIngredients1 = new DishIngredient { DishId = 1, IngredientId = 1, Dish = dish, Ingredient = ingredient1 };
            var dishIngredients2 = new DishIngredient { DishId = 1, IngredientId = 2, Dish = dish, Ingredient = ingredient2 };
            dish.DishIngredients.Add(dishIngredients1);
            dish.DishIngredients.Add(dishIngredients2);
            var  shoppingCart = new ShoppingCart { ShoppingCartId = 4, CartItems = new List<CartItem>() };
            var cartItems = new CartItem { DishId = 1, CartItemId = 1, ShoppingCartId = 4, Quantity = 1, Price = 80, Dish = dish };
            shoppingCart.CartItems.Add(cartItems);

            var calculationService = new CartCalculationService();
            var totalAmount = calculationService.TotalForCart(shoppingCart);

            Assert.Equal(80, totalAmount);
        }



        [Fact]
        public void TotalPriceFor_CartItemIngredients_ForSpecificCartItem()
        {
            var dish = new Dish { Name = "Capricciosa", DishId = 1, Price = 80, DishIngredients = new List<DishIngredient>() };
            var ingredient1 = new Ingredient { Name = "Bacon", IngredientId = 1, Price = 10 };
            var ingredient2 = new Ingredient { Name = "Cheese", IngredientId = 2, Price = 5 };
            var ingredient3 = new Ingredient { Name = "Extra", IngredientId = 3, Price = 15 };
            var dishIngredients1 = new DishIngredient { DishId = 1, IngredientId = 1, Dish = dish, Ingredient = ingredient1 };
            var dishIngredients2 = new DishIngredient { DishId = 1, IngredientId = 2, Dish = dish, Ingredient = ingredient2 };
            dish.DishIngredients.Add(dishIngredients1);
            dish.DishIngredients.Add(dishIngredients2);                        
            var cartItem = new CartItem {CartItemId = 1, Dish=dish, DishId =1, CartItemIngredients = new List<CartItemIngredient>()};
            var cartItemIngredient1 = new CartItemIngredient { CartItemId = 1, CartItem = cartItem, IngredientId = 1, Ingredient = ingredient1 };
            var cartItemIngredient2 = new CartItemIngredient { CartItemId = 2, CartItem = cartItem, IngredientId = 2, Ingredient = ingredient2 };
            var cartItemIngredient3 = new CartItemIngredient { CartItemId = 3, CartItem = cartItem, IngredientId = 3, Ingredient = ingredient3 };
            cartItem.CartItemIngredients.Add(cartItemIngredient1);
            cartItem.CartItemIngredients.Add(cartItemIngredient2);
            cartItem.CartItemIngredients.Add(cartItemIngredient3);


            var calculationService = new CartCalculationService();
            var totalPrice = calculationService.TotalPriceForCartItemIngredients(cartItem);

            Assert.Equal(15, totalPrice);
        }
    }
}
