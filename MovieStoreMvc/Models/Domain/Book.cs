using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMvc.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }

        [Display(Name ="Release Year")]
        public string? ReleaseYear { get; set; }

        public string? BookImage { get; set; }  // stores Book image name with extension (eg, image0001.jpg)

        [Required]
        public string? Author { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Genres { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        [NotMapped]
        public string ? GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList ? MultiGenreList { get; set; }

    }
}
