namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "LastLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastLogin", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
