using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Packaging.Signing;

namespace BookStoreMvc.Repositories.Implementation
{
    public class CartService : ICartService

    {
        private readonly DatabaseContext ctx;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartService(DatabaseContext ctx, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            this.ctx = ctx;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AddToCart(int BookId)
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User is not logged in");
                }
                List<CartItem> cart = await GetCart(userId);
                CartItem matchingItem = null;
                foreach (var Item in cart)
                {
                    if (Item.BookId == BookId)
                    {
                        matchingItem = Item;
                    }
                }

                if (matchingItem != null)
                {
                    matchingItem.Quantity = matchingItem.Quantity + 1;
                    ctx.CartItems.Update(matchingItem);
                }
                else
                {
                    CartItem cartItem = new CartItem
                    {
                        UserId = userId,
                        BookId = BookId,
                        Quantity = 1
                    };
                    ctx.CartItems.Add(cartItem);
                }
                ctx.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveFromCart(int BookId)
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User is not logged in");
                }
                List<CartItem> cart = await GetCart(userId);
                CartItem matchingItem = null;
                foreach (var Item in cart)
                {
                    if (Item.BookId == BookId)
                    {
                        matchingItem = Item;
                    }
                }

                if (matchingItem == null)
                {
                    throw new InvalidOperationException("No items in cart");
                }
                else if (matchingItem.Quantity == 1)
                {
                    ctx.CartItems.Remove(matchingItem);
                }
                else
                {
                    matchingItem.Quantity = matchingItem.Quantity - 1;
                    ctx.CartItems.Update(matchingItem);
                }
                ctx.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal);
            return userId;
        }

        public async Task<List<CartItem>> GetCart(string UserId)
        {
            var cart = await ctx.CartItems.Where(x => x.UserId == UserId).ToListAsync<CartItem>();
            return cart;
        }

        public async Task<List<CartItem>> GetUserCart()
        {
            var userId = GetUserId();
            if (userId == null)
                throw new InvalidOperationException("Invalid Userid");
            var shoppingCart = await ctx.CartItems
                                  .Include(x => x.Book)
                                  .Where(a => a.UserId == userId).ToListAsync();
            return shoppingCart;
        }
    }
}


