namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "MessageReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "MessageSenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "MessageSenderId" });
            DropIndex("dbo.Messages", new[] { "MessageReceiverId" });
            CreateTable(
                "dbo.Dialogs",
                c => new
                    {
                        DialogId = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        GuestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DialogId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.GuestId, cascadeDelete: false)
                .Index(t => t.CreatorId)
                .Index(t => t.GuestId);
            
            CreateTable(
                "dbo.DialogReplies",
                c => new
                    {
                        DialogReplyId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Datetime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SenderId = c.Int(nullable: false),
                        DialogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DialogReplyId)
                .ForeignKey("dbo.Dialogs", t => t.DialogId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.DialogId);
            
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.MessageId);
            
            DropForeignKey("dbo.Dialogs", "GuestId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dialogs", "CreatorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DialogReplies", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DialogReplies", "DialogId", "dbo.Dialogs");
            DropIndex("dbo.DialogReplies", new[] { "DialogId" });
            DropIndex("dbo.DialogReplies", new[] { "SenderId" });
            DropIndex("dbo.Dialogs", new[] { "GuestId" });
            DropIndex("dbo.Dialogs", new[] { "CreatorId" });
            DropTable("dbo.DialogReplies");
            DropTable("dbo.Dialogs");
            CreateIndex("dbo.Messages", "MessageReceiverId");
            CreateIndex("dbo.Messages", "MessageSenderId");
            AddForeignKey("dbo.Messages", "MessageSenderId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "MessageReceiverId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
