namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        AuctionId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Information = c.String(maxLength: 400),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PicturePath = c.String(maxLength: 1000),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsNationalCurrency = c.Boolean(nullable: false),
                        FinishDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.AuctionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsFinalBid = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        AuctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.Auctions", t => t.AuctionId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Sname = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 256),
                        BanStatus = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 300),
                        Datetime = c.DateTime(nullable: false),
                        FeedbackSenderId = c.Int(nullable: false),
                        FeedbackReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.AspNetUsers", t => t.FeedbackReceiverId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.FeedbackSenderId, cascadeDelete: false)
                .Index(t => t.FeedbackSenderId)
                .Index(t => t.FeedbackReceiverId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        ComplaintId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BanStatus = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ViolatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComplaintId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.ViolatorId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.ViolatorId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 100),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Auctions", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Complaints", "ViolatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "FeedbackSenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Complaints", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "FeedbackReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Auctions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Complaints", new[] { "ViolatorId" });
            DropIndex("dbo.Complaints", new[] { "SenderId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Feedbacks", new[] { "FeedbackReceiverId" });
            DropIndex("dbo.Feedbacks", new[] { "FeedbackSenderId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropIndex("dbo.Bids", new[] { "UserId" });
            DropIndex("dbo.Auctions", new[] { "CategoryId" });
            DropIndex("dbo.Auctions", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Complaints");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bids");
            DropTable("dbo.Auctions");
        }
    }
}
