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
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Column("Sname", TypeName = "nvarchar")]
        [MaxLength(50)]
        [Display(Name = "Фамилия")]
        public string Sname { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Username", TypeName = "nvarchar")]
        [Display(Name = "Никнейм")]
        public override string UserName { get; set; }

        [Column("BanStatus", TypeName = "bit")]
        [Display(Name = "Бан")]
        public bool isBanned { get; set; }





        public ICollection<Auction> Auctions { get; set; }

        public ICollection<Bid> Bids { get; set; }

        [InverseProperty("FeedbackSender")]
        public ICollection<Feedback> OutgoingFeedbacks { get; set; }

        [InverseProperty("FeedbackReceiver")]
        public ICollection<Feedback> IncomingFeedbacks { get; set; }



        [InverseProperty("Sender")]
        public ICollection<Complaint> OutgoingComplaints { get; set; }

        [InverseProperty("Violator")]
        public ICollection<Complaint> Violations { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser, int> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}