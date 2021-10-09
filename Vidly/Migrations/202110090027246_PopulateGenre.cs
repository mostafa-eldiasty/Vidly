namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenre : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Genres(Id,Name) Values(1,'Comedy')");
            Sql("insert into Genres(Id,Name) Values(2,'Drama')");
            Sql("insert into Genres(Id,Name) Values(3,'Fantasy')");
            Sql("insert into Genres(Id,Name) Values(4,'Horror')");
            Sql("insert into Genres(Id,Name) Values(5,'Mystery')");
            Sql("insert into Genres(Id,Name) Values(6,'Romance')");
            Sql("insert into Genres(Id,Name) Values(7,'Thriller')");
            Sql("insert into Genres(Id,Name) Values(8,'Action')");
        }
        
        public override void Down()
        {
        }
    }
}
