using BookStoreMvc.Models.Domain;
using BookStoreMvc.Models.DTO;

namespace BookStoreMvc.Repositories.Abstract
{
    public interface ICartService
    {
        public Task<bool> AddToCart(int BookId);
        public Task<bool> RemoveFromCart(int BookId);
        public Task<List<CartItem>> GetCart(string UserId);

        public Task<List<CartItem>> GetUserCart();

        public Task<int> GetCartItemCount();

        public Task<bool> DoCheckout(OrderDetails order);

        public string GetUserId();

    }
}
