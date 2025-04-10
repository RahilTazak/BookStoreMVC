using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models.Domain
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        public string? GenreName { get; set; }
    }
}
