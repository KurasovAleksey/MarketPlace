namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
