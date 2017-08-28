using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Data;

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
            return _context.DishIngredients.Where(d => d.DishId == dishId).ToList();
        }

        public List<DishIngredient> DishIngredientsAll()
        {
            return _context.DishIngredients.ToList();
        }
    }
}
