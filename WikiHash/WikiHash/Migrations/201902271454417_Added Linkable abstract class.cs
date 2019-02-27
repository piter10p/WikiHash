namespace WikiHash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLinkableabstractclass : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Articles");
            DropPrimaryKey("dbo.Media");
            DropColumn("dbo.Articles", "ArticleId");
            DropColumn("dbo.Articles", "Link");
            DropColumn("dbo.Media", "MediaId");
            DropColumn("dbo.Media", "Link");
            AddColumn("dbo.Articles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Media", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Media", "Title", c => c.String(maxLength: 256));
            AddPrimaryKey("dbo.Articles", "Id");
            AddPrimaryKey("dbo.Media", "Id");
            CreateIndex("dbo.Articles", "Title", unique: true);
            CreateIndex("dbo.Media", "Title", unique: true);
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Media", "Link", c => c.String(maxLength: 128));
            AddColumn("dbo.Media", "MediaId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Articles", "Link", c => c.String(maxLength: 512));
            AddColumn("dbo.Articles", "ArticleId", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.Media", new[] { "Title" });
            DropIndex("dbo.Articles", new[] { "Title" });
            DropPrimaryKey("dbo.Media");
            DropPrimaryKey("dbo.Articles");
            AlterColumn("dbo.Media", "Title", c => c.String(maxLength: 128));
            DropColumn("dbo.Media", "Id");
            DropColumn("dbo.Articles", "Id");
            AddPrimaryKey("dbo.Media", "MediaId");
            AddPrimaryKey("dbo.Articles", "ArticleId");
        }
    }
}
