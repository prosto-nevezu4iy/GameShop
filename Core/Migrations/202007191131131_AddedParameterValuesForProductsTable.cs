namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParameterValuesForProductsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductValues",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ValueId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ParameterValues", t => t.ValueId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ValueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductValues", "ValueId", "dbo.ParameterValues");
            DropForeignKey("dbo.ProductValues", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductValues", new[] { "ValueId" });
            DropIndex("dbo.ProductValues", new[] { "ProductId" });
            DropTable("dbo.ProductValues");
        }
    }
}
