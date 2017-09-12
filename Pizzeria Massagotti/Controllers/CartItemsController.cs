using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzeriaMassagotti.Data;
using PizzeriaMassagotti.Models;
using PizzeriaMassagotti.Services;
using Microsoft.AspNetCore.Http;

namespace PizzeriaMassagotti.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DishService _dishService;
        private readonly IngredientService _ingredientService;
        private readonly CartService _cartService;

        public CartItemsController(ApplicationDbContext context, DishService dishService, IngredientService ingredientService, CartService cartService)
        {
            _context = context;
            _dishService = dishService;
            _ingredientService = ingredientService;
            _cartService = cartService;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CartItems.Include(c => c.Dish).Include(c => c.ShoppingCart);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Dish)
                .Include(c => c.ShoppingCart)
                .SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId");
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CartItemId,ShoppingCartId,DishId,Quantity")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", cartItem.DishId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", cartItem.ShoppingCartId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var cartItem = await _context.CartItems.SingleOrDefaultAsync(m => m.CartItemId == id);
            var cartItem = await _context.CartItems.Include(c => c.Dish).Include(c => c.CartItemIngredients).ThenInclude(c=> c.Ingredient).SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "Name", cartItem.DishId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", cartItem.ShoppingCartId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CartItemId,ShoppingCartId,DishId,Quantity,Price")] CartItem cartItem, IFormCollection collection)
        {
            if (id != cartItem.CartItemId)
            {
                return NotFound();
            }
            ///////från DishController //////////

            _cartService.RemoveCartItemIngredients(id);
            var _cartItem = _context.CartItems.Include(ci => ci.CartItemIngredients)
                .Where(ci => ci.CartItemId == id).FirstOrDefault();

            foreach (var item in collection.Keys.Where(m => m.StartsWith("ingredient-")))
            {
                var ingStr = item.Remove(0, 11);
                var ingId = Int32.Parse(ingStr);
                var listIngredient = _ingredientService.All().FirstOrDefault(d => d.IngredientId == ingId);

                _cartItem.CartItemIngredients.Add(new CartItemIngredient() { Ingredient = listIngredient });
                _cartItem.Price += _dishService.DishHasIngredient(_cartItem.DishId, listIngredient.IngredientId) 
                    ? 0: listIngredient.Price;

            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(_cartItem.CartItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
             
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Home");
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", _cartItem.DishId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", _cartItem.ShoppingCartId);
            return View(_cartItem);

        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Dish)
                .Include(c => c.ShoppingCart)
                .SingleOrDefaultAsync(m => m.CartItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartItem = await _context.CartItems.SingleOrDefaultAsync(m => m.CartItemId == id);
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.CartItemId == id);
        }
    }
}
