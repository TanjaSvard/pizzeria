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



    }
}
