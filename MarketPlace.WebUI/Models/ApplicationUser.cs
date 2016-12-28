using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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
        public ApplicationUser()
        {
        }

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
        [Column("RegistrationDate", TypeName = "datetime2")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [MaxLength(20)]
        public override string UserName { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<Bid> Bids { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Message> SendingMessages { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Message> ReceivingMessages { get; set; }

        [InverseProperty("Sender")]
        public ICollection<Feedback> SendingFeedbacks { get; set; }

        [InverseProperty("Receiver")]
        public ICollection<Feedback> ReceivingFeedbacks { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}