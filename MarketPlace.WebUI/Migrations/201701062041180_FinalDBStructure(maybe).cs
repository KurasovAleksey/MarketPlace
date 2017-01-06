namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalDBStructuremaybe : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserChats", newName: "CharMemberships");
            AddColumn("dbo.AspNetUsers", "BanStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUserRoles", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.AspNetUsers", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUserRoles", "StartDate");
            DropColumn("dbo.AspNetUsers", "BanStatus");
            RenameTable(name: "dbo.CharMemberships", newName: "UserChats");
        }
    }
}
