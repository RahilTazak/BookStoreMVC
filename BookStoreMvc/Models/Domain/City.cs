using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreMvc.Models.Domain
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public string CityName { get; set; }
        public Country Country { get; set; }
    }
}
