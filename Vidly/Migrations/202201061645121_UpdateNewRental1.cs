namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewRental1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewRentals", "MovieId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewRentals", "MovieId");
        }
    }
}
