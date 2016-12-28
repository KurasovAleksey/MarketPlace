using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
    int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("IdentityDb") { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}