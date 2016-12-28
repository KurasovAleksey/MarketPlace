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

        [InverseProperty("Creator")]
        public ICollection<Dialog> CreatedDialogs { get; set; }

        [InverseProperty("Guest")]
        public ICollection<Dialog> GuestDialogs { get; set; }

        public ICollection<DialogReply> Replies { get; set; }

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