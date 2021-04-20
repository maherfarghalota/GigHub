namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForiegnKeysPropertiesToGig : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Gigs", name: "Artist_Id", newName: "ArtistID");
            RenameColumn(table: "dbo.Gigs", name: "Genre_id", newName: "GenreID");
            RenameIndex(table: "dbo.Gigs", name: "IX_Artist_Id", newName: "IX_ArtistID");
            RenameIndex(table: "dbo.Gigs", name: "IX_Genre_id", newName: "IX_GenreID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Gigs", name: "IX_GenreID", newName: "IX_Genre_id");
            RenameIndex(table: "dbo.Gigs", name: "IX_ArtistID", newName: "IX_Artist_Id");
            RenameColumn(table: "dbo.Gigs", name: "GenreID", newName: "Genre_id");
            RenameColumn(table: "dbo.Gigs", name: "ArtistID", newName: "Artist_Id");
        }
    }
}
