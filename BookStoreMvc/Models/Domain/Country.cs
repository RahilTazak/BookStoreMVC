using System.ComponentModel.DataAnnotations;

namespace BookStoreMvc.Models.Domain
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
    }
}
