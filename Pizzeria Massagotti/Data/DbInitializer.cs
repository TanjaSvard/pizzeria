using PizzeriaMassagotti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PizzeriaMassagotti.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var studentUser = new ApplicationUser
            {
                UserName = "student@test.se",
                Email = "student@test.se",
                Orders = new List<Order>(),
                Name = "Tanja Svärd",
                Address = "Tröskvägen 26",
                ZipCode = "17552",
                City = "Järfälla"
            };

            var studentUserResult = userManager.CreateAsync(studentUser, "Pa$$w0rd").Result;

            var teacherUser = new ApplicationUser
            {
                UserName = "teacher@test.se",
                Email = "teacher@test.se"
            };
            var teacherUserResult = userManager.CreateAsync(teacherUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole { Name = "Admin" };
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser
            {
                UserName = "admin@test.se",
                Email = "admin@test.se"
            };
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
            userManager.AddToRoleAsync(adminUser, "Admin");


           



            //todo refactor
            //todo add orders and orderdishes
            //if (context.Dishes.ToList().Count == 0)
            //{
            // var capricciosa = new Dish { Name = "Capricciosa", Price = 90 };
            // var margaritha = new Dish { Name = "Margherita", Price = 69 };
            // var hawaii = new Dish { Name = "Hawaii", Price = 85 };
            // context.AddRange(capricciosa, margaritha, hawaii);
            // context.SaveChanges();
            //}



            if (!context.Dishes.Any())
            {
                var cheese = new Ingredient { Name = "Cheese", Price = 5 };
                var ham = new Ingredient { Name = "Ham", Price = 10 };
                var tomatoSauce = new Ingredient { Name = "Tomato", Price = 5 };
                var mushroom = new Ingredient { Name = "Mushroom", Price = 5 };
                var shrimp = new Ingredient { Name = "Shrimp", Price = 10 };
                var tuna = new Ingredient { Name = "Tuna" , Price = 7};
                var pineapple = new Ingredient { Name = "Pineapple", Price = 7 };
                var curry = new Ingredient { Name = "Curry", Price = 3 };
                var bacon = new Ingredient { Name = "Bacon", Price = 10 };
                var banana = new Ingredient { Name = "Banana", Price = 5 };
                var salmon = new Ingredient { Name = "Salmon", Price = 15};

                var pizza = new Category { Name = "Pizza" };
                var tropicalPizza = new Category { Name = "Tropical Pizza" };
                var specialPizza = new Category { Name = "Special Pizza" };

                var margherita = new Dish { Name = "Margherita", Price = 89, Category = pizza};
                var fungi = new Dish { Name = "Fungi", Price = 91, Category = specialPizza };
                var capricciosa = new Dish { Name = "Capricciosa", Price = 99, Category = pizza };
                var hawaii = new Dish { Name = "Hawaii", Price = 99, Category = tropicalPizza };

                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };
                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };

                var fungiTomatoSauce = new DishIngredient { Dish = fungi, Ingredient = tomatoSauce };
                var fungiCheese = new DishIngredient { Dish = fungi, Ingredient = cheese };
                var fungiMushroom = new DishIngredient { Dish = fungi, Ingredient = mushroom };

                var hawaiiTomatoSauce = new DishIngredient { Dish = hawaii, Ingredient = tomatoSauce };
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese };
                var hawaiiCurry = new DishIngredient { Dish = hawaii, Ingredient = curry };
                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawaiiMushroom = new DishIngredient { Dish = hawaii, Ingredient = mushroom };
                var hawaiiBanana = new DishIngredient { Dish = hawaii, Ingredient = banana };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaTomatoSauce);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaCheese);

                margherita.DishIngredients = new List<DishIngredient>();
                margherita.DishIngredients.Add(margheritaCheese);
                margherita.DishIngredients.Add(margheritaTomatoSouce);
                

                fungi.DishIngredients = new List<DishIngredient>();
                fungi.DishIngredients.Add(fungiMushroom);
                fungi.DishIngredients.Add(fungiCheese);
                fungi.DishIngredients.Add(fungiTomatoSauce);

                hawaii.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients.Add(hawaiiTomatoSauce);
                hawaii.DishIngredients.Add(hawaiiCheese);
                hawaii.DishIngredients.Add(hawaiiCurry);
                hawaii.DishIngredients.Add(hawaiiHam);
                hawaii.DishIngredients.Add(hawaiiMushroom);
                hawaii.DishIngredients.Add(hawaiiBanana);
                hawaii.DishIngredients.Add(hawaiiPineapple);

               

                var firstOrder = new Order();
                firstOrder.OrderDateTime = DateTime.Now;
                firstOrder.ApplicationUser = studentUser;

                var firstOrderHawaii = new OrderDish { Dish = hawaii, Order = firstOrder };
                var firstOrderFungi = new OrderDish { Dish = fungi, Order = firstOrder };

                var firstOrderDishes = new List<OrderDish>();
                firstOrderDishes.Add(firstOrderHawaii);
                firstOrderDishes.Add(firstOrderFungi);
                firstOrder.OrderDishes = firstOrderDishes;

                foreach (var dish in firstOrder.OrderDishes)
                {
                    firstOrder.TotalAmount = firstOrder.TotalAmount + dish.Dish.Price;
                }
                //firstOrder.TotalAmount = firstOrder.OrderDishes.ForEach(dish => dish.Dish.Price += dish.Dish.Price);

                //var secondOrder = new Order();

                studentUser.Orders.Add(firstOrder);

                context.Orders.AddRange(firstOrder);
                context.OrderDishes.AddRange(firstOrderFungi, firstOrderHawaii);
                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana, pineapple, shrimp, tuna);
                context.Dishes.AddRange(capricciosa, margherita, hawaii, fungi);
                context.DishIngredients.AddRange(capricciosaTomatoSauce, capricciosaCheese,
                    capricciosaHam, margheritaCheese, margheritaTomatoSouce, fungiMushroom,
                    fungiCheese, fungiTomatoSauce, hawaiiPineapple, hawaiiBanana, hawaiiCheese,
                    hawaiiCurry, hawaiiHam, hawaiiMushroom, hawaiiTomatoSauce);
                context.SaveChanges();
            }
        }
    }
}