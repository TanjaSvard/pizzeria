using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaMassagotti.Data;
using PizzeriaMassagotti.Services;
using System;
using Xunit;

namespace PizzeriaMassagottiUnitTest
{

    public class IngredientServiceTests: BasePizzeriaTests
    {

        public override void InitializeDatabase()
        {
            base.InitializeDatabase();
            var context = serviceProvider.GetService<ApplicationDbContext>();
            context.Ingredients.Add(new PizzeriaMassagotti.Models.Ingredient { Name = "BBB", Price = 2});
            context.Ingredients.Add(new PizzeriaMassagotti.Models.Ingredient { Name = "AAA", Price = 5 });
            context.SaveChanges();
            }

        [Fact]
        public void All_Are_Sorted()
        {

            var _ingredients = serviceProvider.GetService<IngredientService>();//Arrange
            var ings = _ingredients.All();//Act
            //Assert.Equal(ings.Count, 0);
            Assert.Equal(2, ings.Count);//Assert
            Assert.Equal(ings[0].Name, "AAA");
            Assert.Equal(ings[1].Name, "BBB");
        }
    }
}
