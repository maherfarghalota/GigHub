namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNotification : DbMigration
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
                    Gig_ID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Gigs", t => t.Gig_ID, cascadeDelete: true)
                .Index(t => t.Gig_ID);

            CreateTable(
                "dbo.UserNotifications",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    NotificationID = c.Int(nullable: false),
                    IsRead = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.NotificationID })
                .ForeignKey("dbo.Notifications", t => t.NotificationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.NotificationID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationID", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "Gig_ID", "dbo.Gigs");
            DropIndex("dbo.UserNotifications", new[] { "NotificationID" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "Gig_ID" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
