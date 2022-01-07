namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewRental2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewRentals", "DateRented", c => c.DateTime(nullable: false));
            AddColumn("dbo.NewRentals", "DateReturned", c => c.DateTime());
            AddColumn("dbo.NewRentals", "Customer_Id", c => c.Int(nullable: false));
            AddColumn("dbo.NewRentals", "Movie_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NewRentals", "Customer_Id");
            CreateIndex("dbo.NewRentals", "Movie_Id");
            AddForeignKey("dbo.NewRentals", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            DropColumn("dbo.NewRentals", "CustomerId");
            DropColumn("dbo.NewRentals", "MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewRentals", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.NewRentals", "CustomerId", c => c.Int(nullable: false));
            DropForeignKey("dbo.NewRentals", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.NewRentals", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.NewRentals", new[] { "Movie_Id" });
            DropIndex("dbo.NewRentals", new[] { "Customer_Id" });
            DropColumn("dbo.NewRentals", "Movie_Id");
            DropColumn("dbo.NewRentals", "Customer_Id");
            DropColumn("dbo.NewRentals", "DateReturned");
            DropColumn("dbo.NewRentals", "DateRented");
        }
    }
}
