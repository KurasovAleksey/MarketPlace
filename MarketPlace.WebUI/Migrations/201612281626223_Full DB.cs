namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FullDB : DbMigration
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
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
                        UserId = c.Int(nullable: false),
                        AuctionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.Auctions", t => t.AuctionId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AuctionId);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        FeedbackId = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 300),
                        Datetime = c.DateTime(nullable: false),
                        UserFromId = c.Int(nullable: false),
                        UserToId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserToId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFromId, cascadeDelete: false)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Text = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UserFromId = c.Int(nullable: false),
                        UserToId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserToId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFromId, cascadeDelete: false)
                .Index(t => t.UserFromId)
                .Index(t => t.UserToId);
            
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
                "dbo.Auctions",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        FinishDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        MaxQuantity = c.Short(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .Index(t => t.SaleId);
            
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shops", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Auctions", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropForeignKey("dbo.Messages", "UserFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "UserFromId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "UserToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Feedbacks", "UserToId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropIndex("dbo.Shops", new[] { "SaleId" });
            DropIndex("dbo.Auctions", new[] { "SaleId" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Messages", new[] { "UserToId" });
            DropIndex("dbo.Messages", new[] { "UserFromId" });
            DropIndex("dbo.Feedbacks", new[] { "UserToId" });
            DropIndex("dbo.Feedbacks", new[] { "UserFromId" });
            DropIndex("dbo.Bids", new[] { "AuctionId" });
            DropIndex("dbo.Bids", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Items", new[] { "UserId" });
            DropIndex("dbo.Sales", new[] { "ItemId" });
            AlterColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            DropTable("dbo.Shops");
            DropTable("dbo.Auctions");
            DropTable("dbo.Categories");
            DropTable("dbo.Messages");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Bids");
            DropTable("dbo.Items");
            DropTable("dbo.Sales");
        }
    }
}
