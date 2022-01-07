namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberAvaliable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvaliable", c => c.Int(nullable: false));
            Sql("Update Movies Set NumberAvaliable = NumberInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvaliable");
        }
    }
}
