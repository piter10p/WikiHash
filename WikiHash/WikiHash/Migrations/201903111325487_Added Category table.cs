namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategorytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CategoryName, unique: true);
            
            AddColumn("dbo.Articles", "CategoryId", c => c.Int());
            AddColumn("dbo.Media", "CategoryId", c => c.Int());
            CreateIndex("dbo.Articles", "CategoryId");
            CreateIndex("dbo.Media", "CategoryId");
            AddForeignKey("dbo.Articles", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Media", "CategoryId", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Articles", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Media", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "CategoryName" });
            DropIndex("dbo.Articles", new[] { "CategoryId" });
            DropColumn("dbo.Media", "CategoryId");
            DropColumn("dbo.Articles", "CategoryId");
            DropTable("dbo.Categories");
        }
    }
}
