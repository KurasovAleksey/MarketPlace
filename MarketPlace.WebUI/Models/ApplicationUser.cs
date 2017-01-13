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
        [Column("Name", TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Column("Sname", TypeName = "nvarchar")]
        [MaxLength(50)]
        public string Sname { get; set; }

        [Required]
        [Column("RegistrationDate", TypeName = "datetime2")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Username", TypeName = "nvarchar")]
        public override string UserName { get; set; }

        [Required]
        [Column("LastLogin", TypeName = "datetime2")]
        public DateTime LastLoginDtime { get; set; }

        [Required]
        [Column("BanStatus", TypeName = "bit")]
        public bool isBanned { get; set; }




        public ICollection<Item> Items { get; set; }

        public ICollection<Bid> Bids { get; set; }

        //public ICollection<CharMembership> UserChats { get; set; }

        //public ICollection<Message> Messages { get; set; }

        [InverseProperty("FeedbackSender")]
        public ICollection<Feedback> OutgoingFeedbacks { get; set; }

        [InverseProperty("FeedbackReceiver")]
        public ICollection<Feedback> IncomingFeedbacks { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}