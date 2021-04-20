namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenersTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres VALUES (1, 'Jazz')");
            Sql("INSERT INTO Genres VALUES (2, 'Blues')");
            Sql("INSERT INTO Genres VALUES (3, 'Rock')");
            Sql("INSERT INTO Genres VALUES (4, 'Metal')");
        }
        
        public override void Down()
        {
            Sql("Delete from Genres WHERE id IN (1, 2, 3, 4)");
        }
    }
}
