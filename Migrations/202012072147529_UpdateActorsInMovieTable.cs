namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActorsInMovieTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ActorsInMovies", name: "MovieId", newName: "Movie_Id");
            RenameIndex(table: "dbo.ActorsInMovies", name: "IX_MovieId", newName: "IX_Movie_Id");
            AddColumn("dbo.ActorsInMovies", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ActorsInMovies", "Customer_Id");
            AddForeignKey("dbo.ActorsInMovies", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            DropColumn("dbo.ActorsInMovies", "ActorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActorsInMovies", "ActorName", c => c.String(nullable: false, maxLength: 255));
            DropForeignKey("dbo.ActorsInMovies", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ActorsInMovies", new[] { "Customer_Id" });
            DropColumn("dbo.ActorsInMovies", "Customer_Id");
            RenameIndex(table: "dbo.ActorsInMovies", name: "IX_Movie_Id", newName: "IX_MovieId");
            RenameColumn(table: "dbo.ActorsInMovies", name: "Movie_Id", newName: "MovieId");
        }
    }
}
