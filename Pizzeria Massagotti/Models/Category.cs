using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMassagotti.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
