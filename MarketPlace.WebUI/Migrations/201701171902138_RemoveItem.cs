namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Auctions", "ItemId", "dbo.Items");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "ItemId" });
            DropIndex("dbo.Items", new[] { "UserId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            AddColumn("dbo.Auctions", "Title", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Auctions", "Description", c => c.String(nullable: false, maxLength: 1000));
            AddColumn("dbo.Auctions", "PicturePath", c => c.String(maxLength: 1000));
            AddColumn("dbo.Auctions", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Auctions", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Items", "PicturePath", c => c.String(maxLength: 1000));
            CreateIndex("dbo.Auctions", "UserId");
            CreateIndex("dbo.Auctions", "CategoryId");
            AddForeignKey("dbo.Auctions", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Auctions", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Auctions", "ItemId");
            DropColumn("dbo.Items", "UserId");
            DropColumn("dbo.Items", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.Items", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Auctions", "ItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Auctions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Auctions", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Auctions", new[] { "CategoryId" });
            DropIndex("dbo.Auctions", new[] { "UserId" });
            AlterColumn("dbo.Items", "PicturePath", c => c.String(maxLength: 200));
            DropColumn("dbo.Auctions", "CategoryId");
            DropColumn("dbo.Auctions", "UserId");
            DropColumn("dbo.Auctions", "PicturePath");
            DropColumn("dbo.Auctions", "Description");
            DropColumn("dbo.Auctions", "Title");
            CreateIndex("dbo.Items", "CategoryId");
            CreateIndex("dbo.Items", "UserId");
            CreateIndex("dbo.Auctions", "ItemId");
            AddForeignKey("dbo.Items", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.Auctions", "ItemId", "dbo.Items", "ItemId", cascadeDelete: true);
            AddForeignKey("dbo.Items", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
