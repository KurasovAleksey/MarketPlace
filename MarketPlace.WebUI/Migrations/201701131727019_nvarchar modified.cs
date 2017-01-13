namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nvarcharmodified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Auction", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Shop", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auction");
            RenameTable(name: "dbo.Auction", newName: "Auctions");
            DropIndex("dbo.Sales", new[] { "ItemId" });
            DropIndex("dbo.Auctions", new[] { "SaleId" });
            DropIndex("dbo.Shop", new[] { "SaleId" });
            DropPrimaryKey("dbo.Auctions");
            AddColumn("dbo.Auctions", "AuctionId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Auctions", "Price", c => c.Decimal(nullable: false, precision: 16, scale: 4));
            AddColumn("dbo.Auctions", "Information", c => c.String(nullable: false, maxLength: 400));
            AddColumn("dbo.Auctions", "CreationDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Auctions", "ItemId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Sname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Messages", "Text", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Chats", "Preview", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Chats", "Info", c => c.String(maxLength: 30));
            AlterColumn("dbo.AspNetRoles", "Description", c => c.String(nullable: false, maxLength: 100));
            AddPrimaryKey("dbo.Auctions", "AuctionId");
            CreateIndex("dbo.Auctions", "ItemId");
            AddForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions", "AuctionId", cascadeDelete: false);
            DropColumn("dbo.Auctions", "SaleId");
            DropTable("dbo.Sales");
            DropTable("dbo.Shop");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Shop",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        MaxQuantity = c.Short(),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 16, scale: 4),
                        Information = c.String(nullable: false, maxLength: 400, unicode: false),
                        CreationDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        isClosed = c.Boolean(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
            AddColumn("dbo.Auctions", "SaleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bids", "AuctionId", "dbo.Auctions");
            DropIndex("dbo.Auctions", new[] { "ItemId" });
            DropPrimaryKey("dbo.Auctions");
            AlterColumn("dbo.AspNetRoles", "Description", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Chats", "Info", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.Chats", "Preview", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.Messages", "Text", c => c.String(nullable: false, maxLength: 1000, unicode: false));
            AlterColumn("dbo.AspNetUsers", "Sname", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Auctions", "ItemId");
            DropColumn("dbo.Auctions", "CreationDate");
            DropColumn("dbo.Auctions", "Information");
            DropColumn("dbo.Auctions", "Price");
            DropColumn("dbo.Auctions", "AuctionId");
            AddPrimaryKey("dbo.Auctions", "SaleId");
            CreateIndex("dbo.Shop", "SaleId");
            CreateIndex("dbo.Auctions", "SaleId");
            CreateIndex("dbo.Sales", "ItemId");
            AddForeignKey("dbo.Bids", "AuctionId", "dbo.Auction", "SaleId");
            AddForeignKey("dbo.Shop", "SaleId", "dbo.Sales", "SaleId");
            AddForeignKey("dbo.Auction", "SaleId", "dbo.Sales", "SaleId");
            RenameTable(name: "dbo.Auctions", newName: "Auction");
        }
    }
}
