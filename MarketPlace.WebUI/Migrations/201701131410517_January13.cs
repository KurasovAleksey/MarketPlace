namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class January13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.AspNetUsers", "Sname", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Sname", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 20, unicode: false));
        }
    }
}
