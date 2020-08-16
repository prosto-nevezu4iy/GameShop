namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageToCategoriesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Image", c => c.Binary());
            AddColumn("dbo.Categories", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "ImageMimeType");
            DropColumn("dbo.Categories", "Image");
        }
    }
}
