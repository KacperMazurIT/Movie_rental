namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateActorsInMovieTable1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActorsInMovies", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.ActorsInMovies", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.ActorsInMovies", name: "Movie_Id", newName: "MovieId");
            RenameIndex(table: "dbo.ActorsInMovies", name: "IX_Movie_Id", newName: "IX_MovieId");
            AddColumn("dbo.ActorsInMovies", "ActorName", c => c.String(nullable: false));
            DropColumn("dbo.ActorsInMovies", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ActorsInMovies", "Customer_Id", c => c.Int(nullable: false));
            DropColumn("dbo.ActorsInMovies", "ActorName");
            RenameIndex(table: "dbo.ActorsInMovies", name: "IX_MovieId", newName: "IX_Movie_Id");
            RenameColumn(table: "dbo.ActorsInMovies", name: "MovieId", newName: "Movie_Id");
            CreateIndex("dbo.ActorsInMovies", "Customer_Id");
            AddForeignKey("dbo.ActorsInMovies", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
