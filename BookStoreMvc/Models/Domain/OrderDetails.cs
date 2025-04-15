using BookStoreMvc.Models.DTO;

namespace BookStoreMvc.Models.Domain
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IEnumerable<CartItem> Order { get; set; }
        public double TotalOrderAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PaymentMethod { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
