using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationUser : 
        IdentityUser<int, ApplicationUserLogin, ApplicationUserRole,ApplicationUserClaim>,
        IUser<int>
    {
        [Required]
        [Column("Name", TypeName = "varchar")]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Column("Sname", TypeName = "varchar")]
        [MaxLength(20)]
        public string Sname { get; set; }

        [Column("RoleId", TypeName = "int")]
        public int RoleId { get; set; }

        [Required]
        [Column("RegistrationDate")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        public ApplicationUser()
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
    UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}