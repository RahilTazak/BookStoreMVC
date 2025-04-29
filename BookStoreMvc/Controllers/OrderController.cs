using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;
using BookStoreMvc.Repositories.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BookStoreMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly DatabaseContext ctx;
        private readonly ICartService cartService;

        public OrderController(DatabaseContext ctx, ICartService cartService)
        {
            this.ctx = ctx;
            this.cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> OrderList()    
        {
            string userId = cartService.GetUserId();
            var ordersList = await ctx.OrderDetails.Include(a=>a.CartItems).Where(x => x.UserId == userId).ToListAsync();
            return Ok(ordersList);
        }       
    }
}
