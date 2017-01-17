using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MarketPlace.WebUI.Models.AccountModels.Utils;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
    int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public ApplicationDbContext() : base("MarketPlace")
        {
        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Auction>().Property(a => a.Price).HasPrecision(16, 4);
            modelBuilder.Entity<Bid>().Property(b => b.Amount).HasPrecision(16, 4);
        }
    }
}