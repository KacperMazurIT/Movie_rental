namespace MovieRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewRentalTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewRentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        MovieName = c.String(),
                        DateRented = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NewRentals");
        }
    }
}
