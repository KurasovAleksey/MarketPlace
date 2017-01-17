namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
