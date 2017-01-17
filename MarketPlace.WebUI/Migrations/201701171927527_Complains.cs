namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Complains : DbMigration
    {
        public override void Up()
        {
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
            
            DropTable("dbo.Items");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 1000),
                        PicturePath = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ItemId);
            
            DropForeignKey("dbo.Complaints", "ViolatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Complaints", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Complaints", new[] { "ViolatorId" });
            DropIndex("dbo.Complaints", new[] { "SenderId" });
            DropTable("dbo.Complaints");
        }
    }
}
