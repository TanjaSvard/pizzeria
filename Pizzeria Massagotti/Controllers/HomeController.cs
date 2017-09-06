using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Services;
using PizzeriaMassagotti.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace PizzeriaMassagotti.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DishService _dishService;
        private readonly IngredientService _ingredientService;
        private readonly CartService _cartService;

        public HomeController(ApplicationDbContext context, DishService dishService, IngredientService ingredientService, CartService cartService)
        {
            _context = context;
            _dishService = dishService;
            _ingredientService = ingredientService;
            _cartService = cartService;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.Include(d =>d.Dishes).ThenInclude(c =>c.DishIngredients).ThenInclude(x =>x.Ingredient).ToListAsync());           
        }


        public IActionResult MakeOrderAction(IFormCollection form)
        {
            var key = form.Keys.FirstOrDefault(k => k.Contains("-"));
            var dashPos = key.IndexOf("-");
            var action = key.Substring(0, dashPos);
            var id = int.Parse(key.Substring(dashPos + 1));
            switch (action)
            {
                case "add":
                    _cartService.AddDish(id);

                    break;
                //case "remove":
                //    _cartService.RemoveDish(id);
                //    break;
                case "increase":
                    _cartService.IncreaseNumberOfDishInCart(id);
                        break;
                case "decrease":
                    _cartService.DecreaseNumberOfDishInCart(id);
                    break;
                case "customize":
                    _cartService.CustomizeDish(id);
                    break;

                    //case "delete":
                    //    _cartService.RemoveDish(id);
                    //    break;
                    //case "customize":
                    //    return RedirectToAction("Customize", "CartItems", new { })
                    //    break;
            }

            return RedirectToAction("Index", _context.Categories.ToList());
    }


    public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
