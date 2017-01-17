namespace MarketPlace.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bids", "IsFinalBid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bids", "IsFinalBid");
        }
    }
}
