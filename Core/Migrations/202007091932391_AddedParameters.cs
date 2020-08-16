namespace Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedParameters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParameterGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParameterSubGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParameterGroups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ParameterValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SubGroupId = c.Int(nullable: false),
                        Value = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParameterSubGroups", t => t.SubGroupId, cascadeDelete: true)
                .Index(t => t.SubGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParameterSubGroups", "GroupId", "dbo.ParameterGroups");
            DropForeignKey("dbo.ParameterValues", "SubGroupId", "dbo.ParameterSubGroups");
            DropIndex("dbo.ParameterValues", new[] { "SubGroupId" });
            DropIndex("dbo.ParameterSubGroups", new[] { "GroupId" });
            DropTable("dbo.ParameterValues");
            DropTable("dbo.ParameterSubGroups");
            DropTable("dbo.ParameterGroups");
        }
    }
}
