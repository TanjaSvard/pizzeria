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


        public List<CartItem> GetCart()
        {
            byte[] cartIdBytes = new byte[4];

            bool exists = _session.TryGetValue("cartId", out cartIdBytes);

            if (!exists)
            {
                return new List<CartItem>();

            }


            int shoppingCartId = _session.GetInt32("cartId").Value;

            var cartItemList = new List<CartItem>();

            foreach (var item in _context.CartItems.Where(c => c.ShoppingCartId == shoppingCartId))
            {
                cartItemList.Add(item);
            }

            return cartItemList;
        }



        public void AddDish(int dishId)
        {
            byte[] cartIdBytes = new byte[4];

            bool exists = _session.TryGetValue("cartId", out cartIdBytes);

            if (!exists)
            {
                var shoppingCart = new ShoppingCart();
                _context.Add(shoppingCart);
                _context.SaveChanges();
                _session.SetInt32("cartId", shoppingCart.ShoppingCartId);
            }


            //int ? shoppingCartId = _session.GetInt32("cartId");
            int shoppingCartId = _session.GetInt32("cartId").Value;
            if (_context.CartItems.Any(c => c.DishId == dishId && c.ShoppingCartId == shoppingCartId))
            {
                _context.CartItems.FirstOrDefault(c => c.DishId == dishId && c.ShoppingCartId == shoppingCartId).Quantity++;
                _context.SaveChanges();
            }
            else
            {
                CartItem cartItem = new CartItem();
                cartItem.ShoppingCartId = shoppingCartId;
                cartItem.DishId = dishId;
                cartItem.Quantity = 1;
                _context.Add(cartItem);
                _context.SaveChanges();
            }

        }

        //public void RemoveDish(int cartItemId)
        //{
        //    int shoppingCartId = _session.GetInt32("cartId").Value;
        //    if (_context.CartItems.Any(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId))
        //    {
        //        _context.Remove(_context.CartItems.Find(cartItemId));
        //        _context.SaveChanges();

        //    }

        //}


        public void IncreaseNumberOfDishInCart(int cartItemId)
        {
            int shoppingCartId = _session.GetInt32("cartId").Value;
            if (_context.CartItems.Any(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId))
            {
                var quantity = _context.CartItems.FirstOrDefault(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId).Quantity++;
                _context.SaveChanges();
            }
        }

        public void DecreaseNumberOfDishInCart(int cartItemId)
        {
            int shoppingCartId = _session.GetInt32("cartId").Value;

            if (_context.CartItems.Any(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId))
            {
                var quantity = _context.CartItems.FirstOrDefault(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId).Quantity--;
                _context.SaveChanges();
            }

            //else
            //{
            //    RemoveDish(cartItemId);
            //}

        }
    }


    //public int TotalAmount()
    //{
    //    return _context.ShoppingCart.
    //}

    //public int Total()
    //{
    //    return 123;
    //}

    //public int GetTempCartId(ISession session)
    //{
    //    if (!session.GetInt32("CartId").HasValue)
    //    {
    //        var tempCart = new CartItem {Items = new List<CartItem>() };
    //        _context.ShoppingCart.Add(tempCart);
    //        _context.SaveChanges();
    //        session.SetInt32("CartId", tempCart.ShoppingCartId);

    //    }
    //    var cartId = session.GetInt32("CartId").Value;
    //    return cartId;
    //}

    //public async Task AddItemForCurrentSession(ISession session, int dishId)
    //{
    //    var cartItem = new CartItem();
    //    cartItem.ShoppingCartId = GetTempCartId(session);
    //    cartItem.Dish = _context.Dishes.Find(dishId);
    //    cartItem.Quantity = 1;
    //    _context.Add(cartItem);
    //    await _context.SaveChangesAsync();
    //}

}
