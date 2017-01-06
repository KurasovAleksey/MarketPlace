namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondFinalDBStructuremaybe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "isClosed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Items", "PicturePath", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "PicturePath", c => c.String(maxLength: 2000));
            DropColumn("dbo.Sales", "isClosed");
        }
    }
}
