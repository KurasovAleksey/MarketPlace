using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Sname { get; set; }

        public int UserTypeId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ApplicationUser()
        {
        }
    }
}