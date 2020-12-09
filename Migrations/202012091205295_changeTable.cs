namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActorsInMovies", "MovieId", "dbo.Movies");
            DropIndex("dbo.ActorsInMovies", new[] { "MovieId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ActorsInMovies", "MovieId");
            AddForeignKey("dbo.ActorsInMovies", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
        }
    }
}
