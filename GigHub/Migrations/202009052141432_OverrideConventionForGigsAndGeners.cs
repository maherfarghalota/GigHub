namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverrideConventionForGigsAndGeners : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gigs", "artist_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Gigs", "genre_id", "dbo.Genres");
            DropIndex("dbo.Gigs", new[] { "artist_Id" });
            DropIndex("dbo.Gigs", new[] { "genre_id" });
            AlterColumn("dbo.Genres", "name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Gigs", "venue", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Gigs", "artist_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Gigs", "genre_id", c => c.Byte(nullable: false));
            CreateIndex("dbo.Gigs", "artist_Id");
            CreateIndex("dbo.Gigs", "genre_id");
            AddForeignKey("dbo.Gigs", "artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Gigs", "genre_id", "dbo.Genres", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "genre_id", "dbo.Genres");
            DropForeignKey("dbo.Gigs", "artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "genre_id" });
            DropIndex("dbo.Gigs", new[] { "artist_Id" });
            AlterColumn("dbo.Gigs", "genre_id", c => c.Byte());
            AlterColumn("dbo.Gigs", "artist_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Gigs", "venue", c => c.String());
            AlterColumn("dbo.Genres", "name", c => c.String());
            CreateIndex("dbo.Gigs", "genre_id");
            CreateIndex("dbo.Gigs", "artist_Id");
            AddForeignKey("dbo.Gigs", "genre_id", "dbo.Genres", "id");
            AddForeignKey("dbo.Gigs", "artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
