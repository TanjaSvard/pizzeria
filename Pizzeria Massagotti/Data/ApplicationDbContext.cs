﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagotti.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Säger explicit vilka ids som ska användas som primary key i dishingredient
            builder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });
            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

            //för en till många-relation räcker detta
            builder.Entity<Dish>()
               .HasOne(c => c.Category)
               .WithMany(d => d.Dishes)
               .HasForeignKey(c => c.CategoryId);

            builder.Entity<CartItemIngredient>()
                .HasKey(di => new { di.CartItemId, di.IngredientId });
            builder.Entity<CartItemIngredient>()
                .HasOne(di => di.CartItem)
                .WithMany(d => d.CartItemIngredients)
                .HasForeignKey(di => di.CartItemId);
            builder.Entity<CartItemIngredient>()
                 .HasOne(di => di.Ingredient)
                 .WithMany(i => i.CartItemIngredients)
                 .HasForeignKey(di => di.IngredientId);              
            
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }     
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemIngredient> CartItemIngredients { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }

    }
}
