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
                var chicken = new Ingredient { Name = "Chicken", Price = 10 };
                var pepperoni = new Ingredient { Name = "Pepperoni", Price = 3 };
                var olives = new Ingredient { Name = "Olives", Price = 3 };
                var mozarella = new Ingredient { Name = "Mozarella", Price = 5 };
                var extraTomato = new Ingredient { Name = "Tomato, extra", Price = 3 };
                var extraCheese = new Ingredient { Name = "Cheese, extra", Price = 3 };
                var chili = new Ingredient { Name = "Cheese, extra", Price = 3 };



                //Categorier
                var classicPizza = new Category { Name = "Classic pizza" };
                var specialPizza = new Category { Name = "Special Pizza" };
                var mexicanPizza = new Category { Name = "Mexican Pizza" };
                var delicatessenPizza = new Category { Name = "Delicatessen Pizza" };


                //Dishes - Pizzor
                var margherita = new Dish { Name = "Margherita", Price = 75, Category = classicPizza };
                var vesuvio = new Dish { Name = "Vesuvio", Price = 75, Category = classicPizza };
                var alFungi = new Dish { Name = "Al Fungi", Price = 75, Category = classicPizza };
                var alTonno = new Dish { Name = "Al Tonno", Price = 75, Category = classicPizza };


                var bussola = new Dish { Name = "Bussola", Price = 80, Category = specialPizza };
                var opera = new Dish { Name = "Opera", Price = 80, Category = specialPizza };
                var capricciosa = new Dish { Name = "Capricciosa", Price = 80, Category = specialPizza };
                


                var mexicana = new Dish { Name = "Mexicana", Price = 95, Category = mexicanPizza };
                var azteka = new Dish { Name = "Azteka", Price = 95, Category = mexicanPizza };
               

                var venice = new Dish { Name = "Venice", Price = 95, Category = delicatessenPizza };
                var leChef = new Dish { Name = "LeChef", Price = 95, Category = delicatessenPizza };
              

                //DishIngredienter
                var margheritaCheese = new DishIngredient { Dish = margherita, Ingredient = cheese };
                var margheritaTomatoSouce = new DishIngredient { Dish = margherita, Ingredient = tomatoSauce };

                var vesuvioCheese = new DishIngredient { Dish = vesuvio, Ingredient = cheese };
                var vesuvioTomatoSouce = new DishIngredient { Dish = vesuvio, Ingredient = tomatoSauce };
                var vesuvioHam = new DishIngredient { Dish = vesuvio, Ingredient = ham};

                var alFungiTomatoSauce = new DishIngredient { Dish = alFungi, Ingredient = tomatoSauce };
                var alFungiCheese = new DishIngredient { Dish = alFungi, Ingredient = cheese };
                var alFungiMushroom = new DishIngredient { Dish = alFungi, Ingredient = mushroom };


                var alTonnoTomatoSauce = new DishIngredient { Dish = alTonno, Ingredient = tomatoSauce };
                var alTonnoCheese = new DishIngredient { Dish = alTonno, Ingredient = cheese };
                var alTonnoTuna = new DishIngredient { Dish = alTonno, Ingredient = tuna };

                var bussolaTomatoSauce = new DishIngredient { Dish = bussola, Ingredient = tomatoSauce };
                var bussolaCheese = new DishIngredient { Dish = bussola, Ingredient = cheese };
                var bussolaHam = new DishIngredient { Dish = bussola, Ingredient = ham };
                var bussolaShrimp = new DishIngredient { Dish = bussola, Ingredient = shrimp};


                var operaTomatoSauce = new DishIngredient { Dish = opera, Ingredient = tomatoSauce };
                var operaCheese = new DishIngredient { Dish = opera, Ingredient = cheese };
                var operaTuna = new DishIngredient { Dish = opera, Ingredient = tuna };
                var operaHam = new DishIngredient { Dish = opera, Ingredient = ham };


                var capricciosaTomatoSauce = new DishIngredient { Dish = capricciosa, Ingredient = tomatoSauce };
                var capricciosaCheese = new DishIngredient { Dish = capricciosa, Ingredient = cheese };
                var capricciosaMushroom = new DishIngredient { Dish = capricciosa, Ingredient = mushroom };
                var capricciosaHam = new DishIngredient { Dish = capricciosa, Ingredient = ham };

                var mexicanaTomatoSauce = new DishIngredient { Dish = mexicana, Ingredient = tomatoSauce };
                var mexicanaCheese = new DishIngredient { Dish = mexicana, Ingredient = cheese };
                var mexicanaPepperoni = new DishIngredient { Dish = mexicana, Ingredient = pepperoni };
                var mexicanaChili = new DishIngredient { Dish = mexicana, Ingredient = chili };
                var mexicanaBacon = new DishIngredient { Dish = mexicana, Ingredient = bacon };

                var aztekaTomatoSauce = new DishIngredient { Dish = azteka, Ingredient = tomatoSauce };
                var aztekaCheese = new DishIngredient { Dish = azteka, Ingredient = cheese };
                var aztekaChili = new DishIngredient { Dish = azteka, Ingredient = chili };
                var aztekaHam = new DishIngredient { Dish = azteka, Ingredient = ham };

                var veniceTomatoSauce = new DishIngredient { Dish = venice, Ingredient = tomatoSauce };
                var veniceCheese = new DishIngredient { Dish = venice, Ingredient = cheese };
                var veniceMozarella = new DishIngredient { Dish = venice, Ingredient = mozarella };

                var leChefTomatoSauce = new DishIngredient { Dish = leChef, Ingredient = tomatoSauce };
                var leChefCheese = new DishIngredient { Dish = leChef, Ingredient = cheese };
                var leChefMozarella = new DishIngredient { Dish = leChef, Ingredient = mozarella };
                var leChefOlives= new DishIngredient { Dish = leChef, Ingredient = olives };
                var leChefBacon = new DishIngredient { Dish = leChef, Ingredient = bacon };

                //List<DishIngredient>
                margherita.DishIngredients = new List<DishIngredient>();
                margherita.DishIngredients.Add(margheritaCheese);
                margherita.DishIngredients.Add(margheritaTomatoSouce);

                vesuvio.DishIngredients = new List<DishIngredient>();
                vesuvio.DishIngredients.Add(vesuvioCheese);
                vesuvio.DishIngredients.Add(vesuvioTomatoSouce);
                vesuvio.DishIngredients.Add(vesuvioHam);

                alFungi.DishIngredients = new List<DishIngredient>();
                alFungi.DishIngredients.Add(alFungiMushroom);
                alFungi.DishIngredients.Add(alFungiCheese);
                alFungi.DishIngredients.Add(alFungiTomatoSauce);

                alTonno.DishIngredients = new List<DishIngredient>();
                alTonno.DishIngredients.Add(alTonnoCheese);
                alTonno.DishIngredients.Add(alTonnoTomatoSauce);
                alTonno.DishIngredients.Add(alTonnoTuna);

                bussola.DishIngredients = new List<DishIngredient>();
                bussola.DishIngredients.Add(bussolaCheese);
                bussola.DishIngredients.Add(bussolaTomatoSauce);
                bussola.DishIngredients.Add(bussolaHam);
                bussola.DishIngredients.Add(bussolaShrimp);

                opera.DishIngredients = new List<DishIngredient>();
                opera.DishIngredients.Add(operaCheese);
                opera.DishIngredients.Add(operaTomatoSauce);
                opera.DishIngredients.Add(operaHam);
                opera.DishIngredients.Add(operaTuna);

                capricciosa.DishIngredients = new List<DishIngredient>();
                capricciosa.DishIngredients.Add(capricciosaCheese);
                capricciosa.DishIngredients.Add(capricciosaTomatoSauce);
                capricciosa.DishIngredients.Add(capricciosaHam);
                capricciosa.DishIngredients.Add(capricciosaMushroom);


                mexicana.DishIngredients = new List<DishIngredient>();
                mexicana.DishIngredients.Add(mexicanaCheese);
                mexicana.DishIngredients.Add(mexicanaTomatoSauce);
                mexicana.DishIngredients.Add(mexicanaPepperoni);
                mexicana.DishIngredients.Add(mexicanaChili);
                mexicana.DishIngredients.Add(mexicanaBacon);

                azteka.DishIngredients = new List<DishIngredient>();
                azteka.DishIngredients.Add(aztekaCheese);
                azteka.DishIngredients.Add(aztekaTomatoSauce);
                azteka.DishIngredients.Add(aztekaHam);
                azteka.DishIngredients.Add(aztekaChili);

                venice.DishIngredients = new List<DishIngredient>();
                venice.DishIngredients.Add(veniceCheese);
                venice.DishIngredients.Add(veniceTomatoSauce);
                venice.DishIngredients.Add(veniceMozarella);

                leChef.DishIngredients = new List<DishIngredient>();
                leChef.DishIngredients.Add(leChefCheese);
                leChef.DishIngredients.Add(leChefTomatoSauce);
                leChef.DishIngredients.Add(leChefOlives);
                leChef.DishIngredients.Add(leChefMozarella);
                leChef.DishIngredients.Add(leChefBacon);


                context.Ingredients.AddRange(cheese, tomatoSauce, ham, mushroom, bacon, curry, banana, pineapple, shrimp, tuna, extraTomato, chicken, olives, pepperoni, mozarella, salmon, extraCheese, chili);
                context.Dishes.AddRange(vesuvio, margherita, bussola, alFungi, alTonno, opera, azteka, mexicana, leChef, venice);
                context.DishIngredients.AddRange(
                    margheritaCheese, margheritaTomatoSouce,
                    vesuvioHam, vesuvioCheese, vesuvioTomatoSouce,
                    alFungiCheese, alFungiTomatoSauce, alFungiMushroom,
                    alTonnoCheese, alTonnoTuna, alTonnoTomatoSauce,
                    bussolaCheese, bussolaHam, bussolaShrimp, bussolaTomatoSauce,
                    operaCheese, operaHam, operaTomatoSauce, operaTuna,
                    capricciosaTomatoSauce, capricciosaCheese, capricciosaHam, capricciosaMushroom,
                    mexicanaCheese, mexicanaPepperoni, mexicanaTomatoSauce, mexicanaChili, mexicanaBacon,
                    aztekaCheese, aztekaChili, aztekaHam, aztekaTomatoSauce,
                    veniceMozarella, veniceCheese, veniceTomatoSauce,
                    leChefBacon, leChefCheese, leChefMozarella, leChefOlives
                   );
                context.SaveChanges();
            }
        }
    }
}