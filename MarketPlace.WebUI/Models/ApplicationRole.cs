using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }
}