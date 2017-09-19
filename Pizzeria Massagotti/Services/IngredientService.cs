using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Data;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaMassagotti.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;
        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Ingredient> All()
        {
            var getIngredients = _context.Ingredients.ToList();

            return getIngredients.OrderBy(i => i.Name).ToList();

            //return _context.Ingredients.OrderByDescending(i => i.Name)).ToList();
        }

        public List<Ingredient> Adjusted(int cartItemId)
        {
            var allIngredients = _context.Ingredients.ToList();
            var cartItem = _context.CartItems.Include(ci=>ci.CartItemIngredients).ThenInclude(i=>i.Ingredient).FirstOrDefault(c=>c.CartItemId == cartItemId);
            var adjustedListOfIngredients = new List<Ingredient>();
            foreach (var item in allIngredients)
            {
                if (!cartItem.CartItemIngredients.Any(c=>c.IngredientId==item.IngredientId))
                {
                    adjustedListOfIngredients.Add(item);
                }                             
            }            
            return adjustedListOfIngredients.OrderBy(c=>c.Name).ToList();
        }
    }
}
