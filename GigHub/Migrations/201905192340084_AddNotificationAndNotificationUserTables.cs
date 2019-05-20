namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationAndNotificationUserTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        OriginalDateTime = c.DateTime(),
                        OriginalVenue = c.String(),
                        Gig_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Gigs", t => t.Gig_ID)
                .Index(t => t.Gig_ID);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        NotificationID = c.Int(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.NotificationID })
                .ForeignKey("dbo.Notifications", t => t.NotificationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.NotificationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationID", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Gig_ID", "dbo.Gigs");
            DropIndex("dbo.UserNotifications", new[] { "NotificationID" });
            DropIndex("dbo.UserNotifications", new[] { "UserID" });
            DropIndex("dbo.Notifications", new[] { "Gig_ID" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
