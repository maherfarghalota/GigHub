namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollowing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Followings",
                c => new
                    {
                        FollowerID = c.String(nullable: false, maxLength: 128),
                        FolloweeID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerID, t.FolloweeID })
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerID)
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeID)
                .Index(t => t.FollowerID)
                .Index(t => t.FolloweeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Followings", "FolloweeID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Followings", "FollowerID", "dbo.AspNetUsers");
            DropIndex("dbo.Followings", new[] { "FolloweeID" });
            DropIndex("dbo.Followings", new[] { "FollowerID" });
            DropTable("dbo.Followings");
        }
    }
}
