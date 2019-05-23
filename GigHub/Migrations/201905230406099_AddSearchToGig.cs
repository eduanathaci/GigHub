namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSearchToGig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "Search", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gigs", "Search");
        }
    }
}
