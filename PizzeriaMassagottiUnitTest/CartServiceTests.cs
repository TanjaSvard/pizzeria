using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaMassagotti.Data;
using PizzeriaMassagotti.Services;
using Xunit;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagottiUnitTest
{
    class CartServiceTests : BasePizzeriaTests
    {

        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Dishes.Add(new Dish {Name = "Capricciosa", DishId = 1, Price = 80, DishIngredients = new List<DishIngredient>()});
            context.Ingredients.Add(new Ingredient { Name = "Bacon", IngredientId = 1, Price = 10});
            context.Ingredients.Add(new Ingredient { Name = "Cheese", IngredientId = 2, Price = 5});
            context.DishIngredients.Add(new DishIngredient { DishId = 1, IngredientId = 1});
            context.DishIngredients.Add(new DishIngredient { DishId = 1, IngredientId = 2});
            context.CartItems.Add(new CartItem { DishId = 1, CartItemId = 1, ShoppingCartId = 4, Quantity = 1});
            context.ShoppingCart.Add(new ShoppingCart {ShoppingCartId = 4, CartItems = new List<CartItem>()});
            context.SaveChanges();
        }

        [Fact]
        public void CalculateTotalAmountInCartBasedOnDishPrice()
        {
            //Arrange
            var _items= serviceProvider.GetService<CartService>();

            //Act
            var result = _items.TotalAmount(4);

            //Assert
            Assert.Equal(80, result);

        }
    }
}
