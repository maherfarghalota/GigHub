namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viewModels : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Gigs", new[] { "artist_Id" });
            DropIndex("dbo.Gigs", new[] { "genre_id" });
            CreateIndex("dbo.Gigs", "Artist_Id");
            CreateIndex("dbo.Gigs", "Genre_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Gigs", new[] { "Genre_id" });
            DropIndex("dbo.Gigs", new[] { "Artist_Id" });
            CreateIndex("dbo.Gigs", "genre_id");
            CreateIndex("dbo.Gigs", "artist_Id");
        }
    }
}
