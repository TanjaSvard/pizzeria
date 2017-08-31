using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaMassagotti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;

namespace PizzeriaMassagotti.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _services;
        private readonly ISession _session;

        public CartService(ApplicationDbContext context, IServiceProvider services)
        {
            _context = context;
            _services = services;
            _session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

        }

        public void AddDish(int dishId)
        {
            byte[] cartIdBytes = new byte[4];

            bool exists = _session.TryGetValue("cartId", out cartIdBytes);

            if (!exists)
            {
                var cart = new ShoppingCart();
                _context.Add(cart);
                _context.SaveChanges();
                _session.SetInt32("cartId", cart.ShoppingCartId);
            }
            else
            {
                //int ? cartId = _session.GetInt32("cartId");
                int cartId = _session.GetInt32("cartId").Value;
                CartItem cartItem = new CartItem();
                cartItem.ShoppingCartId = cartId;
                cartItem.DishId = dishId;

                _context.Add(cartItem);
                _context.SaveChanges();
            }
            
        }
            

    }
}
