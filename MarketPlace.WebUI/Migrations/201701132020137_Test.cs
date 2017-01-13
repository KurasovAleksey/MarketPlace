namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.CharMemberships", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.CharMemberships", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.CharMemberships", new[] { "UserId" });
            DropIndex("dbo.CharMemberships", new[] { "ChatId" });
            DropTable("dbo.Messages");
            DropTable("dbo.CharMemberships");
            DropTable("dbo.Chats");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CharMemberships",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ChatId = c.Int(nullable: false),
                        WasRemoved = c.Boolean(nullable: false),
                        ChatName = c.String(),
                    })
                .PrimaryKey(t => new { t.UserId, t.ChatId });
            
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        Preview = c.String(nullable: false, maxLength: 30),
                        Info = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ChatId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 1000),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SenderId = c.Int(nullable: false),
                        ChatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            CreateIndex("dbo.CharMemberships", "ChatId");
            CreateIndex("dbo.CharMemberships", "UserId");
            CreateIndex("dbo.Messages", "ChatId");
            CreateIndex("dbo.Messages", "SenderId");
            AddForeignKey("dbo.Messages", "SenderId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CharMemberships", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CharMemberships", "ChatId", "dbo.Chats", "ChatId", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "ChatId", "dbo.Chats", "ChatId", cascadeDelete: true);
        }
    }
}
