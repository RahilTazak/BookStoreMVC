using BookStoreMvc.Models.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;

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

    }
}
