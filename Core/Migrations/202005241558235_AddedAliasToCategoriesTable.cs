namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAliasToCategoriesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Alias", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Alias");
        }
    }
}
