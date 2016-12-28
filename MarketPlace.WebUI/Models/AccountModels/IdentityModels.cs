using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationUserRole : IdentityUserRole<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
}