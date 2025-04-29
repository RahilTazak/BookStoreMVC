using BookStoreMvc.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Text.Json.Serialization;

namespace BookStoreMvc.Models.DTO
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderDetails")]
        public int? OrderId { get; set; }  // Nullable if CartItem is not ordered yet

        [JsonIgnore]
        public OrderDetails Order { get; set; }
        public bool IsOrdered { get; set; } = false;   //Update it to true at order placement

    }
}
