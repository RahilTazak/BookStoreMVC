using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Models.Domain;
using BookStoreMvc.Repositories.Abstract;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;


namespace BookStoreMvc.Controllers
{
    [Authorize]
    public class CartItemsController : Controller
    {
        private readonly DatabaseContext ctx;

        private readonly ICartService _cartService;

        public CartItemsController(DatabaseContext ctx, ICartService cartService)
        {
            this.ctx = ctx;
            this._cartService = cartService;
        }

        // GET: CartItems

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        public async Task<IActionResult> AddToCart(int BookId)
        {
            var addedToCart = await _cartService.AddToCart(BookId);
            if (addedToCart == true)
            {
                TempData["msg"] = "Added To Cart";
                return
                    RedirectToAction("BookDetail", "Home", new { BookId = BookId });
            }
            return View();
        }

        public async Task<IActionResult> UpdateCartQty(int BookId)
        {
            var updatedCart = await _cartService.AddToCart(BookId);
            if (updatedCart == true)
            {
                // TempData["msg"] = "Added To Cart";
                return RedirectToAction("GetUserCart");
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveFromCart(int BookId)
        {
            var RemoveFromCart = await _cartService.RemoveFromCart(BookId);
            if (RemoveFromCart == true)
            {
                return RedirectToAction("GetUserCart");
            }
            return RedirectToAction("GetUserCart");
        }

        [HttpGet]
        public async Task<IActionResult> GetUserCart()
        {

            var userCart = await _cartService.GetUserCart();

            return View(userCart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartService.GetCartItemCount();
            return Ok(cartItem);
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var model = new OrderDetails();
            ViewData["countries"] = new SelectList(ctx.Countries, "Id", "CountryName");
            ViewData["cities"] = new SelectList(ctx.Cities, "Id", "CityName");
            var UserId = _cartService.GetUserId();
            model.UserId = UserId;
            double totalOrderAmt = (double)ctx.CartItems.
                    Where(a => a.UserId == UserId).Select(b => b.Book.Price * b.Quantity).Sum();
            model.TotalOrderAmount = totalOrderAmt;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderDetails order)
        {
            ViewData["countries"] = new SelectList(ctx.Countries, "Id", "CountryName",order.CountryId);
            ViewData["cities"] = new SelectList(ctx.Cities, "Id", "CityName",order.CityId);
            var cartItem = await _cartService.DoCheckout(order);
            if (cartItem == true)
            {
                return RedirectToAction("GetUserCart");
            }
            return View();
        }
    }
}

