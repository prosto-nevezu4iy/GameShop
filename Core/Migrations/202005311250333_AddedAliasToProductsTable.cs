namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAliasToProductsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Alias", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Alias");
        }
    }
}
