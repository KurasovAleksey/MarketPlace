using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace MarketPlace.WebUI.Models.AccountModels.Utils
{
    public class ApplicationUserStore :
        UserStore<ApplicationUser, ApplicationRole, int,
        ApplicationUserLogin, ApplicationUserRole,
        ApplicationUserClaim>, IUserStore<ApplicationUser, int>,
        IDisposable
    {
        public ApplicationUserStore() : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }
}