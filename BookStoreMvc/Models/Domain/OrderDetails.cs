using BookStoreMvc.Models.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMvc.Models.Domain
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int CountryId { get; set; }

        // [NotMapped]
        //[InverseProperty("Order")]
        public List<CartItem> CartItems { get; set; }
        public double TotalOrderAmount { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string OrderStatus { get; set; }
    }
}
