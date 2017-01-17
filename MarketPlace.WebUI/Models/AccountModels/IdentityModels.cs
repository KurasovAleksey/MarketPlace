using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public ApplicationUserRole()
        {
            StartDate = DateTime.Today;
        }

        [Column("StartDate", TypeName = "datetime2")]
        public DateTime StartDate { get; set; }
    }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationUserLogin : IdentityUserLogin<int> { }
}