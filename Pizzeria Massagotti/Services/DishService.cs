using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Data;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaMassagotti.Services
{
    public class DishService
    {
        private readonly ApplicationDbContext _context;
       
        public DishService(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public List<Dish> All()
        {
            return _context.Dishes.ToList();
        }

        public List<DishIngredient> DishIngredientsForDishId(int dishId)
        {
            return _context.DishIngredients.Where(d => d.DishId == dishId).Include(c=>c.Ingredient).OrderBy(i=>i.Ingredient.Name).ToList();
        }
   
        public bool DishHasIngredient(int dishId, int ingredientId)
        {
            return _context.DishIngredients.Any(x => x.DishId == dishId && x.IngredientId == ingredientId);
            
        }

        public void RemoveIngredients(int dishId)
        {
           var dishIngs = _context.DishIngredients.Where(x => x.DishId == dishId);

            foreach (var ing in dishIngs)
            {
                _context.Remove(ing);
            }
            _context.SaveChanges();
        }
    }
}
