using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PizzeriaMassagotti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzeriaMassagotti.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzeriaMassagotti.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;

        public CartService(ApplicationDbContext context, ISession session)
        {
            _context = context;
            _session = session;
        }


        
        public ShoppingCart GetCart()
        {
            byte[] cartIdBytes = new byte[4];

            bool exists = _session.TryGetValue("cartId", out cartIdBytes);

            if (!exists)
            {
                return new ShoppingCart { CartItems = new List<CartItem>() };
            }

            int shoppingCartId = _session.GetInt32("cartId").Value;

            return _context.ShoppingCart.Include(c => c.CartItems)
                .Where(c => c.ShoppingCartId == shoppingCartId).FirstOrDefault();

        }

        public Order SaveCartOnOrder(int cartId)
        {
            var order = new Order();
            var v = _context.ShoppingCart.Include(c => c.CartItems).ThenInclude(ci => ci.CartItemIngredients).FirstOrDefault(c => c.ShoppingCartId == cartId);
            var listOfDishes = _context.Dishes.Include(m=>m.DishIngredients).ThenInclude(c=>c.Ingredient).ToList();
            order.ShoppingCartId = v.ShoppingCartId;
            

            foreach (var item in v.CartItems)
            {
                var d = listOfDishes.FirstOrDefault(x => x.DishId == item.DishId);
                item.Dish = d;                 
            }
            order.CartItems = v.CartItems;
            _context.Add(order);
            _context.SaveChanges();
            return order;
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
            //if (_context.CartItems.Any(c => c.DishId == dishId && c.ShoppingCartId == shoppingCartId))
            //{
            //    _context.CartItems.FirstOrDefault(c => c.DishId == dishId && c.ShoppingCartId == shoppingCartId).Quantity++;
            //    _context.SaveChanges();
            //}
            //else
            //{
            CartItem cartItem = new CartItem();
            cartItem.ShoppingCartId = shoppingCartId;
            cartItem.DishId = dishId;
            cartItem.Price = _context.Dishes.FirstOrDefault(x => x.DishId == dishId).Price;
            cartItem.CartItemIngredients = new List<CartItemIngredient>();
            foreach (var item in _context.DishIngredients.Where(m => m.DishId == dishId))
            {
                cartItem.CartItemIngredients.Add(new CartItemIngredient { CartItem = cartItem, IngredientId = item.IngredientId});              
            }
            cartItem.Quantity = 1;
            
            _context.Add(cartItem);
            _context.SaveChanges();
            //}

        }

        public void RemoveDish(int cartItemId)
        {
            int shoppingCartId = _session.GetInt32("cartId").Value;
            if (_context.CartItems.Any(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId))
            {
                _context.Remove(_context.CartItems.Find(cartItemId));
                _context.SaveChanges();

            }

        }


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

                var quantity = _context.CartItems.FirstOrDefault(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId).Quantity;

                if (quantity > 1)
                {
                    _context.CartItems.FirstOrDefault(c => c.CartItemId == cartItemId && c.ShoppingCartId == shoppingCartId).Quantity--;
                    _context.SaveChanges();
                }

                else
                {
                    RemoveDish(cartItemId);
                }
            }
        }


        public void RemoveCartItemIngredients(int cartItemId)
        {
            var cartItemIngs = _context.CartItemIngredients.Where(x => x.CartItemId == cartItemId);

            foreach (var ing in cartItemIngs)
            {
                _context.Remove(ing);
            }

            _context.SaveChanges();
        }


        public List<CartItemIngredient> All(int cartItemId)
        {
            return _context.CartItemIngredients.Include(c =>c.Ingredient).Where(x => x.CartItemId == cartItemId).ToList();

        }

  
        public int TotalAmount(int shopCartId)
        {
            return _context.CartItems.Where(ci => ci.ShoppingCartId == shopCartId)
                .Sum(ci => ci.Price * ci.Quantity);
        }
    }


  
 

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
