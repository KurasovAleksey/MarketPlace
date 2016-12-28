namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Information = c.String(nullable: false, maxLength: 400, unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ItemId = c.Int(nullable: false),
                        FinishDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MaxQuantity = c.Short(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: false)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        PicturePath = c.String(maxLength: 2000),
                        UserId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Sname = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleId = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserName = c.String(nullable: false, maxLength: 256),
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
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserId = c.Int(nullable: false),
                        AuctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.Sales", t => t.AuctionId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.AuctionId);
            
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
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Text = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MessageSenderId = c.Int(nullable: false),
                        MessageReceiverId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.MessageReceiverId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.MessageSenderId, cascadeDelete: false)
                .Index(t => t.MessageSenderId)
                .Index(t => t.MessageReceiverId);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
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
                        Title = c.String(nullable: false, maxLength: 50),
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
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Sales", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "MessageSenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "FeedbackSenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "MessageReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "FeedbackReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Sales");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "MessageReceiverId" });
            DropIndex("dbo.Messages", new[] { "MessageSenderId" });
            DropIndex("dbo.Feedbacks", new[] { "FeedbackReceiverId" });
            DropIndex("dbo.Feedbacks", new[] { "FeedbackSenderId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropIndex("dbo.Bids", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Items", new[] { "UserId" });
            DropIndex("dbo.Sales", new[] { "ItemId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Messages");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Bids");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Items");
            DropTable("dbo.Sales");
        }
    }
}
