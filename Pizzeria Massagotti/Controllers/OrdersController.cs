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

namespace PizzeriaMassagotti.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public OrdersController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.ShoppingCart);
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        public ActionResult Index(int shoppingCartId)
        {
            var applicationDbContext = _context.Orders.Include(o => o.ShoppingCart).FirstOrDefault(c =>c.ShoppingCartId == shoppingCartId);
            return View(applicationDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ShoppingCart)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ShoppingCartId,Anonymous,ApplicationUserId,OrderDateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", order.ShoppingCartId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}
            var order = _cartService.SaveCartOnOrder(id);
            //var order = await _context.Orders.Include(m =>m.ShoppingCart).SingleOrDefaultAsync(m => m.ShoppingCartId == id);
            if (order == null)
            {
                return NotFound();
            }

            order.Anonymous = !User.Identity.IsAuthenticated;

            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", order.ShoppingCartId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ShoppingCartId,Anonymous,ApplicationUserId,OrderDateTime")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCart, "ShoppingCartId", "ShoppingCartId", order.ShoppingCartId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.ShoppingCart)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
