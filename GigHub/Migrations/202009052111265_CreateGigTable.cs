namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGigTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        id = c.Byte(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        dateTime = c.DateTime(nullable: false),
                        venue = c.String(),
                        artist_Id = c.String(maxLength: 128),
                        genre_id = c.Byte(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.artist_Id)
                .ForeignKey("dbo.Genres", t => t.genre_id)
                .Index(t => t.artist_Id)
                .Index(t => t.genre_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "genre_id", "dbo.Genres");
            DropForeignKey("dbo.Gigs", "artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "genre_id" });
            DropIndex("dbo.Gigs", new[] { "artist_Id" });
            DropTable("dbo.Gigs");
            DropTable("dbo.Genres");
        }
    }
}
