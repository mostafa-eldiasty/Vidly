namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewRental : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.NewRentals", "MovieId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewRentals", "MovieId", c => c.Int(nullable: false));
        }
    }
}
