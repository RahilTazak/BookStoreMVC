using Microsoft.AspNetCore.Identity;

namespace BookStoreMvc.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name{ get; set; }
    }
}
