using PizzeriaMassagotti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagotti.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Category> All()
        {
            return _context.Category.ToList();
        }       

    }
}
