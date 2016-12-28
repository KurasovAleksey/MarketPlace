using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketPlace.WebUI.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
    int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext() : base("MarketPlace") { }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Sale>().Property(s => s.Price).HasPrecision(16, 4);
            modelBuilder.Entity<Bid>().Property(b => b.Amount).HasPrecision(16, 4);
        }
    }
}